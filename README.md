Сервис рассылки писем (WCF). 

- Хостится внутри Windows- сервиса

- Сохраняет очередь писем. 

- Позволяет использовать предопределенные шаблоны.

- Позволяет отправлять почту в определенное время.

- Можно использовать для массовой рассылки (умеет слать в несколько потоков, параметры задаются в конфиге)

Новые шаблоны необходимо добавлять в код, пересобирать и перезаливать сервис.

Из-за ограничений почтового сервера (адрес задается в конфиге) сейчас возможна только рассылка внутри сети.

На данный момент существует проблема, при подключении референса на сервис в клиенте создается некорректный endpoint

    endpoint address="http://localhost:8733/MailBoxGateway/service" 
    
Для корректной работы его необходимо заменить на верный адрес сервиса (vs-tst00-web02)

    endpoint address="http://vs-tst00-web02:8733/MailBoxGateway/service" 

==============================================================================

Методы сервиса

1. SendMail(EmailRequest req) - посылка текстового письма без форматирования. 
EmailRequest - класс представляющий собой запрос на рассылку почты.
message = текст письма
subject = заголовок письма
sender = от чьего имени отправлять
recipientList = список получателей
allowSendTime = дата отправления (если не указана то отправит сразу)

            var list = new List<string>();
			for (var i = 0; i < 10; i++)
				list.Add($"titarenkons@uralsibins.ru");
				
            var client = new MailBoxService.GatewayClient();
			var msg = new EmailRequest()
			{
				message = "тестовая почта с тест 02",
				subject = "тестовая почта с тест 02",
				sender = "test2@uralsibins.ru",
				recipientList = list.ToArray(),
				allowSendTime = DateTime.Now.AddMinutes(10)
			};

			try
			{
				svc.SendMail(msg);
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
			
2. SendTemplateMail(EmailRequest req, SimpleTemplate templateData) - посылка письма на основе предоперделенного шаблона

Шаблон пока один SimpleTemplate - сообщение с эмблемой компании, именем получателя и текстом. 
Name = обращение к получателю
Message = текст письма

			SimpleTemplate template = new SimpleTemplate()
			{
				Message = "WCF сервис - для рассылки писем. Многопоточная рассылка + базовый механизм шаблонизации.",
				Name = "Никита Сергеевич"
			};
			
			var client = new MailBoxService.GatewayClient();
			client.SendTemplateMail(msg, template);