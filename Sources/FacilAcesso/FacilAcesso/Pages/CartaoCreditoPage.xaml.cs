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
	public partial class CartaoCreditoPage : VContentPage
	{
		public CartaoCreditoPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayModal(new CadastrarNovoCartaoModal());
        }

        public void CartaoInserido()
        {
            creditCardData.IsVisible = true;
            creditCardNoData.IsVisible = false;
            lbl1.IsVisible = true;
            lbl2.IsVisible = true;
        }
    }
}