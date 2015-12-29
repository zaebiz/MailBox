using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxModels
{
	public static class Settings
	{
		public static string MailServer = "10.175.32.66";
		public static int MailServerPort = 25;
		public static int ThreadCount = 2;
		public static int MailPortionSize = 100;
		public static int MailCheckPeriod = 60;

		static Settings()
		{
			var config = ConfigurationManager.AppSettings;

			if (config["ThreadCount"] != null) ThreadCount = Convert.ToInt32(config["ThreadCount"]);
			if (config["MailPortionSize"] != null) MailPortionSize = Convert.ToInt32(config["MailPortionSize"]);
			if (config["MailCheckPeriod"] != null) MailCheckPeriod = Convert.ToInt32(config["MailCheckPeriod"]);
			if (config["MailServerPort"] != null) MailServerPort = Convert.ToInt32(config["MailServerPort"]);
			if (config["MailServer"] != null) MailServer = config["MailServer"];
		}
	}
}
