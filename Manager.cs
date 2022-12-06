using System;
using System.Collections.Generic;

namespace TestingAssemblyLoad
{
    public class Manager
    {
        DllLoader _dllLoader;
        Utilities _Utilities;

        WriterHelper _writerHelper;
        public Manager(DllLoader dllLoader, WriterHelper writerHelper, Utilities utilities)
        {
            _dllLoader = dllLoader;
            _Utilities = utilities;
            _writerHelper = writerHelper;
        }
        public void FlowManager(string defaultPath, List<string> dllPaths, List<string> nspace)
        {
            dllPaths.Add(defaultPath + "\\CareStack.Unified\\CareStack.Backend\\CareStack.Web\\bin\\CareStack.Web.dll");
            dllPaths.Add(defaultPath + "\\CareStack.Unified\\CareStack.Backend\\CareStack.Backend.Common\\bin\\Debug\\CareStack.Backend.Core.dll");
            var x = _dllLoader.LoadAssemblies(dllPaths, nspace);
            Console.WriteLine("Enter the path to write your file");
            var filePath = Console.ReadLine();
            _writerHelper.WriteParserDependentEntities(x.Item1,filePath);
            _writerHelper.WriterParserIndependentEntities(x.Item2,filePath);
        }
    }
}
