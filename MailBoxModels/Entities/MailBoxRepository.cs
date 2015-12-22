using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Gateway;
using NLog;

namespace MailBoxModels.Entities
{
	public class MailBoxRepository : IDisposable
	{
		MailBoxEntities _db;

		public MailBoxRepository()
		{
			_db = new MailBoxEntities();
		}

		Message CreateMessage(string recipient, MessageIssue issue)
		{
			var email = _db.Messages.Create();
			email.recipient = recipient;
			email.status = 0;
			email.issue = issue;

			return email;
		}

		public void CreateIssue(MailRequest request)
		{
			//var issue = _db.Issues.Create();
			var issue = Converter.MailRequestToMessageIssue(request);

			foreach (var r in request.recipientList)
				_db.Messages.Add(CreateMessage(r, issue));

			_db.SaveChanges();
			NLog.LogManager.GetCurrentClassLogger()
				.Info($"Issue #{issue.issueId} with subject <{request.subject}> for {issue.messageCount} recipients SUCCESSFULLY ADDED");
		}

		public IEnumerable<MessageIssue> ReadAllIssues()
		{
			return _db.Issues.ToList();
		}

		public IEnumerable<Message> ReadMessages(int count = 0)
		{
			var msgList = _db.Messages
				.Where(m => m.status == 0)
				.OrderBy(m => m.messageId);

			return (count > 0) ? msgList.Take(count) : msgList;
		}

		public void UpdateMessageStatus(IEnumerable<int> msgIds)
		{
			_db.Messages
				.Where(m => msgIds.Any(li => li == m.messageId))
				.ToList()
				.ForEach(m => m.status = 1);

			_db.SaveChanges();
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
