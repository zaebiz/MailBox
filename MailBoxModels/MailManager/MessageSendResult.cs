using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels.MailManager
{
	public class MessageSendResult
	{
		public int messageId { get; set; }
		public Entities.MessageStatus status { get; set; }
	}
}
