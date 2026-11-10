using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _22.18.1 Load correct firmware version into the DUT
    class TestItemStep22_18_1 : SimTestItem
    {
        public TestItemStep22_18_1() : base("_22.18.1") { }
    }

    // _22.18.2 Verify programming
    class TestItemStep22_18_2 : SimTestItem
    {
        public TestItemStep22_18_2() : base("_22.18.2") { }
    }

    // _22.18.3 Check 19.5 V supply current
    class TestItemStep22_18_3 : SimTestItem
    {
        public TestItemStep22_18_3() : base("_22.18.3") { }
    }

    // _22.18.4 Check setting 1 LED is ON
    class TestItemStep22_18_4 : SimTestItem
    {
        public TestItemStep22_18_4() : base("_22.18.4") { }
    }

    // _22.18.5 Read FW version
    class TestItemStep22_18_5 : SimTestItem
    {
        public TestItemStep22_18_5() : base("_22.18.5") { }
    }

    // _22.18.6 DISCHARGE ALL CAPACITORS < 100Volt
    class TestItemStep22_18_6 : SimTestItem
    {
        public TestItemStep22_18_6() : base("_22.18.6") { }
    }

    // _22.18.7 Report to operator Pass/fail
    class TestItemStep22_18_7 : SimTestItem
    {
        public TestItemStep22_18_7() : base("_22.18.7") { }
    }

    // _22.18.8 Store test results
    class TestItemStep22_18_8 : SimTestItem
    {
        public TestItemStep22_18_8() : base("_22.18.8") { }
    }
}
