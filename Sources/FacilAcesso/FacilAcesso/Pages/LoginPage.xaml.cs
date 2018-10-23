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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacilAcessoMasterDetailPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecuperarSenhaPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CriarContaPage());
        }
    }
}