using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Entities;
using MailBoxModels;
using MailBoxModels.EmailTemplatesModels;
using System.ServiceModel;
using MailBoxModels.Gateway;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var repo = new MailBoxRepository();
			//TestRazorEngine();
			TestMessageInsert(repo);
			//TestMessageManager();

			// direct test
			//TestMessageInsert(repo);
			//TestIssueReadFromDb(repo);
			//TestMessageReadFromDb(repo);

			// test gateway service
			//SendMessagesThroughService();
			//SendServiceExceptions();
			//TestIssueReadFromDb(repo);
			Console.ReadKey();
		}

		public static void TestRazorEngine()
		{
			var html =  "Вам необходимо выполнить действия по Жалобе клиента: "
							+ "skdjfskdjbf" + "<br>За номером: " + "sdsd" + "<br>Доп. информация: " + "<br>" + "1213"
							+ "<br>Переход на портал ЦКК: <a href=\"http://msk01-ckkweb01.uralsibins.ru/ckk/KardKK.aspx?incidentid="
							+ "123" + "\">Перейти</a><br>Инструкция: <a href=\"" + "www.mauil.ru" + "\">Открыть</a>";

			var template = new SimpleTemplate()
			{
				Name = "123",
				Message = html
			};

			var email = EmailTemplatesManager.Instance.GetTemplateHtml<SimpleTemplate>(template);
			return;
		}

		public static void TestMessageManager()
		{
			var manager = MailBoxModels.MailManager.MailManager.Instance;
			manager.Start();
		}

		public static void SendServiceExceptions()
		{			
			var list = new List<string>();
			for (var i = 0; i < 10; i++)
				list.Add($"titarenkons@uralsibins.ru");

			var client = new MailBoxService.GatewayClient();
			var msg = new EmailRequest()
				.To(list)
				.Subject("тест ошибок")
				.Message("test")
				.From("nikita");

			try
			{
				//client.SendMail(null);
				client.SendMail(msg);
			}
			catch (FaultException<EmailRequestFault> ex)
			{
				foreach (var e in ex.Detail.ErrorList)
					Console.WriteLine(e);
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static void SendMessagesThroughService()
		{
			var list = new List<string>();
			for (var i = 0; i < 10; i++)
				list.Add($"titarenkons@uralsibins.ru");

			var msg = new EmailRequest()
				.To(list)
				.Subject("тест сервис")
				.Message("A README file contains information about other files in a repository and is commonly distributed with computer software, forming part of its documentation. We recommend you to add README file to the repository and GitLab will render it here instead of this message.");

			var client = new MailBoxService.GatewayClient();
			client.SendMail(msg);

			SimpleTemplate template = new SimpleTemplate()
			{
				Message = "WCF сервис - для рассылки писем. Многопоточная рассылка + базовый механизм шаблонизации.",
				Name = "Никита Сергеевич"
			};

			msg = new MailBoxModels.Gateway.EmailRequest()
				.To(list)
				.Subject("тест секрвис c шаблоном")
				.Message("");

			client.SendTemplateMail(msg, template);
        }

		public static void TestMessageInsert(MailBoxRepository repo)
		{
			var list = new List<string>();
			for (var i = 0; i < 3; i++)
				list.Add($"titarenkons@uralsibins.ru");

			//var msg = new MailBoxModels.Gateway.EmailRequest()
			//	.To(list)
			//	.Subject("6 минут")
			//	.Message("6 минут")
			//	.WhenSend(DateTime.Now.AddMinutes(6));

			//repo.CreateIssue(msg);

			// c HTML внутри сообщения и шаблоном
			var htmlMessage = "Вам необходимо выполнить действия по Жалобе клиента: "
							+ "skdjfskdjbf" + "<br>За номером: " + "sdsd" + "<br>Доп. информация: " + "<br>" + "1213"
							+ "<br>Переход на портал ЦКК: <a href=\"http://msk01-ckkweb01.uralsibins.ru/ckk/KardKK.aspx?incidentid="
							+ "123" + "\">Перейти</a><br>Инструкция: <a href=\"" + "www.mauil.ru" + "\">Открыть</a>";

			SimpleTemplate letter = new SimpleTemplate()
			{
				Message = htmlMessage,
				Name = "Никита Сергеевич"
			};

			var html = EmailTemplatesManager.Instance.GetTemplateHtml<SimpleTemplate>(letter);
			var msg = new MailBoxModels.Gateway.EmailRequest()
				.To(list)
				.Subject("тест c шаблоном 3 мин")
				.Message(html)
				.WhenSend(DateTime.Now);

			repo.CreateIssue(msg);			
		}

		public static void TestIssueReadFromDb(MailBoxRepository repo)
		{
			var issues = repo.ReadAllIssues();
			foreach (var i in issues)
			{
				Console.WriteLine($"issue {i.issueId} from {i.sender} text {i.subject}");
				foreach (var m in i.Messages)
					Console.WriteLine($"to {m.recipient}");
			}
		}

		public static void TestMessageReadFromDb(MailBoxRepository repo)
		{
			var msgs = repo.ReadMessages();

			foreach (var m in msgs)
				Console.WriteLine($"message to {m.recipient} from {m.issue.sender} status {m.status}");
		}

		public static void TestUpdateMessageStatus(MailBoxRepository repo)
		{
			var msgIds = new List<int>() { 1, 3, 7 };
			repo.UpdateMessageStatus(msgIds, MessageStatus.Success);
		}
	}
}
