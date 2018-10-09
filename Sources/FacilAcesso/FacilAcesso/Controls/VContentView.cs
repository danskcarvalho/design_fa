using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VContentView : ContentView
    {
        public VContentView()
        {
        }

        public VContentPage ParentPage
        {
            get; set;
        }
    }
}
