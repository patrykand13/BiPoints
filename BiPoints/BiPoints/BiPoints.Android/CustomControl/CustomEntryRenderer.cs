using Android.Content;
using Android.Graphics.Drawables;
using BiPoints.CustomControl;
using BiPoints.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace BiPoints.Droid.CustomControl
{
    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();

                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(0, 0, 0, 0);

            }
        }

        public CustomEntryRenderer(Context context) : base(context)
        {
        }
    }
}