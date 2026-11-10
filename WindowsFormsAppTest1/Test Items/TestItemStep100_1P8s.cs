using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _100 P1 Charge time flashcap
    class TestItemStep100_P1 : SimTestItem
    {
        public TestItemStep100_P1() : base("_100 P1") { }
    }

    // _100 P2 Flash cap voltage End Value
    class TestItemStep100_P2 : SimTestItem
    {
        public TestItemStep100_P2() : base("_100 P2") { }
    }

    // _100 P3 Flash cap voltage max Value
    class TestItemStep100_P3 : SimTestItem
    {
        public TestItemStep100_P3() : base("_100 P3") { }
    }

    // _100 P4 Flash cap voltage NI
    class TestItemStep100_P4 : SimTestItem
    {
        public TestItemStep100_P4() : base("_100 P4") { }
    }

    // _100 P5 Lamp voltage max value
    class TestItemStep100_P5 : SimTestItem
    {
        public TestItemStep100_P5() : base("_100 P5") { }
    }

    // _100 P6 VFC_BeginStopCharging
    class TestItemStep100_P6 : SimTestItem
    {
        public TestItemStep100_P6() : base("_100 P6") { }
    }

    // _100 P7 VFC_difference begin end
    class TestItemStep100_P7 : SimTestItem
    {
        public TestItemStep100_P7() : base("_100 P7") { }
    }

    // _100 P8 Vlamp_BeginStopCharging
    class TestItemStep100_P8 : SimTestItem
    {
        public TestItemStep100_P8() : base("_100 P8") { }
    }

    // _100 P13 NI Vlamp Pos conv off
    class TestItemStep100_P13 : SimTestItem
    {
        public TestItemStep100_P13() : base("_100 P13") { }
    }

    // _100 P9 Vlamp_EndStopCharging
    class TestItemStep100_P9 : SimTestItem
    {
        public TestItemStep100_P9() : base("_100 P9") { }
    }

    // _100 P10 Vlamp Pos NI
    class TestItemStep100_P10 : SimTestItem
    {
        public TestItemStep100_P10() : base("_100 P10") { }
    }

    // _100 P11 Vlamp Neg NI
    class TestItemStep100_P11 : SimTestItem
    {
        public TestItemStep100_P11() : base("_100 P11") { }
    }

    // _100 P12 IGBT active
    class TestItemStep100_P12 : SimTestItem
    {
        public TestItemStep100_P12() : base("_100 P12") { }
    }
}
