using System;
using System.Collections.Generic;
using System.ServiceModel;
using MailBoxModels.Gateway;
using MailBoxModels.Entities;
using MailBoxModels.EmailTemplatesModels;

namespace GatewayService
{
	public class GatewayService : IGateway
	{
		public void SendMail(EmailRequest req)
		{
			ValidateRequest(req);
			using (MailBoxRepository db = new MailBoxRepository())
			{
				db.CreateIssue(req);
			}
		}

		public void SendTemplateMail(EmailRequest req, SimpleTemplate templateData)
		{
			if (templateData == null) throw new FaultException("template can`t be null");
			ValidateRequest(req);

			req.message = EmailTemplatesManager.Instance.GetTemplateHtml<SimpleTemplate>(templateData);
			using (MailBoxRepository db = new MailBoxRepository())
			{
				db.CreateIssue(req);
			}
		}

		void ValidateRequest(EmailRequest req)
		{
			if (req == null) throw new FaultException("request can`t be null");

			var errorList = req.Validate();
			if (errorList.Count > 0)
			{
				var exceptionData = new EmailRequestFault(errorList);
				throw new FaultException<EmailRequestFault>(exceptionData);
			}
		}
	}
}
