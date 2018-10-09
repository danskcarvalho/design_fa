using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VAbsoluteLayout : AbsoluteLayout
    {
        public event EventHandler<PanUpdatedEventArgs> PanUpdated;
        public event EventHandler<PinchGestureUpdatedEventArgs> PinchUpdated;

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            PanUpdated?.Invoke(sender, e);
        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            PinchUpdated?.Invoke(sender, e);
        }
    }
}
