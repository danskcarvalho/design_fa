using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VButton), typeof(FacilAcesso.iOS.VButtonRenderer))]
namespace FacilAcesso.iOS
{
    public class VButtonRenderer : ButtonRenderer
    {
    }
}