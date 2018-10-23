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
	public partial class CompartilharVisitantePage : VContentPage
	{
		public CompartilharVisitantePage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            stckOverlay.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            stckOverlay.IsVisible = false;
        }
    }
}