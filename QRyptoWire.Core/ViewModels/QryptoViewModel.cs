using System;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;

namespace QRyptoWire.Core.ViewModels
{
	public abstract class QryptoViewModel : MvxViewModel
	{
		public ManualResetEvent ResetEvent = new ManualResetEvent(false);

		public void MakeApiCallAsync<TResult>(Func<TResult> call, Action<TResult> callback = null)
		{
			ResetEvent.Reset();

			TResult res = default(TResult);
			Task.Run(async () => await Task.Run(() =>
			{
				res = call.Invoke();
			})).Wait();

			callback?.Invoke(res);

			ResetEvent.Set();
		}
	}
}
