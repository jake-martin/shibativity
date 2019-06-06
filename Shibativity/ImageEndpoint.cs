using Shibativity.Helpers;
using System;
using System.Net;

namespace Shibativity
{
	public class ImageEndpoint
	{
		//TODO: refactor into one method
		public static string GetPictureUrl()
		{
			var randomGenerator = new Random();
			var randomInt = randomGenerator.Next(0, 4);

			var imageUrl = randomInt == 0 ? Constants.BIRD_ENDPOINT : Constants.SHIBE_ENDPOINT;

			var remoteImageUrl = string.Empty;
			using (var client = new WebClient())
			{
				remoteImageUrl = client.DownloadString(imageUrl);
			}
			//TODO replace this with regex
			remoteImageUrl = remoteImageUrl.Replace("\"", "");
			remoteImageUrl = remoteImageUrl.Replace("[", "");
			remoteImageUrl = remoteImageUrl.Replace("]", "");

			return remoteImageUrl;
		}
		public static string GetShibePicture()
		{
			var imageUrl = Constants.SHIBE_ENDPOINT;

			var remoteImageUrl = string.Empty;
			using (var client = new WebClient())
			{
				remoteImageUrl = client.DownloadString(imageUrl);
			}
			//TODO replace this with regex
			remoteImageUrl = remoteImageUrl.Replace("\"", "");
			remoteImageUrl = remoteImageUrl.Replace("[", "");
			remoteImageUrl = remoteImageUrl.Replace("]", "");

			return remoteImageUrl;
		}
	}
}
