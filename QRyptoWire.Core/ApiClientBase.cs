using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using QRyptoWire.Core.Messages;
using QRyptoWire.Shared;
using RestSharp.Portable;

namespace QRyptoWire.Core
{
	public abstract class ApiClientBase
	{
		private readonly IMvxMessenger _messenger;

		protected ApiClientBase(IMvxMessenger messenger)
		{
			_messenger = messenger;
		}

		private const string SessionExpiredMessageText = "Your session has expired! Please close the app and log in again";

		private const string RequestFailedMessageText =
			"Request to service failed! Check if you have stable internet connection and try again";

		protected void Execute(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			client.IgnoreResponseStatusCode = true;
			try
			{
				var response = Task.Run(async () => await client.Execute(request));
				response.Wait();

				if (!response.Result.IsSuccess)
				{
					_messenger.Publish(response.Result.StatusCode == HttpStatusCode.Unauthorized
						? new RequestFailedMessage(this, SessionExpiredMessageText)
						: new RequestFailedMessage(this, RequestFailedMessageText));
				}
			}
			catch (Exception ex)
			{
				//_messenger.Publish(new RequestFailedMessage(this, RequestFailedMessageText));
			}
		}

		protected TRet Execute<TRet>(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			client.IgnoreResponseStatusCode = true;
			try
			{
				var response = Task.Run(async () => await client.Execute<TRet>(request));
				response.Wait();

				if (response.Result.IsSuccess)
					return response.Result.Data;
				else
				{
					_messenger.Publish(response.Result.StatusCode == HttpStatusCode.Unauthorized
						? new RequestFailedMessage(this, SessionExpiredMessageText)
						: new RequestFailedMessage(this, RequestFailedMessageText));
				}
			}
			catch (Exception ex)
			{
				//_messenger.Publish(new RequestFailedMessage(this, RequestFailedMessageText));
			}

			return default(TRet);
		}

		protected bool TryExecute(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			client.IgnoreResponseStatusCode = true;
			try
			{
				var response = Task.Run(async () => await client.Execute(request));
				response.Wait();

				if (response.Result.IsSuccess)
					return true;
				else
				{
					_messenger.Publish(response.Result.StatusCode == HttpStatusCode.Unauthorized
						? new RequestFailedMessage(this, SessionExpiredMessageText)
						: new RequestFailedMessage(this, RequestFailedMessageText));
				}
			}
			catch (Exception ex)
			{
				//_messenger.Publish(new RequestFailedMessage(this, RequestFailedMessageText));
			}

			return false;
		}
	}
}
