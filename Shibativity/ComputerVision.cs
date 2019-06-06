using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Shibativity.Helpers;
using Shibativity.Helpers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shibativity
{
	public class ComputerVision
	{
		private static readonly List<VisualFeatureTypes> Features =
			new List<VisualFeatureTypes>()
			{
				VisualFeatureTypes.Categories, VisualFeatureTypes.Description, VisualFeatureTypes.ImageType, VisualFeatureTypes.Tags
			};

		public static async Task<ShibaProcessorResponseModel> ProcessImageAsync(string imageUrl)
		{
			ComputerVisionClient computerVision = new ComputerVisionClient(
				new ApiKeyServiceClientCredentials(Constants.SHIBA_PROCESSOR_KEY),
				new System.Net.Http.DelegatingHandler[] { });

			computerVision.Endpoint = Constants.COMPUTER_VISION_ENDPOINT;

			ImageAnalysis analysis = await computerVision.AnalyzeImageAsync(imageUrl, Features);

			bool shibe = analysis.Description.Captions[0].Text.Contains("dog");

			var shibeProcessorResponse = new ShibaProcessorResponseModel()
			{
				Description = analysis.Description.Captions[0].Text,
				ShibaStatus = shibe
			};
			return shibeProcessorResponse;
		}
	}
}
