using System;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;

namespace QRyptoWire.Core.ViewModels
{
	public abstract class QryptoViewModel : MvxViewModel
	{
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
