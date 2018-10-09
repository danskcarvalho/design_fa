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
    public partial class VCustomPickerModal : VContentView
    {
        public VCustomPickerModal()
        {
            InitializeComponent();
        }

        private IReadOnlyList<string> Items;
        private string Placeholder;
        private Action<int> OnSelected;

        public VCustomPickerModal(
            IReadOnlyList<string> items, 
            string placeholder, 
            Action<int> onSelected, 
            VContentPage parentPage)
            : this()
        {
            Items = items;
            Placeholder = placeholder;
            OnSelected = onSelected;
            ParentPage = parentPage;

            List<object> actions = new List<object>();
            actions.Add(new
            {
                Nome = placeholder,
                Color = Color.FromHex("#FF3824"),
                TappedCommand = new Command(() => {
                    parentPage.HideModal();
                    OnSelected(-1);
                })
            });

            for (int i = 0; i < items.Count; i++)
            {
                var mIndex = i; //IMPORTANTE: Não remover!
                actions.Add(new
                {
                    Nome = items[i],
                    Color = Color.FromHex("#296CB2"),
                    TappedCommand = new Command(() => {
                        parentPage.HideModal();
                        OnSelected(mIndex);
                    })
                });
            }
            itemRepeater.ItemsSource = actions;
        }
    }
}