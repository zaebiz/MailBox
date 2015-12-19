using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MailBoxModels.Gateway;

namespace GatewayService
{
	public class GatewayService : MailBoxModels.Gateway.IGateway
	{
		public void SendMail(IEnumerable<MailMessage> mailList)
		{
			throw new NotImplementedException();
		}
	}
}
