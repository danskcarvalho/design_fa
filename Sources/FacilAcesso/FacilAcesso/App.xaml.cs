using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FacilAcesso
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new HomePage());
            navigationPage.BarTextColor = Color.White;
            navigationPage.BarBackgroundColor = Color.FromHex("#1D346B");
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
