using System.Collections.Generic;

namespace TestingAssemblyLoad
{
    public class DependentEntity
    {
        public string EntityName { get; set; }
        public List<string> EntityDependencies = new List<string>();
    }
}
