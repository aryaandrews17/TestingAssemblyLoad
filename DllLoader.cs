using System;
using System.Collections.Generic;

namespace TestingAssemblyLoad
{
    public class DllLoader
    {
        readonly Utilities utilities = new Utilities();
        readonly DependencyFinder dependencyFinder = new DependencyFinder();
        public (List<DependentEntity>, List<string>) LoadAssemblies(List<string> paths, List<string> nspace)
        {
            foreach (string DllPath in paths)
            {
                for (int j = 0; j < nspace.Count; j++)
                {
                    var types = utilities.GetAllTypesFromDllPath(DllPath, nspace[j]);
                    if (types != null)
                    {
                        foreach (Type type in types)
                        {
                            dependencyFinder.GetServicesInDepth(type.Name, nspace);
                        }
                    }
                    else
                        Console.WriteLine("The assembly did not return any types for the given namespace");
                }
            }
            List<DependentEntity> dependentEntities = dependencyFinder.getDependencyList();
            List<string> independentEntities = dependencyFinder.getIndependentEntities();
            return (dependentEntities, independentEntities);

        }


    }
}
