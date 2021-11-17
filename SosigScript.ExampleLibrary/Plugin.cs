using BepInEx;
using SosigScript;

namespace SosigScript.ExampleLibrary
{
    [BepInPlugin("net.frityet.examplesslib", "Example SosigScript Library", "1.0.0")]
    [BepInDependency("net.frityet.sosigscript", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {


        public Plugin()
        {
            var lib = new Library();
            lib.LoadedObjects.Add("TestObject", new { Value = 94f });
        }
    }
}
