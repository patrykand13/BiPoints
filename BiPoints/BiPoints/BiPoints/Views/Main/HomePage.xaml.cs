using BiPoints.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            homeLabel.Text = ProfileHelper.Name;
        }
    }
}