using BepInEx;
using SosigScript.Common;
using SosigScript.Libraries;

namespace SosigScript
{
	public sealed partial class Plugin : BaseUnityPlugin
	{
		public static ScriptLoader? ScriptLoader { get; private set; }
		public static LibraryLoader? LibraryLoader { get; private set; }

		private const string PLUGINS_DIR = "BepInEx/Plugins";

		public static event Delegates.UnityEventDelegate OnAwake;
		public static event Delegates.UnityEventDelegate OnStart;
		public static event Delegates.UnityEventDelegate OnUpdate;
		public static event Delegates.UnityEventDelegate OnFixedUpdate;

		public Plugin()
		{
			Logger.LogInfo("Started SosigScript");

			ScriptLoader = new ScriptLoader(PLUGINS_DIR, "sslua");
			LibraryLoader = new LibraryLoader();
		}

		private void Awake()
		{

			foreach (var script in ScriptLoader!.LoadedResources)
			{
				script.Value.Options.DebugPrint = msg => Logger.LogInfo($"(SosigScript - {script.Key.Name}) - {msg}");
			}

			Logger.LogInfo($"Loaded {ScriptLoader.LoadedResourceCount} scripts!");

			OnAwake.Invoke();
		}

		private void Start()
		{
			OnStart.Invoke();
		}

		private void Update()
		{
			OnUpdate.Invoke();
		}

		private void FixedUpdate()
		{
			OnFixedUpdate.Invoke();
		}
	}
}
