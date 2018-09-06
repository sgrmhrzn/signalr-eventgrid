using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTestApp.Enums
{
    public enum ConnectionState
    {
        Connecting = 0,
        Connected = 1,
        Reconnecting = 2,
        Disconnected = 3
    }
}
