using System;
using BepInEx;
using MoonSharp.Interpreter;
using SosigScript.Common;

namespace SosigScript
{
	public sealed partial class Plugin : BaseUnityPlugin
	{
		public static ScriptLoader ScriptLoader { get; private set; }

		private const string PLUGINS_DIR = "BepInEx/Plugins";

		public Plugin()
		{
			Logger.LogInfo("Started SosigScript");
		}

		private void Awake()
		{
			ScriptLoader = new ScriptLoader(PLUGINS_DIR, "sslua");

			foreach (var script in ScriptLoader.LoadedResources)
			{
				script.Value.Options.DebugPrint = msg => Logger.LogInfo($"(SosigScript - {script.Key.Name}) - {msg}");
			}

			Logger.LogInfo($"Loaded {ScriptLoader.LoadedResourceCount} scripts!");

			ScriptLoader.Execute("Awake");
		}

		private void Start()
		{
			ScriptLoader.Execute("Start");
		}

		private void Update()
		{
			ScriptLoader.Execute("Update");
		}

		private void FixedUpdate()
		{
			ScriptLoader.Execute("FixedUpdate");
		}
	}
}
