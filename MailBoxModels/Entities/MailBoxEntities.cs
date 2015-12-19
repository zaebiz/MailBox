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
		public DbSet<MessageIssue> Issues { get; set; }
		public DbSet<Message> Messages { get; set; }		
	}

	public class MessageIssue 
	{
		[Key]
		public int issueId { get; set; }

		public DateTime issueDate { get; set; }
		public int messageCount { get; set; }
		public string requestIpAdress { get; set; }

		public string sender { get; set; }
		public string subject { get; set; }
		public string message { get; set; }

		public virtual IEnumerable<Message> Messages { get; set; }
	}

	public class Message
	{
		[Key]
		public int messageId { get; set; }				

		public string recipient { get; set; }
		public int status { get; set; }

		public int? issueId { get; set; }
		public virtual MessageIssue issue { get; set; }
	}
}
