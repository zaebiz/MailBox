using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MailBoxModels.Gateway
{
	[ServiceContract]
	public interface IGateway
	{
		[OperationContract]
		void SendGroupMail(MailMessage msg, List<string> msgList);

		[OperationContract]
		void SendMail(MailMessage msg);
	}
}
