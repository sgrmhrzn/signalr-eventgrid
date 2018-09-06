using System;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly testAssembly = Assembly.LoadFile(@"c:\SignalRCore.dll");
            Type hub = testAssembly.GetType("SignalRChat.Hubs.NotificationHub");
            object instance = Activator.CreateInstance(hub);
            MethodInfo hubMethod = hub.GetMethod("SendMessage");
            hubMethod.Invoke(instance, new[] { "hello" });
        }
    }
}
