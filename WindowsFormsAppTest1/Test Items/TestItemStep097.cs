using System;

namespace WindowsFormsAppTest1.TestItem
{
    // Simulation mode: no instrument is controlled.
    // sMeasured = column C of the simulation CSV (see SimTestItem.cs), spec from Spec.ini.

    // _097 Discharge flashcap; set initial condition
    class TestItemStep097 : SimTestItem
    {
        public TestItemStep097() : base("_097") { }
    }
}
