using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dywham.Breeze.Fabric.Utils.Bcl
{
    public static class AssemblyUtils
    {
        public static Assembly[] GetRuntimeAssemblies()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories).ToList();
            var assemblies = new HashSet<Assembly>();

            foreach (var file in files)
            {
                if (string.IsNullOrEmpty(Path.GetFileName(file)))
                {
                    continue;
                }

                assemblies.Add(Assembly.LoadFrom(file));
            }

            assemblies.Add(Assembly.GetEntryAssembly());

            return assemblies.ToArray();
        }
    }
}