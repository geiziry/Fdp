using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.InfraStructure
{
    public static class Helpers
    {
        public static Type GetTypeFromAssembly(this string AssemblyName,string Type)
        {
            Assembly assembly = Assembly.LoadFrom(AssemblyName);
            return assembly.GetType(Type);
        }
    }
}
