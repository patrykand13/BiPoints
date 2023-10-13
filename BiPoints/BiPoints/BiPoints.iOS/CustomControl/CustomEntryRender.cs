using BiPoints.CustomControl;
using BiPoints.iOS.CustomControl;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRender))]
namespace BiPoints.iOS.CustomControl
{
    public class CustomEntryRender : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Control != null)
            {

                base.OnElementPropertyChanged(sender, e);

                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}