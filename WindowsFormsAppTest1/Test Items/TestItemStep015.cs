using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _015 P1 Measure 3V3 current
    class TestItemStep015_P1 : SimTestItem
    {
        public TestItemStep015_P1() : base("_015 P1") { }
    }

    // _015 P2 Measure 12V voltage
    class TestItemStep015_P2 : SimTestItem
    {
        public TestItemStep015_P2() : base("_015 P2") { }
    }

    // _015 P3 Check disabled 12V voltage
    class TestItemStep015_P3 : SimTestItem
    {
        public TestItemStep015_P3() : base("_015 P3") { }
    }

    // _015 P4 Check Vmain voltage
    class TestItemStep015_P4 : SimTestItem
    {
        public TestItemStep015_P4() : base("_015 P4") { }
    }

    // _015 P5 12V voltage tristate
    class TestItemStep015_P5 : SimTestItem
    {
        public TestItemStep015_P5() : base("_015 P5") { }
    }

    // _015 P6 Check driver support voltage
    class TestItemStep015_P6 : SimTestItem
    {
        public TestItemStep015_P6() : base("_015 P6") { }
    }

    // _015 P7 Check D309
    class TestItemStep015_P7 : SimTestItem
    {
        public TestItemStep015_P7() : base("_015 P7") { }
    }
}
