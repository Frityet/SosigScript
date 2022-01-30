using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public class ScriptLoader : ResourceLoader<SosigScript>
    {
        private const string SEARCH_DIRECTORY = "BepInEx/plugins";
        private const string SCRIPT_EXTENSION = "lua";

        public ScriptLoader()
        {
            var searchdir = new DirectoryInfo(SEARCH_DIRECTORY);

            foreach (DirectoryInfo dir in searchdir.GetDirectories())
                foreach (FileInfo file in dir.GetFiles())
                    if (file.Extension == SCRIPT_EXTENSION)
                        LoadedResources.Add(new SosigScript(file.FullName));
        }

        public void LoadScript(string path) => LoadedResources.Add(new SosigScript(path));

        public void RunScripts()
        {
            foreach (SosigScript script in LoadedResources)
            {
                script.ExecuteFunction("start");
                Plugin.OnUpdate         += () => script.ExecuteFunction("update");
                Plugin.OnFixedUpdate    += () => script.ExecuteFunction("fixedupdate");
            }
        }
    }
}
