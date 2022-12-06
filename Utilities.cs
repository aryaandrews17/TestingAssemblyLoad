using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace TestingAssemblyLoad
{
    public class Utilities
    {
        public List<DependentEntity> dependentEntities = new List<DependentEntity>();
        public List<DependentEntity> DependencyStorer(Type type, ParameterInfo parameter)
        {
            var element = dependentEntities.FirstOrDefault(x => x.EntityName == type.Name);
            if (element != null)
            {
                element.EntityDependencies.Add(parameter.ParameterType.Name);
            }
            else
            {
                DependentEntity dependentEntity = new DependentEntity();
                dependentEntity.EntityName = type.Name;
                dependentEntity.EntityDependencies.Add(parameter.ParameterType.Name);
                dependentEntities.Add(dependentEntity);
            }
            return dependentEntities;
        }

        public bool DllValidation(string path)
        {
            var types = GetAllTypesFromDllPath(path + "\\CareStack.Unified\\CareStack.Backend\\CareStack.Web\\bin\\CareStack.Web.dll", ".Carestack");
            if (types != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Type> GetAllTypesFromDllPath(string path, string nspace)
        {
            IEnumerable<Type> types = null;
            try
            {
                types = from t in Assembly.LoadFrom(path).GetTypes()
                        where (t.FullName.Contains(nspace)) && (t.IsClass || t.IsInterface) && (!t.IsNested)// &&(t.BaseType.FullName != "System.Object")
                        select t;
            }
            catch
            {
                return types;
            }
            return types;
        }

        public Type GetType(string typeName, List<string> NameSpaces)
        {
            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from t in assembly.GetTypes()
                         where t.Name == typeName
                         select t);
            foreach (var i in NameSpaces)
            {
                foreach (var t in types)
                {
                    if (t.Namespace.Contains(i))
                    {
                        return t;
                    }
                }
            }
            return null;
        }

        public Type FindInterfaceFromAssembly(string typeName, List<string> NameSpaces)
        {
            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from t in assembly.GetTypes()
                         where t.Name == typeName
                         select t);
            foreach (var i in NameSpaces)
            {
                foreach (var t in types)
                {
                    if (t.Namespace.Contains(i))
                    {
                        return t;
                    }
                }
            }
            return null;
        }

        public Type GetImplementingClass(Type interfaces, List<string> NameSpaces)
        {
            var implementedClasses = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                      from t in assembly.GetTypes()
                                      where interfaces.IsAssignableFrom(t) && t.IsClass == true
                                      select t);
            foreach (var i in implementedClasses)
            {
                foreach (var j in NameSpaces)
                {
                    if (i.Namespace.Contains(j))
                    {
                        return i;
                    }
                }
            }
            return null;
        }

        public bool ScopeChecker(ParameterInfo parameter, List<string> NameSpaces)
        {
            var check = (from t in NameSpaces
                         where parameter.ParameterType.Namespace.Contains(t)
                         select t).FirstOrDefault();
            if (check != null)
                return true;
            return false;
        }
    }
}
