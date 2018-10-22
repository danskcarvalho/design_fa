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
	public partial class HistoricoComprasModal : VContentView
	{
		public HistoricoComprasModal ()
		{
			InitializeComponent ();
            itemRepeater.ItemsSource = new List<object> { new object(), new object(), new object(), new object(), new object(), new object(), new object(), new object(),
            new object(), new object(), new object(), new object() };

        }
	}
}