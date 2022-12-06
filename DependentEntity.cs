using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssemblyLoad
{
    public class DependentEntity
    {
        public string EntityName { get; set; }
        public List<string> EntityDependencies = new List<string>();
    }
}
