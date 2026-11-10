using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _020 P1 Push button - UIButtonON
    class TestItemStep020_P1 : SimTestItem
    {
        public TestItemStep020_P1() : base("_020 P1") { }
    }

    // _020 P2 Push button - UIButtonFlash
    class TestItemStep020_P2 : SimTestItem
    {
        public TestItemStep020_P2() : base("_020 P2") { }
    }
}
