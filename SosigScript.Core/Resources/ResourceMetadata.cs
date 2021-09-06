using System.IO;

namespace SosigScript.Core
{
	public struct ResourceMetadata
	{
		public string Name				{ get; set; }
		public string Author			{ get; set; }
		public string Description		{ get; set; }
		public string Version			{ get; set; }

		public FileInfo File			{ get; set; }
	}
}
