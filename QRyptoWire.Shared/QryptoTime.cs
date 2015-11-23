using System;

namespace QRyptoWire.Shared
{
	public static class QryptoTime
	{
		public static Func<DateTime> CurrentLogic = () => DateTime.UtcNow;

		public static void RevertLogic()
		{
			CurrentLogic = () => DateTime.UtcNow;
		}

		public static DateTime GetTime => CurrentLogic.Invoke();
	}
}
