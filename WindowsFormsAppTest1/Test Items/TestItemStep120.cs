using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _120 P1 STS IR DAC setting 511
    class TestItemStep120_P3 : SimTestItem
    {
        public TestItemStep120_P3() : base("_120 P1") { }
    }

    // _120 P2 Measure RED LED current (in 100 Ohm)
    class TestItemStep120_P4 : SimTestItem
    {
        public TestItemStep120_P4() : base("_120 P2") { }
    }
}
