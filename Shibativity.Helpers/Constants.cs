using System;

namespace Shibativity.Helpers
{
	public class Constants
	{
		//Azure
		public static readonly string COMPUTER_VISION_ENDPOINT = "https://southcentralus.api.cognitive.microsoft.com";
		public static readonly string SHIBA_PROCESSOR_KEY = "";

		//Shibe.online
		public static readonly string SHIBE_ENDPOINT = "http://shibe.online/api/shibes?count=1&urls=true&httpsUrls=false";
		public static readonly string BIRD_ENDPOINT = "http://shibe.online/api/birds?count=1&urls=true&httpsUrls=false";

		//Shibativity GUIDs
		public static readonly Guid IMAGE_OBJECT_GUID = new Guid("479E4FF2-002F-4EBF-B70C-176F8FCCFD3E");
		public static readonly Guid IMAGE_DESCRIPTION_FIELD_GUID = new Guid("3F27AEA8-3CA9-40BD-978D-FC09B402C951");
		public static readonly Guid IMAGE_URL_FIELD_GUID = new Guid("4C491163-07D8-42DD-83F3-4A1352C89163");
		public static readonly Guid IMAGE_RESULT_FIELD_GUID = new Guid("AE7B482C-B954-4DDD-A24E-8722C1BE7104");
		public static readonly Guid IMAGE_STATUS_FIELD_GUID = new Guid("F166A284-9F2E-44F4-AD25-9AFF01E14782");
		public static readonly Guid IMAGE_NAME_FIELD_GUID = new Guid("1887CCFA-DF78-4E6A-82A9-CBA0A6B22FB9");

		public static readonly int WORKSPACE_ID = 1017737;
	}
}