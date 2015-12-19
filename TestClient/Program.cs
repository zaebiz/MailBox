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
			testRepository();
        }

		public static void testRepository()
		{
			var repo = new MailBoxModels.Entities.Repository();
			var msg = new MailBoxModels.Gateway.MailMessage()
				.To("to@to.ru")
				.Subject("тест")
				.Message("тестовое сообщение");

			var list = new List<string>();
			list.Add("a1@mail.ru");
			list.Add("a2@mail.ru");
			list.Add("a3@mail.ru");
			list.Add("a4@mail.ru");
			list.Add("a5@mail.ru");

			repo.CreateIssue(msg, null);
			repo.CreateIssue(msg, list);
		}
	}
}
