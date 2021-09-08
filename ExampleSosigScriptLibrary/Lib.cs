using SosigScript.Libraries;
using SosigScript.Resources;

namespace ExampleSosigScriptLibrary
{
	public class ExampleLib
	{
		private SosigScriptLibrary Library;

		public void Test()
		{
			Library = new SosigScriptLibrary
			(
				new ResourceMetadata
				{
					Name = "Test Library",
					GUID = "sosigscript.testlib",
					Author = "Frityet",
					Dependencies = null,
					Version = "1.0.0"
				}
			);

			var name = "Frityet";

			Library.RegisterObject("Name", name);
		}
	}
}
