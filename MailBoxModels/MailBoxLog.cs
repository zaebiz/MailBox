using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MailBoxModels
{
	public class MailBoxLog
	{
		static Lazy<MailBoxLog> _log = new Lazy<MailBoxLog>(() => new MailBoxLog());
		public static MailBoxLog Instance
		{
			get { return _log.Value; }
		}


		MailBoxLog() { }

		public void Debug(string message)
		{
#if DEBUG
			LogManager.GetCurrentClassLogger().Debug(message);
#endif
		}

		public void Info(string message)
		{
			LogManager.GetCurrentClassLogger().Info(message);
		}

		public void Error(string message)
		{
			LogManager.GetCurrentClassLogger().Error(message);
		}

		public void Error(Exception e)
		{
			LogManager.GetCurrentClassLogger().Error(e);
		}
	}
}
