using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _006 P1 Measure supply voltage
    class TestItemStep006_P1 : SimTestItem
    {
        public TestItemStep006_P1() : base("_006 P1") { }
    }

    // _006 P2 19.5 V supply current
    class TestItemStep006_P2 : SimTestItem
    {
        public TestItemStep006_P2() : base("_006 P2") { }
    }
}
