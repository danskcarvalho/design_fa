using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VContentPage : ContentPage
    {
        public static readonly BindableProperty IsLoadingProperty =
           BindableProperty.Create("IsLoading", typeof(bool), typeof(VContentPage), false);
        public static readonly BindableProperty GoBackCommandProperty =
            BindableProperty.Create("GoBackCommand", typeof(ICommand), typeof(VContentPage), null);
        public static readonly BindableProperty ShowMenuCommandProperty =
            BindableProperty.Create("ShowMenuCommand", typeof(ICommand), typeof(VContentPage), null);
        public static readonly BindableProperty IsPopupVisibleProperty =
            BindableProperty.Create("IsPopupVisible", typeof(bool), typeof(VContentPage), false);
        public static readonly BindableProperty PopupContentProperty =
            BindableProperty.Create("PopupContent", typeof(View), typeof(VContentPage), null);
        public static readonly BindableProperty ClosePopupCommandProperty =
            BindableProperty.Create("ClosePopupCommand", typeof(ICommand), typeof(VContentPage), null);

        public VContentPage()
        {
            ClosePopupCommand = new Command(() =>
            {
                HideModal();
            });
            GoBackCommand = new Command(async () =>
            {
                await this.Navigation.PopAsync();
            });
            ShowMenuCommand = new Command(() =>
            {
                ShowMenu();
            });
        }

        private void ShowMenu()
        {
            var masterDetail = GoToMasterPage();
            if (masterDetail != null)
                masterDetail.IsPresented = true;
        }
        private Xamarin.Forms.MasterDetailPage GoToMasterPage()
        {
            try
            {
                Element current = this;
                while (current != null && !(current is Xamarin.Forms.MasterDetailPage))
                {
                    current = current.Parent;
                }

                if (current != null)
                    return (Xamarin.Forms.MasterDetailPage)current;
                else
                {
                    foreach (var page in this.Navigation.NavigationStack)
                    {
                        if (page is Xamarin.Forms.MasterDetailPage)
                            return (Xamarin.Forms.MasterDetailPage)page;
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public ICommand GoBackCommand
        {
            get { return (ICommand)GetValue(GoBackCommandProperty); }
            set { SetValue(GoBackCommandProperty, value); }
        }
        public ICommand ShowMenuCommand
        {
            get { return (ICommand)GetValue(ShowMenuCommandProperty); }
            set { SetValue(ShowMenuCommandProperty, value); }
        }
        public bool IsPopupVisible
        {
            get { return (bool)GetValue(IsPopupVisibleProperty); }
            set { SetValue(IsPopupVisibleProperty, value); }
        }
        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }
        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }

        public Task DisplayModal(VContentView content)
        {
            content.ParentPage = this;
            this.PopupContent = content;
            this.IsPopupVisible = true;
            return Task.FromResult(true);
        }
        public void HideModal()
        {
            if (this.PopupContent != null && this.PopupContent is VContentView)
                ((VContentView)this.PopupContent).ParentPage = null;
            this.PopupContent = null;
            this.IsPopupVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPopupVisible)
            {
                var content = PopupContent as VContentView;
                if (content != null)
                {
                    HideModal();
                    return true;
                }
                else
                    return base.OnBackButtonPressed();
            }
            else
                return base.OnBackButtonPressed();
        }
    }
}
