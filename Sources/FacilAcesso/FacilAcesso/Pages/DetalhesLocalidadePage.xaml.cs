﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacilAcesso
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalhesLocalidadePage : VContentPage
	{
		public DetalhesLocalidadePage ()
		{
			InitializeComponent ();
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayModal(new QRcodeModal());
        }
    }
}