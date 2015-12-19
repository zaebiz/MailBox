using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MailBoxModels.Gateway;
using MailBoxModels.Entities;

namespace GatewayService
{
	public class GatewayService : IGateway
	{
		public void SendMail(MailRequest req)
		{
			if (req == null) throw new ArgumentNullException("request");

			using (MailBoxRepository db = new MailBoxRepository())
			{
				db.CreateIssue(req);
			}
		}
	}
}
