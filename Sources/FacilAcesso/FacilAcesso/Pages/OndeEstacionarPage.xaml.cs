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
	public partial class OndeEstacionarPage : VContentPage
	{
		public OndeEstacionarPage ()
		{
			InitializeComponent ();
            listView.ItemsSource = new List<object> { new object(), new object(), new object(), new object(), new object(), new object(), new object(),
            new object(), new object(), new object(), new object(), new object() };

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (tabNavigation.SelectedIndex == 1)
                await Navigation.PushAsync(new TicketPage());
            else
                await DisplayModal(new CadastrarNovoCartao2Modal());
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                await Navigation.PushAsync(new DetalhesEstacionamentoPage());
                listView.SelectedItem = null;
            }
        }
    }
}