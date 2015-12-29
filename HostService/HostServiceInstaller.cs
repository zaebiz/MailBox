using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace HostService
{
	[RunInstaller(true)]
	public class HostServiceInstaller : Installer
	{
		private ServiceProcessInstaller process;
		private ServiceInstaller service;

		public HostServiceInstaller()
		{
			process = new ServiceProcessInstaller();
			process.Account = ServiceAccount.LocalSystem;
			service = new ServiceInstaller();
			service.ServiceName = "MailBoxHostService";
			Installers.Add(process);
			Installers.Add(service);
		}
	}
}
