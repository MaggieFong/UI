using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _130 P1 STS IR DAC setting 511
    class TestItemStep130_P3 : SimTestItem
    {
        public TestItemStep130_P3() : base("_130 P1") { }
    }

    // _130 P2 Measure IR LED current (in 150 Ohm)
    class TestItemStep130_P4 : SimTestItem
    {
        public TestItemStep130_P4() : base("_130 P2") { }
    }
}
