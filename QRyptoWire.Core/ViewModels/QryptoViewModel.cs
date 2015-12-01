using System;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Messages;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public abstract class QryptoViewModel : MvxViewModel
	{
		protected readonly IMvxMessenger _messenger;
		protected readonly IPopupHelper _helper;
		private MvxSubscriptionToken _token;

		protected delegate void CleaningUpEventHandler();
		protected event CleaningUpEventHandler CleaningUp;

		protected QryptoViewModel(IMvxMessenger messenger, IPopupHelper helper)
		{
			_helper = helper;
			_messenger = messenger;
			_token = _messenger.Subscribe<RequestFailedMessage>(OnRequestFailed, MvxReference.Strong);
			CleaningUp += () =>
			{
				if (_token == null)
					return;
				_token.Dispose();
				_token = null;
			};
		}

		public void OnCleanup()
		{
			CleaningUp?.Invoke();
		}

		public void OnRequestFailed(RequestFailedMessage message)
		{
			Dispatcher.RequestMainThreadAction(() =>_helper.ShowRequestFailedPopup(message.MessageBody));
		}

		protected ManualResetEvent ResetEvent = new ManualResetEvent(false);
		private bool _working;

		public bool Working
		{
			get { return _working; }
			set
			{
				_working = value;
				RaisePropertyChanged();
			}
		}

		public async void MakeApiCallAsync<TResult>(Func<TResult> call, Action<TResult> callback = null)
		{
			ResetEvent.Reset();
			Working = true;

			var res = await Task.Run(call);

			if(res != null)
				callback?.Invoke(res);

			Working = false;
			ResetEvent.Set();
		}

		public async void MakeApiCallAsync(Action call, Action callback = null)
		{
			ResetEvent.Reset();
			Working = true;

			await Task.Run(call);
			callback?.Invoke();

			Working = false;
			ResetEvent.Set();
		}
	}
}
