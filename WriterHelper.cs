using System.Collections.Generic;

namespace TestingAssemblyLoad
{
    public class WriterHelper
    {
        readonly WriteManager writeManager = new WriteManager();
        public void WriteParserDependentEntities(List<DependentEntity> dependentEntities,string filePath)
        {
            foreach (DependentEntity i in dependentEntities)
            {
                writeManager.WriteDependentServicesToFile(i.EntityName + " is dependent due to the following: ", filePath);
                foreach (var j in i.EntityDependencies)
                {
                    writeManager.WriteDependentServicesToFile("* " + j, filePath);
                }
            }
        }

        public void WriterParserIndependentEntities(List<string> independentEntities, string filePath)
        {
            foreach (var i in independentEntities)
            {
                writeManager.WriteIndependentServicesToFile(i, filePath);
            }
        }

    }
}
