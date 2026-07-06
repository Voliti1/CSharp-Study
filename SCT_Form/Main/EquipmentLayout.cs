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
            public long Wafer2Down;
            public long Wafer2Up;
            public long Wafer3Down;
            public long Wafer3Up;
            public long Wafer4Down;
            public long Wafer4Up;
            public long Wafer5Down;
            public long Wafer5Up;
        }

        private static readonly Dictionary<string, ModuleProfile> Modules = new Dictionary<string, ModuleProfile>
        {
            { "PM A", new ModuleProfile { LR = -59064, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 5, DoorCloseOutput = 4, DoorUpSensor = 6, DoorDownSensor = 7, LampOutput = 3 } },
            { "PM B", new ModuleProfile { LR = -190823, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 8, DoorCloseOutput = 7, DoorUpSensor = 8, DoorDownSensor = 9, LampOutput = 6 } },
            { "PM C", new ModuleProfile { LR = -322000, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 11, DoorCloseOutput = 10, DoorUpSensor = 10, DoorDownSensor = 11, LampOutput = 9 } },
        };

        // 상/하(UD) 좌표는 FOUP A/B 공통(동일 높이), 좌/우(LR) 좌표만 FOUP마다 다름.
        private static readonly Dictionary<string, FoupProfile> Foups = new Dictionary<string, FoupProfile>
        {
            { "FOUP A", new FoupProfile { LR = 13140, Wafer1Down = 100379, Wafer1Up = 302380, Wafer2Down = 781878, Wafer2Up = 982378, Wafer3Down = 1432388, Wafer3Up = 1627604, Wafer4Down = 2119399, Wafer4Up = 2332102, Wafer5Down = 2818463, Wafer5Up = 3018457 } },
            { "FOUP B", new FoupProfile { LR = -395093, Wafer1Down = 100379, Wafer1Up = 302380, Wafer2Down = 781878, Wafer2Up = 982378, Wafer3Down = 1432388, Wafer3Up = 1627604, Wafer4Down = 2119399, Wafer4Up = 2332102, Wafer5Down = 2818463, Wafer5Up = 3018457 } },
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
