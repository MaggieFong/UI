using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _END Time Measurement
    class TestItemEnd : SimTestItem
    {
        public TestItemEnd() : base("_END") { }
    }
}
