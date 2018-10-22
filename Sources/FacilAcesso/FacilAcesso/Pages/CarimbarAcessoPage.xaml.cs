using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacilAcesso
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarimbarAcessoPage : VContentPage
	{
		public CarimbarAcessoPage ()
		{
			InitializeComponent ();
            picker.ItemsSource = new List<string> { "Transferir pagamento para mim", "Transferir pagamento para outrem" };

        }
	}
}