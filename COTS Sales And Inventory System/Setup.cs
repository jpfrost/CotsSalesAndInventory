using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;

namespace COTS_Sales_And_Inventory_System
{
    [RunInstaller(true)]
    public partial class Setup : Installer
    {
        public Setup()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            var regSrv = new RegistrationServices();
            regSrv.RegisterAssembly(GetType().Assembly,
                AssemblyRegistrationFlags.SetCodeBase);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            var regSrv = new RegistrationServices();
            regSrv.UnregisterAssembly(GetType().Assembly);
        }
    }
}