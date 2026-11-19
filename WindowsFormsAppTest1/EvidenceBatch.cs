using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppTest1.TestItem;

namespace WindowsFormsAppTest1
{
    // Evidence batch mode (simulation only):
    //   WindowsFormsAppTest1.exe --evidence <csv folder> <png output folder>
    // Every *.csv in the folder is run once in simulation mode. A step whose Status column is
    // "Fail" fails, the test stops there (remaining steps stay blank), and the test list plus the
    // PASS/FAIL box is saved as <csv name>.png. The program closes after the last file.
    public partial class PhilipsEmeraldTestTool
    {
        private async void EvidenceBatch_Shown(object sender, EventArgs e)
        {
            string csvDir = StaticValues.evidenceCsvDir;
            string outDir = StaticValues.evidenceOutDir;

            try
            {
                if (!Directory.Exists(csvDir))
                    throw new DirectoryNotFoundException("Evidence CSV folder not found: " + csvDir);
                Directory.CreateDirectory(outDir);

                string[] files = Directory.GetFiles(csvDir, "*.csv");
                Array.Sort(files, StringComparer.OrdinalIgnoreCase);

                ProdConfig.failContinueTest = false;    // stop at the first fail
                Golden.verified = true;                 // no golden sample handling in batch mode

                foreach (string csv in files)
                {
                    string png = Path.Combine(outDir, Path.GetFileNameWithoutExtension(csv) + ".png");
                    Console.WriteLine("[EVIDENCE] {0} -> {1}", csv, png);
                    await RunEvidenceCsv(csv, png);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EVIDENCE] " + ex);
                MessageBox.Show(ex.Message, "Evidence batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private async Task RunEvidenceCsv(string csvPath, string pngPath)
        {
            Config.fileSimCsv = csvPath;
            SimData.Reset();

            string sn = ReadSimSerialNumber(csvPath);
            textSn.Text = sn;
            labelLastSn.Text = sn;
            labelTestResult.Text = "N/A";
            progressBar1.Value = 0;
            TestItemInit();
            // Golden.verified is set for batch mode, so the judge adds the golden offset:
            // use 0 so the simulated values are judged as they are in the CSV.
            for (int i = 0; i < dTotalTestSteps; i++)
                testItems.getTestItem(i).sOffset = "0";
            StaticValues.sBarCode = "@" + sn;

            TaskCompletionSource<bool> done = new TaskCompletionSource<bool>();
            RunWorkerCompletedEventHandler handler = null;
            handler = (s, ev) =>
            {
                workerTestProccess.RunWorkerCompleted -= handler;
                done.TrySetResult(true);
            };
            // added after the designer handler, so it runs after the PASS/FAIL result is shown
            workerTestProccess.RunWorkerCompleted += handler;
            workerTestProccess.RunWorkerAsync();
            await done.Task;

            await Task.Delay(500);
            Refresh();
            SaveEvidenceImage(pngPath);
        }

        // Test list + PASS/FAIL box + serial number under it
        private void SaveEvidenceImage(string pngPath)
        {
            Rectangle area = ScreenBounds(gridViewTest);
            area = Rectangle.Union(area, ScreenBounds(labelTestResult));
            area = Rectangle.Union(area, ScreenBounds(labelLastSn));

            using (Bitmap whole = new Bitmap(Width, Height))
            {
                DrawToBitmap(whole, new Rectangle(0, 0, Width, Height));

                Rectangle crop = new Rectangle(area.X - Left, area.Y - Top, area.Width, area.Height);
                crop.Intersect(new Rectangle(0, 0, whole.Width, whole.Height));

                using (Bitmap part = whole.Clone(crop, whole.PixelFormat))
                {
                    part.Save(pngPath, ImageFormat.Png);
                }
            }
        }

        private static Rectangle ScreenBounds(Control c)
        {
            return c.Parent.RectangleToScreen(c.Bounds);
        }

        private static string ReadSimSerialNumber(string csvPath)
        {
            foreach (string line in File.ReadAllLines(csvPath, Encoding.Default))
            {
                if (line.StartsWith("Serial Number:"))
                    return line.Substring("Serial Number:".Length).Split(',')[0].Trim();
                if (line.StartsWith("Step,"))
                    break;
            }
            return "";
        }
    }
}
