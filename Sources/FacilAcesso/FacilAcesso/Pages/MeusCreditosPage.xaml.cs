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
	public partial class MeusCreditosPage : VContentPage
	{
		public MeusCreditosPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayModal(new HistoricoComprasModal());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await DisplayModal(new CompraRealizadaModal());
        }
    }
}