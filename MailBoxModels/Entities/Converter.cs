using AutoMapper;
using MailBoxModels.Gateway;
using System;
using System.IO;

namespace MailBoxModels.Entities
{
	public class Converter
	{
		static Converter()
		{
			Mapper.CreateMap<EmailRequest, EmailIssue>()
				.AfterMap((src, dest) =>
				{
					dest.messageCount = src.recipientList.Count;
					dest.issueDate = DateTime.Now;
				});
		}

		public static EmailIssue MailRequestToMessageIssue(EmailRequest request)
		{
			return Mapper.Map<EmailIssue>(request);
		}

		public static string ToXml<T>(T obj)
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

			using (StringWriter sw = new StringWriter())
			{
				serializer.Serialize(sw, obj);
				return sw.ToString();
			}
		}
	}
}
