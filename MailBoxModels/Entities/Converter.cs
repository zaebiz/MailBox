using AutoMapper;
using MailBoxModels.Gateway;
using System;

namespace MailBoxModels.Entities
{
	public class Converter
	{
		static Converter()
		{
			Mapper.CreateMap<MailRequest, MessageIssue>()
				.AfterMap((src, dest) =>
				{
					dest.messageCount = src.recipientList.Count;
					dest.issueDate = DateTime.Now;
				});
		}

		public static MessageIssue MailRequestToMessageIssue(MailRequest request)
		{
			return Mapper.Map<MessageIssue>(request);
		}
	}
}
