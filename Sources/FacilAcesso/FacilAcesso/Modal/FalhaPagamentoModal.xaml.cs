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
	public partial class FalhaPagamentoModal : VContentView
	{
		public FalhaPagamentoModal ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await this.ParentPage.DisplayModal(new CadastrarNovoCartao3Modal());
        }
    }
}