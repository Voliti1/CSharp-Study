using System;
using System.Collections.Generic;

namespace SCT_Form
{
    internal static class EquipmentLayout
    {
        internal class ModuleProfile
        {
            public long LR;
            public long UDDown;
            public long UDUp;
            public int DoorOpenOutput;
            public int DoorCloseOutput;
            public int DoorUpSensor;
            public int DoorDownSensor;
            public int LampOutput;
        }

        internal class FoupProfile
        {
            public long LR;
            public long Wafer1Down;
            public long Wafer1Up;
        }

        private static readonly Dictionary<string, ModuleProfile> Modules = new Dictionary<string, ModuleProfile>
        {
            { "PM A", new ModuleProfile { LR = -59064, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 5, DoorCloseOutput = 4, DoorUpSensor = 6, DoorDownSensor = 7, LampOutput = 3 } },
            { "PM B", new ModuleProfile { LR = -190823, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 8, DoorCloseOutput = 7, DoorUpSensor = 8, DoorDownSensor = 9, LampOutput = 6 } },
            { "PM C", new ModuleProfile { LR = -322000, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 11, DoorCloseOutput = 10, DoorUpSensor = 10, DoorDownSensor = 11, LampOutput = 9 } },
        };

        private static readonly Dictionary<string, FoupProfile> Foups = new Dictionary<string, FoupProfile>
        {
            { "FOUP A", new FoupProfile { LR = 13140, Wafer1Down = 100379, Wafer1Up = 302380 } },
            { "FOUP B", new FoupProfile { LR = -395093, Wafer1Down = 100379, Wafer1Up = 302380 } },
        };

        internal static string NormalizeModule(string module)
        {
            if (string.Equals(module, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(module, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
        }

        internal static ModuleProfile GetModule(string module)
        {
            return Modules[NormalizeModule(module)];
        }

        internal static FoupProfile GetFoup(string foup)
        {
            return Foups[foup];
        }
    }
}
