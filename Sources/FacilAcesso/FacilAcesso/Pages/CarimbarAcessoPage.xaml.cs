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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayModal(new CarimbarAcessoModal());
        }

        public void Carimbado()
        {
            this.stckCarimbar.IsVisible = false;
            this.stckSucesso.IsVisible = true;
            this.HideModal();
        }
    }
}