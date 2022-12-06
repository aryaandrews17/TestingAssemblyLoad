using System;
using System.Collections.Generic;

namespace TestingAssemblyLoad
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string path;
            string input;
            List<string> NameSpacesList = new List<string>();
            List<string> DllPathsList = new List<string>();
            Utilities utilities = new Utilities();
            WriterHelper helper = new WriterHelper();
            DllLoader dllLoader = new DllLoader();
            Manager manager = new Manager(dllLoader, helper, utilities);
            string title = "Dependency Finder Tool";
            Console.SetCursorPosition(Console.WindowWidth / 2 - title.Length / 2, Console.WindowHeight/20);
            Console.WriteLine( title , Console.Title);
            Console.WriteLine();
           
            do
            {
                
               
                
                Console.WriteLine("Enter the path to your local CodeBase folder (eg:   D:\\Carestack or C:\\Carestack):");
             
                path = Console.ReadLine();
                Console.WriteLine();
                if (utilities.DllValidation(path))
                {
                    break;
                }
                Console.WriteLine("The path you entered is wrong");
            } while (utilities.DllValidation(path) == false);
            Console.WriteLine();
            Console.WriteLine("By default CareStack.Web.dll and CareStack.Backend.Core.dll are loaded from your given path.");
            Console.WriteLine();
            do
            {
                Console.WriteLine("Do you want add more paths to your dll (Y/N):");
               
                input = Console.ReadLine();
                Console.WriteLine();
                if (input == "N" || input == "n")
                    break;
                Console.WriteLine("Enter a path to your DLL file");
               
                string dllPath = Console.ReadLine();
                Console.WriteLine();
                DllPathsList.Add(dllPath);
            } while (input == "Y" || input == "y");
            do
            {
                Console.WriteLine("Enter the namespaces you want to set as boundary scope for your search (It should be of the format 'Billing' or 'Clinical' (note this is case sensitive)n): ");
                
                string nameSpace = Console.ReadLine();
                Console.WriteLine();
                NameSpacesList.Add("." + nameSpace);
                Console.WriteLine("Do you want to add more namespaces (Y/N): ");
                input = Console.ReadLine();
                Console.WriteLine();

                if (input == "N" || input == "n")
                    break;
            } while (input == "Y" || input == "y");

            manager.FlowManager(path, DllPathsList, NameSpacesList);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The dependencies have been written into files. You can find your folder at ");
            Console.WriteLine("Press any key to exit the tool");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
