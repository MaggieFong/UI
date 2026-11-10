using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _007 P1 Step2-program HW binary
    class TestItemStep007_P1 : SimTestItem
    {
        public TestItemStep007_P1() : base("_007 P1") { }
    }

    // _007 P2 Measure 3V3 voltage
    class TestItemStep007_P2 : SimTestItem
    {
        public TestItemStep007_P2() : base("_007 P2") { }
    }
}
