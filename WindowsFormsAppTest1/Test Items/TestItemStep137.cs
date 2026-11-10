using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _137 P1 Attachment EEPROM test
    class TestItemStep137_P1 : SimTestItem
    {
        public TestItemStep137_P1() : base("_137 P1") { }
    }

    // _137 P2 +3V3_switch supply disabled
    class TestItemStep137_P2 : SimTestItem
    {
        public TestItemStep137_P2() : base("_137 P2") { }
    }
}
