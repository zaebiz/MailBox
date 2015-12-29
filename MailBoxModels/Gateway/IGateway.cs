using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MailBoxModels.EmailTemplatesModels;

namespace MailBoxModels.Gateway
{
	[ServiceContract]
	public interface IGateway
	{
		[OperationContract]
		[FaultContract(typeof(EmailRequestFault))]
		void SendMail(EmailRequest msg);

		[OperationContract]
		void SendTemplateMail(EmailRequest req, SimpleTemplate templateData);
    }
}
