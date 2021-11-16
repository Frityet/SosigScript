using BepInEx;
using BepInEx.Logging;
using SosigScript.Common;
using SosigScript.Libraries;

namespace SosigScript
{
	public sealed partial class Plugin : BaseUnityPlugin
	{
		private const string PLUGINS_DIR = "BepInEx/Plugins";

		private BaseLibrary _baseLib;

		public static ScriptLoader?  ScriptLoader  { get; private set; }
		public static LibraryLoader? LibraryLoader { get; private set; }

		public new static ManualLogSource? Logger { get; private set; }

		public Plugin()
		{
			base.Logger.LogInfo("Started SosigScript");

			ScriptLoader = new ScriptLoader(PLUGINS_DIR, "sslua");
			LibraryLoader = new LibraryLoader();
			_baseLib = new BaseLibrary();
		}

		private void Awake()
		{
			Logger = base.Logger;

			foreach (var script in ScriptLoader!.LoadedResources)
				script.Value.Options.DebugPrint = msg => Logger.LogInfo($"(SosigScript - {script.Key.Name}) - {msg}");

			Logger.LogInfo($"Loaded {ScriptLoader.LoadedResources.Count} scripts!");

			OnAwake!.Invoke();
		}

		private void Start()
		{
			OnStart!.Invoke();
		}

		private void Update()
		{
			OnUpdate!.Invoke();
		}

		private void FixedUpdate()
		{
			OnFixedUpdate!.Invoke();
		}

		public static event Delegates.UnityEventDelegate? OnAwake;
		public static event Delegates.UnityEventDelegate? OnStart;
		public static event Delegates.UnityEventDelegate? OnUpdate;
		public static event Delegates.UnityEventDelegate? OnFixedUpdate;
	}
}
