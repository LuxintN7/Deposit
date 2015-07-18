using System.ComponentModel;
using System.ServiceProcess;

namespace InterestPaymentService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InterestPaymentServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.InterestPaymentServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // InterestPaymentServiceProcessInstaller
            // 
            this.InterestPaymentServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.InterestPaymentServiceProcessInstaller.Password = null;
            this.InterestPaymentServiceProcessInstaller.Username = null;
            // 
            // InterestPaymentServiceInstaller
            // 
            this.InterestPaymentServiceInstaller.Description = "Interest payment service (Deposit)";
            this.InterestPaymentServiceInstaller.ServiceName = "InterestPaymentService";
            this.InterestPaymentServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.InterestPaymentServiceInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.InterestPaymentServiceProcessInstaller,
            this.InterestPaymentServiceInstaller});

        }

        #endregion

        private ServiceProcessInstaller InterestPaymentServiceProcessInstaller;
        private ServiceInstaller InterestPaymentServiceInstaller;
    }
}