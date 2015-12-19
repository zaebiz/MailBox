using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels.Gateway
{
	[DataContract]
	public class MailMessage
	{
		[DataMember]
		public string from { get; set; }

		[DataMember]
		public IEnumerable<string> to { get; set; }

		[DataMember]
		public string subject { get; set; }

		[DataMember]
		public string message { get; set; }

		[DataMember]
		public int templateId { get; set; }
	}
}
