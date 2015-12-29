using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxModels.Gateway;
using System.ComponentModel.DataAnnotations;

namespace MailBoxModels.Entities
{
	public class MailBoxEntities : DbContext
	{
		public MailBoxEntities() : base("DbConnection") { }
		public DbSet<EmailIssue> Issues { get; set; }
		public DbSet<Email> Messages { get; set; }		
	}

	public class EmailIssue 
	{
		[Key]
		public int issueId { get; set; }

		public DateTime issueDate { get; set; }
		public int messageCount { get; set; }
		public string requestIpAdress { get; set; }

		public string sender { get; set; }
		public string subject { get; set; }
		public string message { get; set; }

		public virtual ICollection<Email> Messages { get; set; }
	}

	public class Email
	{
		[Key]
		public int messageId { get; set; }				

		public string recipient { get; set; }
		public int status { get; set; }
		public DateTime allowSendTime { get; set; }

		public int? issueId { get; set; }
		public virtual EmailIssue issue { get; set; }
	}
}
