using BepInEx;
using SosigScript;

namespace SosigScript.ExampleLibrary
{
    [BepInPlugin("net.frityet.examplesslib", "Example SosigScript Library", "1.0.0")]
    [BepInDependency("net.frityet.sosigscript", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public struct ExampleStruct
        {
            public string String { get; set; }
            public float Float { get; set; }
        }

        public Plugin()
        {
            var lib = new Library();
            lib.RegisterObject("TestObject", new { Value = 94f });
            lib.RegisterObject("TestObject2", new ExampleStruct() { String = "str", Float = 95f });
        }
    }
}
