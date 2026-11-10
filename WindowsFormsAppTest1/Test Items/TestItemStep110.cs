using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _110 P1 Discharge Resistor
    class TestItemStep110_P1 : SimTestItem
    {
        public TestItemStep110_P1() : base("_110 P1") { }
    }

    // _110 P2 Discharger emitter current
    class TestItemStep110_P2 : SimTestItem
    {
        public TestItemStep110_P2() : base("_110 P2") { }
    }

    // _110 P3 Discharge Resistor
    class TestItemStep110_P3 : SimTestItem
    {
        public TestItemStep110_P3() : base("_110 P3") { }
    }
}
