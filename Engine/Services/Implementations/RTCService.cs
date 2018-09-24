using Engine.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Services.Implementations
{
    public class RTCService : IRTCService
    {
        public string get()
        {
            return "hello world";
        }
    }
}
