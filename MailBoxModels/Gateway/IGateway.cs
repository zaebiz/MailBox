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
		void SendMail(IEnumerable<MailMessage> mailList);
	}
}
