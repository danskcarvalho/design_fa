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
	public partial class MeusCondominiosPage : VContentPage
	{
		public MeusCondominiosPage ()
		{
			InitializeComponent ();
            picker.ItemsSource = new List<string> { "Salvador Shopping Business" };
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
		}

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            picker.FontAttributes = FontAttributes.Bold;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetalhesLocalidadePage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await DisplayAlert("Fácil Acesso", "Rotas", "Ok");
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvidarVisitantesPage());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoricoUsoPage());
        }
    }
}