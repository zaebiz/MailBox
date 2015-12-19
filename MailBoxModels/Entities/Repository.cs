using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Gateway;
using NLog;

namespace MailBoxModels.Entities
{
	class Repository
	{
		MailBoxEntities db;

		public Repository()
		{
			db = new MailBoxEntities();
		}

		Message CreateMessage(MailMessage msg, MessageIssue issue)
		{
			var email = db.Messages.Create();
			email.issue = issue;
			email.status = 0;
			return email;
		}

		public void CreateIssue(MailMessage msg, IEnumerable<string> recipientList)
		{
			if (msg == null) throw new ArgumentNullException("msg");

			var issue = db.Issues.Create();
			issue.issueDate = DateTime.Now;
			issue.messageCount = 1;

			if (recipientList != null && recipientList.Count() > 0)
			{
				issue.messageCount = recipientList.Count();
				foreach (var r in recipientList)
				{
					msg.recipient = r;
					db.Messages.Add(CreateMessage(msg, issue));
				}
			}
			else db.Messages.Add(CreateMessage(msg, issue));

			db.SaveChanges();
			NLog.LogManager.GetCurrentClassLogger()
				.Info($"Issue #{issue.issueId} with subject <{msg.subject}> for {issue.messageCount} recipients SUCCESSFULLY ADDED");
		}
	}
}
