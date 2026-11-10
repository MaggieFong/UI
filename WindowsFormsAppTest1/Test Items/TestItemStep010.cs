using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _010 P1 Initialization of uC - Pin definitions
    class TestItemStep010_P1 : SimTestItem
    {
        public TestItemStep010_P1() : base("_010 P1") { }
    }

    // _010 P2 Initialization of uC - Configuration of the pins
    class TestItemStep010_P2 : SimTestItem
    {
        public TestItemStep010_P2() : base("_010 P2") { }
    }

    // _010 P3 Initialization of uC - Initialization of the outputs
    class TestItemStep010_P3 : SimTestItem
    {
        public TestItemStep010_P3() : base("_010 P3") { }
    }
}
