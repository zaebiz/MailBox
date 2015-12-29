using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.ServiceModel;
using NLog;
using MailBoxModels.MailManager;

namespace HostService
{
	public partial class GatewayHostService : ServiceBase
	{
		ServiceHost svcHost = null;		

		public GatewayHostService()
		{
			InitializeComponent();
			ServiceName = "MailBoxHostService";
		}

		protected override void OnStart(string[] args)
		{
			LogManager.GetCurrentClassLogger().Info("WINDOWS SERVICE START");

			if (svcHost != null) svcHost.Close();

			svcHost = new ServiceHost(typeof(GatewayService.GatewayService));
			svcHost.Open();

			MailManager.Instance.Start();
		}

		protected override void OnStop()
		{
			if (svcHost != null)
			{
				svcHost.Close();
				svcHost = null;
			}
		}
	}
}
