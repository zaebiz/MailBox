using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MailBoxModels.Gateway;

namespace GatewayService
{
	public class GatewayService : IGateway
	{
		public void SendMail(MailMessage msg)
		{
			throw new NotImplementedException();
		}

		public void SendGroupMail(MailMessage msg, List<string> msgList)
		{
			throw new NotImplementedException();
		}
	}
}
