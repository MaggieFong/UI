using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _135 P1 Attachment temperature sensor
    class TestItemStep135_P1 : SimTestItem
    {
        public TestItemStep135_P1() : base("_135 P1") { }
    }

    // _135 P2 Check SCA_temp sensor NTC
    class TestItemStep135_P2 : SimTestItem
    {
        public TestItemStep135_P2() : base("_135 P2") { }
    }

    // _135 P3 Compare SCA_temp
    class TestItemStep135_P3 : SimTestItem
    {
        public TestItemStep135_P3() : base("_135 P3") { }
    }

    // _135 P4 Attachment phototransistor
    class TestItemStep135_P4 : SimTestItem
    {
        public TestItemStep135_P4() : base("_135 P4") { }
    }

    // _135 P5 Check SCA_sensor Phototransistor
    class TestItemStep135_P5 : SimTestItem
    {
        public TestItemStep135_P5() : base("_135 P5") { }
    }

    // _135 P6 Compare SCA_temp(P2) & SCA_sensor(P4)
    class TestItemStep135_P6 : SimTestItem
    {
        public TestItemStep135_P6() : base("_135 P6") { }
    }
}
