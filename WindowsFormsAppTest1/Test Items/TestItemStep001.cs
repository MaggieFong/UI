using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _001
    class TestItemStep001 : SimTestItem
    {
        public TestItemStep001() : base("_001") { }
    }
}
