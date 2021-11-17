using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SosigScript
{
    public class ScriptLoader : ResourceLoader<SosigScript>
    {
        private const string SEARCH_DIRECTORY = "BepInEx/plugins";
        private const string SCRIPT_EXTENTION = "sslua";

        public ScriptLoader() : base()
        {
            var searchdir = new DirectoryInfo(SEARCH_DIRECTORY);

            foreach (DirectoryInfo dir in searchdir.GetDirectories())
                foreach (FileInfo file in dir.GetFiles())
                    if (file.Extension == SCRIPT_EXTENTION)
                        LoadedResources.Add(new SosigScript(file.FullName));

            Plugin.OnStart += () =>
            {
                foreach (SosigScript script in LoadedResources)
                {
                    script.ExecuteFunction("Start");
                }
            };

            Plugin.OnUpdate += () =>
            {
                foreach (SosigScript script in LoadedResources)
                {
                    script.ExecuteFunction("Update");
                }
            };

            Plugin.OnFixedUpdate += () =>
            {
                foreach (SosigScript script in LoadedResources)
                {
                    script.ExecuteFunction("FixedUpdate");
                }
            };
        }

        public void LoadScript(string path) => LoadedResources.Add(new SosigScript(path));
    }
}
