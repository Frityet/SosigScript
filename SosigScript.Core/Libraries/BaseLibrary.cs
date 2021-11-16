using SosigScript.Resources;

namespace SosigScript.Libraries
{
	public class BaseLibrary
	{
		public BaseLibrary()
		{
			Library = new SosigScriptLibrary
			(
				new ResourceMetadata
				{
					Name = "Base Library",
					GUID = "sosigscript.baselib",
					Author = "Frityet",
					Dependencies = null,
					Version = "1.0.0"
				}
			);

			Library.RegisterObject("SosigScriptInfo", new {Version = Plugin.VERSION});
		}

		public SosigScriptLibrary Library { get; }
	}
}
