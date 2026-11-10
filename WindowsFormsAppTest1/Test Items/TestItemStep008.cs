using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _008 P1 Read version number
    class TestItemStep008_P1 : SimTestItem
    {
        public TestItemStep008_P1() : base("_008 P1") { }
    }
}
