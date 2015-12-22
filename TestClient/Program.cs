using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Entities;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var repo = new MailBoxRepository();
			TestMessageReadFromDb(repo);
			TestUpdateMessageStatus(repo);
			TestMessageReadFromDb(repo);
			Console.ReadKey();
		}

		public static void TestMessageReceive(MailBoxRepository repo)
		{
			var list = new List<string>();
			list.Add("a1@mail.ru");
			list.Add("a2@mail.ru");
			list.Add("a3@mail.ru");
			list.Add("a4@mail.ru");
			list.Add("a5@mail.ru");

			var msg = new MailBoxModels.Gateway.MailRequest()
				.To(list)
				.Subject("тест")
				.Message("тестовое сообщение");

			repo.CreateIssue(msg);

			TestIssueReadFromDb(repo);
		}

		public static void TestIssueReadFromDb(MailBoxRepository repo)
		{
			var issues = repo.ReadAllIssues();
			foreach(var i in issues)
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
			repo.UpdateMessageStatus(msgIds);
		}
	}
}
