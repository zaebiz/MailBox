using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels.Gateway
{
	[DataContract]
	public class MailRequest
	{
		[DataMember]
		public string sender { get; set; }

		[DataMember]
		public List<string> recipientList { get; set; }

		[DataMember]
		public string subject { get; set; }

		[DataMember]
		public string message { get; set; }

		//[DataMember]
		//public int templateId { get; set; }

		#region JQuery-like Mail builder methods
		public MailRequest()
		{
			this.sender = "test@uralsibins.ru";
		}

		public MailRequest Subject(string s)
		{
			this.subject = s;
			return this;
		}

		public MailRequest From(string s)
		{
			this.subject = s;
			return this;
		}

		public MailRequest To(IEnumerable<string> list)
		{
			this.recipientList = list.ToList();
			return this;
		}

		public MailRequest Message(string s)
		{
			this.message = s;
			return this;
		}
		#endregion

	}
}
