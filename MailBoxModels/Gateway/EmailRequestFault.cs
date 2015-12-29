using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels.Gateway
{
	[DataContract]
	public class EmailRequestFault
	{
		[DataMember]
		public List<string> ErrorList { get; set; }
		public EmailRequestFault()
		{
			this.ErrorList = new List<string>();
		}

		public EmailRequestFault(List<string> list)
		{
			this.ErrorList = list;
		}
	}
}
