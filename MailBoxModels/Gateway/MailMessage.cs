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
		public string sender { get; set; }

		[DataMember]
		public string recipient { get; set; }

		[DataMember]
		public string subject { get; set; }

		[DataMember]
		public string message { get; set; }

		//[DataMember]
		//public int templateId { get; set; }

		#region JQuery-like Mail builder methods
		public MailMessage Create()
		{
			this.sender = "test@uralsibins.ru";
			return this;
		}

		public MailMessage Subject(string s)
		{
			this.subject = s;
			return this;
		}

		public MailMessage From(string s)
		{
			this.subject = s;
			return this;
		}

		public MailMessage To(string s)
		{
			this.recipient = s;
			return this;
		}

		public MailMessage Message(string s)
		{
			this.message = s;
			return this;
		}
		#endregion

	}
}
