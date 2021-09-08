using System.IO;

namespace SosigScript.Resources
{
	public struct ResourceMetadata
	{
		public string Name				{ get; set; }
		public string GUID				{ get; set; }
		public string Author			{ get; set; }
		public string? Description		{ get; set; }
		public string Version			{ get; set; }
		public string[]? Dependencies	{ get; set; }
		internal FileInfo File			{ get; set; }
	}
}
