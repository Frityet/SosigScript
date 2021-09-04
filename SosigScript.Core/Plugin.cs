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

			foreach (SosigScript script in ScriptLoader.LoadedScripts)
			{
				script.Options.DebugPrint = msg => Logger.LogInfo($"(SosigScript - {script.Name}) - {msg}");
			}

			Logger.LogInfo($"Loaded {ScriptLoader.LoadedScriptCount} scripts!");

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
