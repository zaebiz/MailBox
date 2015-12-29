using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels.EmailTemplatesModels
{
	public interface IEmailTemplate
	{
		string TemplateName
		{
			get;
		}
	}

	[DataContract]
	public class SimpleTemplate : IEmailTemplate
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Message { get; set; }

		string IEmailTemplate.TemplateName
		{
			get
			{
				return "Simple";
			}
		}
	}
}
