using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			TestMessageReceive();
        }

		public static void TestMessageReceive()
		{
			var repo = new MailBoxModels.Entities.MailBoxRepository();			

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
		}

		public static void TestMessageReadFromDb()
		{

		}
	}
}
