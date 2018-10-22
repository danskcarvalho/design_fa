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
    }
}