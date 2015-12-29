using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Entities;
using System.Net.Mail;
using NLog;
using MailBoxModels.EmailTemplatesModels;


namespace MailBoxModels.MailManager
{
	public class MailSender
	{
		List<Email> _msgList;

		public MailSender()
		{
            _msgList = new List<Email>();
		}

		public MailSender(IEnumerable<Email> msgs)
		{
			_msgList = msgs.ToList();
		}

		public IEnumerable<MessageSendResult> SendMail()
		{
			foreach (var m in _msgList)
			{
				var sendStatus = SendLetter(m.issue.sender, m.recipient, m.issue.subject, m.issue.message);
				yield return new MessageSendResult
				{
					messageId = m.messageId,
					status = sendStatus ? MessageStatus.Success : MessageStatus.Fail
				};

				System.Threading.Thread.Sleep(50);
			}
		}

		bool SendLetter(string from, string to, string subject, string message)
		{
			bool status = true;
			MailMessage letter = new MailMessage();
			SmtpClient client = new SmtpClient();

			try
			{
				letter.From = new MailAddress(from);
				letter.To.Add(new MailAddress(to));

				letter.Subject = subject;
				letter.IsBodyHtml = true;
				letter.Body = message;
				AddLetterAttachments(letter);

				client.Host = MailBoxModels.Settings.MailServer;
				client.Port = 25;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.Send(letter);
			}
			catch (Exception ex)
			{
				status = false;
				MailBoxLog.Instance.Error(ex);
			}
			finally
			{
				client.Dispose();
				letter.Dispose();
			}

			return status;
		}

		void AddLetterAttachments(MailMessage letter)
		{
			var attList = EmailTemplatesManager.Instance.GetTemplateAttachments();
			foreach (var att in attList) letter.Attachments.Add(att);
		}

	}

}
