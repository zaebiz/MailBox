using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailBoxModels;
using MailBoxModels.Entities;
using NLog;
using System.Collections.Concurrent;

namespace MailBoxModels.MailManager
{
	public class MailManager
	{		
		// singleton code
		static Lazy<MailManager> _manager = new Lazy<MailManager>(() => new MailManager());		
		public static MailManager Instance
		{
			get { return _manager.Value; }
		}

		static int _runningTaskCount = 0;
		static object _lockObject = new object { };

		private MailManager()
		{
		}

		public void Start()
		{
			MailBoxLog.Instance.Info("START MESSAGE PROCESSING");

			var timer = new System.Timers.Timer(Settings.MailCheckPeriod * 1000);
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Elapsed += ProcessMail;
			timer.Start();
		}

		public void ProcessMail(object src, System.Timers.ElapsedEventArgs e)
		{
			MailBoxLog.Instance.Debug($"TIMER EVENT in #{Thread.CurrentThread.ManagedThreadId}");
			MailBoxLog.Instance.Debug($"Total thread = { Settings.ThreadCount }. Free threads = {Settings.ThreadCount - _runningTaskCount}");

			var canStartCount = Settings.ThreadCount - _runningTaskCount;
            for (var i = 0; i < canStartCount; i++)
			{
				var newMailTask = ProcessMailPortion();
				if (newMailTask != null) incrementThreadCount(1);
				else break;		// no letters for send awailable
			}

			Thread.Sleep(1000);	// wait for all child threads to start
		}

		Task ProcessMailPortion()
		{
			var db = new MailBoxRepository();
			var msgList = db.ReadMessages(Settings.MailPortionSize);
			if (msgList.Count > 0)
			{
				MailBoxLog.Instance.Debug($"portion of {msgList.Count()} letters read success. id = {String.Join(",", msgList.Select(m => m.messageId))} ");
				db.UpdateMessageStatus(msgList.Select(m => m.messageId), MessageStatus.Processing);
				return Task.Factory.StartNew(SendMailPortion, msgList);
			}
			else
			{
				MailBoxLog.Instance.Debug($"no letters available ");
				return null;
			}
		}

		void SendMailPortion(object data)
		{
			MailBoxLog.Instance.Debug($"thread # {Thread.CurrentThread.ManagedThreadId} start");

			List<Email> msgList = data as List<Email>;
			var db = new MailBoxRepository();
			var mailSender = new MailSender(msgList);
			var sendResult = mailSender.SendMail().ToList();
			var successList = sendResult.Where(r => r.status == MessageStatus.Success);
			var rejectList = sendResult.Where(r => r.status == MessageStatus.Fail);

			MailBoxLog.Instance.Debug($"send success: {successList.Count()} items id = {String.Join(",", successList.Select(m => m.messageId))} ");
			MailBoxLog.Instance.Info($"send fail: {rejectList.Count()} items id = {String.Join(",", rejectList.Select(m => m.messageId))} ");

			db.UpdateMessageStatus(successList.Select(r => r.messageId), MessageStatus.Success);
			db.UpdateMessageStatus(rejectList.Select(r => r.messageId), MessageStatus.Fail);

			MailBoxLog.Instance.Debug($"thread # {Thread.CurrentThread.ManagedThreadId} finish");
			incrementThreadCount(-1);
        }

		void incrementThreadCount(int step)
		{
			lock (_lockObject)
			{
				_runningTaskCount += step;
			}
		}

	}
}
