using BepInEx;
using BepInEx.Logging;
using SosigScript.Common;

namespace SosigScript
{
	[BepInPlugin("net.frityet.sosigscript", "SosigScript", "1.0.0")]
	public sealed class Plugin : BaseUnityPlugin
	{
		private const string PLUGINS_DIR = "BepInEx/Plugins";

		public new static ManualLogSource? Logger { get; private set; }

		public static event Delegates.UnityEventDelegate? OnAwake;
		public static event Delegates.UnityEventDelegate? OnStart;
		public static event Delegates.UnityEventDelegate? OnUpdate;
		public static event Delegates.UnityEventDelegate? OnFixedUpdate;

		public Plugin()
		{
			base.Logger.LogInfo("Started SosigScript");
		}

		private void Awake()
		{
			Logger = base.Logger;

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
	}
}
