using kCura.Agent;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;
using System;

namespace Shibativity.Agent
{
	[kCura.Agent.CustomAttributes.Name("Create Image Objects")]
	[System.Runtime.InteropServices.Guid("03a71aa3-4322-4c8b-9b10-7596cf2260d0")]
	public class Relativity_Agent : AgentBase
	{
		/// <summary>
		/// Agent logic goes here
		/// </summary>
		public override void Execute()
		{
			IAPILog logger = Helper.GetLoggerFactory().GetLogger();

			try
			{
				//Get a dbContext for the workspace database
				int workspaceArtifactId = Helpers.Constants.WORKSPACE_ID;

				//Setting up an RSAPI Client
				using (IRSAPIClient rsapiClient = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
				{
					//Set the proxy to use the current workspace
					rsapiClient.APIOptions.WorkspaceID = workspaceArtifactId;

					var counter = 0;
					while (counter < 10)
					{
						var imageRDO = new RDO();
						imageRDO.ArtifactTypeGuids.Add(Helpers.Constants.IMAGE_OBJECT_GUID);
						imageRDO.Fields.Add(new FieldValue(Helpers.Constants.IMAGE_NAME_FIELD_GUID, DateTime.Now.ToLongTimeString() + " " + counter.ToString()));
						rsapiClient.Repositories.RDO.Create(imageRDO);
						counter++;
					}
				}

				logger.LogVerbose("Log information throughout execution.");
			}
			catch (Exception ex)
			{
				//Your Agent caught an exception
				logger.LogError(ex, "There was an exception.");
				RaiseError(ex.Message, ex.Message);
			}
		}

		/// <summary>
		/// Returns the name of agent
		/// </summary>
		public override string Name
		{
			get
			{
				return "Create Image Objects";
			}
		}
	}
}