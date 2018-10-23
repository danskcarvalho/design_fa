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
	public partial class HistoricoUsoPage : VContentPage
	{
		public HistoricoUsoPage ()
		{
			InitializeComponent ();
            listView.ItemsSource = new List<object> { new object(), new object(), new object(), new object(), new object(), new object(), new object(), new object(),
            new object(), new object(), new object(), new object() };

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeusCreditosPage());
        }
    }
}