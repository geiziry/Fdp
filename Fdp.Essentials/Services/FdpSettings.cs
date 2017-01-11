using Fdp.InfraStructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Fdp.Essentials.Services
{
    public class FdpSettings : IFdpSettings
    {
        public string ConnectionString { get; set; }

    }
}