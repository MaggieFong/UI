using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _002 P1 Check Jig closed
    class TestItemStep002_P1 : SimTestItem
    {
        public TestItemStep002_P1() : base("_002 P1") { }
    }

    // _002 P2 Make sure all discharge resistors are disconnected
    class TestItemStep002_P2 : SimTestItem
    {
        public TestItemStep002_P2() : base("_002 P2") { }
    }
}
