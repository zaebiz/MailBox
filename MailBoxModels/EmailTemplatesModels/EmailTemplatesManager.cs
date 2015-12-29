using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using NLog;
using System.Net.Mail;
using System.Net.Mime;

namespace MailBoxModels.EmailTemplatesModels
{
	public class EmailTemplatesManager
	{
		public static Lazy<EmailTemplatesManager> templateService = new Lazy<EmailTemplatesManager>(() => new EmailTemplatesManager());
		public static EmailTemplatesManager Instance
		{
			get { return templateService.Value; }
		}

		public string TemplatesDir
		{
			get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EmailTemplates"); }
		}

		private EmailTemplatesManager()
		{
			CompileTemplates();
		}

		void CompileTemplates()
		{
			var templateFile = Path.Combine(TemplatesDir, "Simple.cshtml");
			Engine.Razor.AddTemplate("Simple", File.ReadAllText(templateFile));
			Engine.Razor.Compile("Simple", typeof(SimpleTemplate));
		}

		public string GetTemplateHtml<T>(T templateData) where T : IEmailTemplate
		{
			return Engine.Razor.Run(templateData.TemplateName, templateData.GetType(), templateData);
		}

		// TODO в классах шаблонах должен содержаться список картинок. При получении запроса на Email с шаблоном, брать из объекта класса предопределенный список кратинок и сохранять в БД вместе с письмом. при отправке брать картинки из списка с диска и аттачить к письму
		// TODO Второй вариант - разобраться почему не работает размещение картинок по ссылке прямо в html письма
		public IEnumerable<Attachment> GetTemplateAttachments()
		{
			var imagesDir = Path.Combine(TemplatesDir, "Images");

			Attachment imgLogo = new Attachment(Path.Combine(imagesDir, "logo_min.png"), MediaTypeNames.Image.Gif);
			imgLogo.ContentId = "logopng";
			yield return imgLogo;

			//Attachment imgHeart = new Attachment(Path.Combine(imagesDir, "heart_min.png"));
			//imgHeart.ContentId = "heartpng";
			//yield return imgLogo;
		}		
	}
}
