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
	public partial class CadastrarNovoCartao3Modal : VContentView
	{
		public CadastrarNovoCartao3Modal ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.ParentPage.HideModal();
        }
    }
}