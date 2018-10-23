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
	public partial class FacilAcessoMasterPage : VContentPage
	{
		public FacilAcessoMasterPage ()
		{
			InitializeComponent ();
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OndeEstacionarPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeusCondominiosPage());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoricoUsoPage());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeusDadosPage());
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartaoCreditoPage());
        }

        private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeusCreditosPage());
        }

        private async void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeusConvitesPage());
        }

        private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarimbarAcessoPage());
        }

        private async void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FaleAcessoFacilPage());
        }
    }
}