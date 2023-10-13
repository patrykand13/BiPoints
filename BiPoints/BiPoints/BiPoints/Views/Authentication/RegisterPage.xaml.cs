using BiPoints.ViewModels.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            BindingContext = new RegisterVM();
            InitializeComponent();
        }
    }
}