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
	public partial class TicketPage : VContentPage
	{
		public TicketPage ()
		{
			InitializeComponent ();
		}

        int taps = 0;
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(taps == 0)
            {
                stckHorario.IsVisible = false;
                stckDados.IsVisible = true;
                stckBottom.IsVisible = true;
            }
            else if(taps == 1)
            {
                stckBottom.IsVisible = false;
                await DisplayModal(new PagamentoRealizadoModal());
            }
            taps++;
        }
    }
}