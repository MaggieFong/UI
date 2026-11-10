using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _082 P1 Check Vfan+
    class TestItemStep082_P1 : SimTestItem
    {
        public TestItemStep082_P1() : base("_082 P1") { }
    }

    // _082 P2 Check Vfan- low
    class TestItemStep082_P2 : SimTestItem
    {
        public TestItemStep082_P2() : base("_082 P2") { }
    }

    // _082 P3 Check Vfan- high
    class TestItemStep082_P3 : SimTestItem
    {
        public TestItemStep082_P3() : base("_082 P3") { }
    }
}
