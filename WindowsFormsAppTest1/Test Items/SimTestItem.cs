using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    /// <summary>
    /// Simulation data source: reads the "Measured" column (column C) of a test log CSV
    /// and returns the value for a given Step name. No instrument is controlled.
    /// CSV path: Config.fileSimCsv (config.ini [Simulation] CSV_File),
    /// default = program folder + SimData.DefaultCsvName.
    /// </summary>
    static class SimData
    {
        public const string DefaultCsvName = "9717622610240172_012909.csv";

        private static readonly object lockObj = new object();
        private static Dictionary<string, string> measured;
        private static HashSet<string> forcedFail;   // steps whose Status column (H) is "Fail"

        /// <summary>Drop the loaded data so the next access reloads from CsvPath (evidence batch mode).</summary>
        public static void Reset()
        {
            lock (lockObj)
            {
                measured = null;
                forcedFail = null;
            }
        }

        public static string CsvPath
        {
            get
            {
                if (!string.IsNullOrEmpty(Config.fileSimCsv))
                    return Config.fileSimCsv;
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultCsvName);
            }
        }

        /// <summary>Load the CSV once. Throws if the file can't be read or has no data table.</summary>
        public static void EnsureLoaded()
        {
            lock (lockObj)
            {
                if (measured != null)
                    return;

                string path = CsvPath;
                if (!File.Exists(path))
                    throw new FileNotFoundException("Simulation CSV not found: " + path);

                Dictionary<string, string> data = new Dictionary<string, string>();
                HashSet<string> fails = new HashSet<string>();
                bool inTable = false;

                foreach (string line in File.ReadAllLines(path, Encoding.Default))
                {
                    List<string> fields = SplitCsvLine(line);
                    if (fields.Count == 0)
                        continue;

                    if (!inTable)
                    {
                        // header row: Step,Test,Measured,...
                        if (fields.Count >= 3 && fields[0].Trim() == "Step" && fields[2].Trim() == "Measured")
                            inTable = true;
                        continue;
                    }

                    string step = fields[0].Trim();
                    if (step.Length == 0)
                        continue;

                    string value = (fields.Count > 2) ? fields[2] : "";
                    if (!data.ContainsKey(step))
                        data.Add(step, value);

                    // column H "Status" = Fail -> this step is forced to fail (failure mode evidence)
                    if (fields.Count > 7 && string.Equals(fields[7].Trim(), "Fail", StringComparison.OrdinalIgnoreCase))
                        fails.Add(step);

                    if (step == "_END")
                        break;      // rows after _END are raw waveform data
                }

                if (!inTable)
                    throw new InvalidDataException("Simulation CSV has no \"Step,Test,Measured\" header: " + path);

                measured = data;
                forcedFail = fails;
                Console.WriteLine("[SIM] Loaded {0} steps ({1} forced fail) from {2}", data.Count, fails.Count, path);
            }
        }

        /// <summary>True if the step's Status column in the simulation CSV is "Fail".</summary>
        public static bool IsForcedFail(string step)
        {
            EnsureLoaded();
            return forcedFail.Contains(step);
        }

        /// <summary>Column C value for the step, or "" if the step is not in the CSV.</summary>
        public static string GetMeasured(string step)
        {
            EnsureLoaded();
            string value;
            return measured.TryGetValue(step, out value) ? value : "";
        }

        private static List<string> SplitCsvLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            sb.Append('"');
                            i++;
                        }
                        else
                            inQuotes = false;
                    }
                    else
                        sb.Append(c);
                }
                else if (c == '"')
                    inQuotes = true;
                else if (c == ',')
                {
                    fields.Add(sb.ToString());
                    sb.Clear();
                }
                else
                    sb.Append(c);
            }
            fields.Add(sb.ToString());
            return fields;
        }
    }

    /// <summary>
    /// Base class for simulated test items: Spec comes from Spec.ini (LoadSpec),
    /// sMeasured comes from the simulation CSV, Pass/Fail uses the normal PassFailJudge().
    /// </summary>
    abstract class SimTestItem : TestItemForm
    {
        protected SimTestItem(string step)
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                SimData.EnsureLoaded();
                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }

        public override WORK_STATUS Proccess()
        {
            try
            {
                sMeasured = SimData.GetMeasured(sStep);
                if (SimData.IsForcedFail(sStep))
                    return WORK_STATUS.FAIL;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }

            return PassFailJudge() ? WORK_STATUS.PASS : WORK_STATUS.FAIL;
        }

        public override void release()
        {
        }
    }
}
