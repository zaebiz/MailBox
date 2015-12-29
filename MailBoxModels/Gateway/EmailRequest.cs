using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MailBoxModels.Gateway
{
	[DataContract(Name = "EmailRequest")]
	public class EmailRequest
	{
		public EmailRequest()
		{
			this.sender = "test@uralsibins.ru";
			this.allowSendTime = DateTime.Now;
		}

		[DataMember]
		[Required]
		[DataType(DataType.EmailAddress)]
		//[EmailAddress]
		[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is invalid.")]
		public string sender { get; set; }

		[DataMember]
		[Required]
		public List<string> recipientList { get; set; }

		[DataMember]
		[Required]
		public string subject { get; set; }

		[DataMember]
		public string message { get; set; }

		[DataMember]
		public DateTime allowSendTime { get; set; }

		#region JQuery-like Mail builder methods
		public EmailRequest Subject(string s)
		{
			this.subject = s;
			return this;
		}

		public EmailRequest From(string s)
		{
			this.sender = s;
			return this;
		}

		public EmailRequest To(IEnumerable<string> list)
		{
			this.recipientList = list.ToList();
			return this;
		}

		public EmailRequest Message(string s)
		{
			this.message = s;
			return this;
		}

		public EmailRequest WhenSend(DateTime d)
		{
			this.allowSendTime = d;
			return this;
		}
		#endregion

		public List<string> Validate()
		{
			var context = new ValidationContext(this, serviceProvider: null, items: null);
			var results = new List<ValidationResult>();
			var errorList = new List<string>();

			if (!Validator.TryValidateObject(this, context, results))
				errorList.AddRange(results.Select(vr => vr.ErrorMessage));

			return errorList;
		}

	}
}
