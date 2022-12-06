using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestingAssemblyLoad
{
    public class DependencyFinder
    {
        public readonly List<string> _dependencyLookUpList = new List<string>();
        public readonly List<string> _independentEntities = new List<string>();
        Utilities Utilities = new Utilities();
        public int numberOfElements;
        public void GetServicesInDepth(string typeName, List<string> NameSpaces)
        {
            Type type = Utilities.GetType(typeName, NameSpaces);
            if (type == null)
            {
                return;
            }

            var circularDependencyCheck = _dependencyLookUpList.FirstOrDefault(t => t == type.FullName);
            var count = 0;
            if (circularDependencyCheck != null) return;

            _dependencyLookUpList.Add(type.FullName);
            if (type != null && (type.Module.Name.Contains("CareStack") || type.Name.Contains("Fuze")))
            {
                ConstructorInfo[] constructorInfo = type.GetConstructors();
                if (constructorInfo.Length == 0)
                {
                    _independentEntities.Add(type.FullName);
                }
                foreach (ConstructorInfo constructor in constructorInfo)
                {
                    ParameterInfo[] Params = constructor.GetParameters();
                    foreach (ParameterInfo parameter in Params)
                    {
                        var ScopeChecker = Utilities.ScopeChecker(parameter, NameSpaces);
                        if (parameter.ParameterType.IsInterface == true && ScopeChecker == true)
                        {
                            Type typeInterface = Utilities.FindInterfaceFromAssembly(parameter.ParameterType.Name, NameSpaces);
                            var implementedClass = Utilities.GetImplementingClass(typeInterface, NameSpaces);

                            if (implementedClass != null)
                            {
                                count++;
                                GetServicesInDepth(implementedClass.Name.ToString(), NameSpaces);
                            }
                        }
                        else if (parameter.ParameterType.IsClass == true && ScopeChecker == true)
                        {
                            count++;
                            GetServicesInDepth(parameter.ParameterType.Name, NameSpaces);
                        }
                        else if (PackageValidation(parameter))
                        {
                            count++;
                        }
                        else
                        {
                            Utilities.DependencyStorer(type, parameter);

                        }
                    }
                    if (count == Params.Length)
                    {
                        _independentEntities.Add(type.FullName);
                    }
                }
            }
        }
        public List<DependentEntity> getDependencyList()
        {
            var dependentEntities = Utilities.dependentEntities;
            return dependentEntities;
        }
        public List<string> getIndependentEntities()
        {
            return _independentEntities;
        }
        private bool PackageValidation(ParameterInfo parameter)
        {
            if (parameter.ParameterType.Name == "IConfigurationService" ||
                            parameter.ParameterType.Name == "CareStackDbContext" ||
                            parameter.ParameterType.Name == "ServiceContext" ||
                            parameter.ParameterType.Name == "ShardService" ||
                            parameter.ParameterType.Name == "Int32" ||
                            parameter.ParameterType.Name == "List`1" ||
                            parameter.ParameterType.Name == "Int32[]" ||
                            parameter.ParameterType.Name == "Boolean" ||
                            parameter.ParameterType.Name == "String")
            {
                return true;
            }
            return false;
        }

    }
}
