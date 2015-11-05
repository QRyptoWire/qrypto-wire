using System;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;

namespace QRyptoWire.Core.ViewModels
{
	public abstract class QryptoViewModel : MvxViewModel
	{
		protected ManualResetEvent ResetEvent = new ManualResetEvent(false);

		public async void MakeApiCallAsync<TResult>(Func<TResult> call, Action<TResult> callback = null)
		{
			ResetEvent.Reset();

			var res = await Task.Run(call);

			if(res != null)
				callback?.Invoke(res);

			ResetEvent.Set();
		}
	}
}
