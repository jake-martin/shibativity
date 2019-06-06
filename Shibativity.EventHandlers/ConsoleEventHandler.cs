using kCura.EventHandler;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DTOs = kCura.Relativity.Client.DTOs;

namespace Shibativity.EventHandlers
{
	[kCura.EventHandler.CustomAttributes.Description("Gets an image from shibe.online, calls Computer Vision, and updates Image object.")]
	[Guid("5BA90B02-9FB1-458D-895D-4159F996F327")]
	public class ConsoleEventHandler : kCura.EventHandler.ConsoleEventHandler
	{

		public override kCura.EventHandler.Console GetConsole(PageEvent pageEvent)
		{
			kCura.EventHandler.Console returnConsole = new kCura.EventHandler.Console() { Items = new List<IConsoleItem>(), Title = "Shiba Processor" }; ;

			//TODO: strings should be variables
			returnConsole.Items.Add(new ConsoleButton() { Name = "GetImage", DisplayText = "Get Image", Enabled = true, RaisesPostBack = true });
			returnConsole.Items.Add(new ConsoleButton() { Name = "Correct", DisplayText = "Mark as Correct", Enabled = true, RaisesPostBack = true });
			returnConsole.Items.Add(new ConsoleButton() { Name = "FalseNegative", DisplayText = "Mark as False Negative", Enabled = true, RaisesPostBack = true });
			returnConsole.Items.Add(new ConsoleButton() { Name = "FalsePositive", DisplayText = "Mark as False Positive", Enabled = true, RaisesPostBack = true });

			var viewImageButton = new ConsoleButton
			{
				Name = "ViewImage",
				DisplayText = "View Image",
				ToolTip = "View Image",
				RaisesPostBack = false,
				Enabled = true
			};

			var imageLocation = String.Empty;
			DTOs.FieldValue field;

			using (IRSAPIClient proxy = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
			{
				//Set the proxy to use the current workspace
				proxy.APIOptions.WorkspaceID = Helper.GetActiveCaseID();

				var imageArtifactId = ActiveArtifact.ArtifactID;
				var rdoToRead = proxy.Repositories.RDO.ReadSingle(imageArtifactId);
				rdoToRead.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
				field = rdoToRead.Fields.Get(Shibativity.Helpers.Constants.IMAGE_URL_FIELD_GUID);
			}

			if (field.Value == null) return returnConsole;
			imageLocation = field.Value.ToString();

			var openImageWindowJavaScript = $"window.open('{imageLocation}', '', 'location=no,scrollbars=no,menubar=no,toolbar=no,status=no,resizable=yes,width=800,height=800');";

			viewImageButton.OnClickEvent = openImageWindowJavaScript;
			returnConsole.Items.Add(viewImageButton);

			return returnConsole;
		}

		public override async void OnButtonClick(ConsoleButton consoleButton)
		{
			switch (consoleButton.Name)
			{

				case "GetImage":

					var imageUrl = ImageEndpoint.GetPictureUrl();
					var computerVisionResponse = await ComputerVision.ProcessImageAsync(imageUrl);

					using (IRSAPIClient proxy = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
					{
						//Set the proxy to use the current workspace
						proxy.APIOptions.WorkspaceID = Helper.GetActiveCaseID();
						var imageArtifactId = ActiveArtifact.ArtifactID;

						var rdoToUpdate = proxy.Repositories.RDO.ReadSingle(imageArtifactId);

						rdoToUpdate.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_URL_FIELD_GUID, imageUrl));
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_DESCRIPTION_FIELD_GUID, computerVisionResponse.Description));
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_STATUS_FIELD_GUID, computerVisionResponse.ShibaStatus));

						proxy.Repositories.RDO.Update(rdoToUpdate);
					}
					break;

				case "Correct":

					using (IRSAPIClient proxy = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
					{
						//Set the proxy to use the current workspace
						proxy.APIOptions.WorkspaceID = Helper.GetActiveCaseID();
						var imageArtifactId = ActiveArtifact.ArtifactID;

						var rdoToUpdate = proxy.Repositories.RDO.ReadSingle(imageArtifactId);

						rdoToUpdate.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_RESULT_FIELD_GUID, "Correct"));

						proxy.Repositories.RDO.Update(rdoToUpdate);
					}
					break;

				case "FalseNegative":

					using (IRSAPIClient proxy = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
					{
						//Set the proxy to use the current workspace
						proxy.APIOptions.WorkspaceID = Helper.GetActiveCaseID();
						var imageArtifactId = ActiveArtifact.ArtifactID;

						var rdoToUpdate = proxy.Repositories.RDO.ReadSingle(imageArtifactId);

						rdoToUpdate.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_RESULT_FIELD_GUID, "False Negative"));

						proxy.Repositories.RDO.Update(rdoToUpdate);
					}
					break;

				case "FalsePositive":

					using (IRSAPIClient proxy = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
					{
						//Set the proxy to use the current workspace
						proxy.APIOptions.WorkspaceID = Helper.GetActiveCaseID();
						var imageArtifactId = ActiveArtifact.ArtifactID;

						var rdoToUpdate = proxy.Repositories.RDO.ReadSingle(imageArtifactId);

						rdoToUpdate.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
						rdoToUpdate.Fields.Add(new DTOs.FieldValue(Helpers.Constants.IMAGE_RESULT_FIELD_GUID, "False Positive"));

						proxy.Repositories.RDO.Update(rdoToUpdate);
					}
					break;
			}
		}

		/// <summary>
		///     The RequiredFields property tells Relativity that your event handler needs to have access to specific fields that
		///     you return in this collection property
		///     regardless if they are on the current layout or not. These fields will be returned in the ActiveArtifact.Fields
		///     collection just like other fields that are on
		///     the current layout when the event handler is executed.
		/// </summary>
		public override FieldCollection RequiredFields
		{
			get
			{
				kCura.EventHandler.FieldCollection retVal = new kCura.EventHandler.FieldCollection();
				return retVal;
			}
		}
	}
}