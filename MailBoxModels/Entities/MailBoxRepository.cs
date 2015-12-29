using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Gateway;
using System.Xml.Serialization;
using NLog;
using MailBoxModels.EmailTemplatesModels;
using System.IO;

namespace MailBoxModels.Entities
{
	public class MailBoxRepository : IDisposable
	{
		MailBoxEntities _db;

		public MailBoxRepository()
		{
			_db = new MailBoxEntities();
		}

		Email CreateMessage(string recipient, EmailIssue issue, DateTime sendTime)
		{
			var email = _db.Messages.Create();
			email.recipient = recipient;
			email.status = (int)MessageStatus.New;
			email.allowSendTime = sendTime;
			email.issue = issue;

			return email;
		}

		public void CreateIssue(EmailRequest request) 
		{
			var issue = Converter.MailRequestToMessageIssue(request);

			foreach (var r in request.recipientList)
				_db.Messages.Add(CreateMessage(r, issue, request.allowSendTime));

			_db.SaveChanges();
			MailBoxLog.Instance.Debug($"Issue #{issue.issueId} with subject <{request.subject}> for {issue.messageCount} recipients SUCCESSFULLY ADDED");
		}

		public IEnumerable<EmailIssue> ReadAllIssues()
		{
			return _db.Issues.ToList();
		}

		public List<Email> ReadMessages(int count = 0)
		{
            var msgList = _db.Messages
				.Where(m => (m.status == (int)MessageStatus.New) && (m.allowSendTime <= DateTime.Now))
				.OrderBy(m => m.messageId);

			return ((count > 0) ? msgList.Take(count) : msgList).ToList();
		}

		public void UpdateMessageStatus(IEnumerable<int> msgIds, MessageStatus status)
		{
			_db.Messages
				.Where(m => msgIds.Any(li => li == m.messageId))
				.ToList()
				.ForEach(m => m.status = (int)status);

			_db.SaveChanges();
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
