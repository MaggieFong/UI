using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _085 P1 PCBA temperature sensor (air out)
    class TestItemStep085_P1 : SimTestItem
    {
        public TestItemStep085_P1() : base("_085 P1") { }
    }

    // _085 P2 Check air out temperature sensor NTC
    class TestItemStep085_P2 : SimTestItem
    {
        public TestItemStep085_P2() : base("_085 P2") { }
    }

    // _085 P3 Compare software(P1) and instrument(P2) values
    class TestItemStep085_P3 : SimTestItem
    {
        public TestItemStep085_P3() : base("_085 P3") { }
    }

    // _085 P4 PCBA temperature sensor (Lampframe_TC)
    class TestItemStep085_P4 : SimTestItem
    {
        public TestItemStep085_P4() : base("_085 P4") { }
    }

    // _085 P5 Check Thermal cutout temperature sensor NTC
    class TestItemStep085_P5 : SimTestItem
    {
        public TestItemStep085_P5() : base("_085 P5") { }
    }

    // _085 P6 Compare software(P4) and instrument(P5) values
    class TestItemStep085_P6 : SimTestItem
    {
        public TestItemStep085_P6() : base("_085 P6") { }
    }

    // _085 P7 Measure Q203-E voltage with thermal cutoff triggered
    class TestItemStep085_P7 : SimTestItem
    {
        public TestItemStep085_P7() : base("_085 P7") { }
    }

    // _085 P8 Measure Q203-E voltage with relay opened
    class TestItemStep085_P8 : SimTestItem
    {
        public TestItemStep085_P8() : base("_085 P8") { }
    }
}
