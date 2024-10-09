using BiPoints.Interfaces.Authenticate;
using BiPoints.Models.Responses.Authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Authentication
{
    internal class RegisterVM : AuthenticationVM
    {
        private readonly IRegisterServices _registerServices;
        private string _username { get; set; }
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); CheckData("username"); } }
        private string _password { get; set; }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); CheckData("password"); } }
        private string _name { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); CheckData("name"); } }
        private string _lastname { get; set; }
        public string Lastname { get { return _lastname; } set { _lastname = value; OnPropertyChanged(); CheckData("lastname"); } }
        private string _usernameFrameColor { get; set; } = "#131C74";
        public string UsernameFrameColor { get { return _usernameFrameColor; } set { _usernameFrameColor = value; OnPropertyChanged(); } }
        private string _passwordFrameColor { get; set; } = "#131C74";
        public string PasswordFrameColor { get { return _passwordFrameColor; } set { _passwordFrameColor = value; OnPropertyChanged(); } }
        private string _nameFrameColor { get; set; } = "#131C74";
        public string NameFrameColor { get { return _nameFrameColor; } set { _nameFrameColor = value; OnPropertyChanged(); } }
        private string _lastnameFrameColor { get; set; } = "#131C74";
        public string LastnameFrameColor { get { return _lastnameFrameColor; } set { _lastnameFrameColor = value; OnPropertyChanged(); } }
        private bool _usernameIsNotValid { get; set; }
        public bool UsernameIsNotValid { get { return _usernameIsNotValid; } set { _usernameIsNotValid = value; OnPropertyChanged(); } }
        private bool _passwordIsNotValid { get; set; }
        public bool PasswordIsNotValid { get { return _passwordIsNotValid; } set { _passwordIsNotValid = value; OnPropertyChanged(); } }
        private bool _nameIsNotValid { get; set; }
        public bool NameIsNotValid { get { return _nameIsNotValid; } set { _nameIsNotValid = value; OnPropertyChanged(); } }
        private bool _lastnameIsNotValid { get; set; }
        public bool LastnameIsNotValid { get { return _lastnameIsNotValid; } set { _lastnameIsNotValid = value; OnPropertyChanged(); } }
        public ICommand SignUpCommand => new Command(async () => await SignUpAsync());
        public ICommand GoLoginCommand => new Command(async () => await GoLoginAsync());
        internal RegisterVM()
        {
            _registerServices = DependencyService.Get<IRegisterServices>();
        }
        private async Task SignUpAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // Check the validity of the entered data.
            var isSuccess = CheckData("all");

            // If the data is invalid, exit the registration process.
            if (!isSuccess)
            {
                IsBusy = false;
                return;
            }

            var response = await _registerServices.RegisterAsync(Username, Password, Name, Lastname);
            // Check if the response is not an error.
            if (CheckIfTheDataIsIncorrect(response))
            {
                IsBusy = false;
                return;
            }
            var authenticateModel = JsonConvert.DeserializeObject<AuthenticateResponse>(response);

            // Update user data in the local database.
            await _editProfileLocalDBServices.EditProfileAsync(authenticateModel.UserId, authenticateModel.Token);

            // Start the main user interface.
            await StartAsync(authenticateModel.UserId, authenticateModel.Token);
            IsBusy = false;
        }
        private async Task GoLoginAsync()
        {
            if (IsBusy)
                return;
            await PopPageAsync();
        }
        private bool CheckData(string type)
        {
            var dataCorrect = true;

            if (type.Equals("username") || type.Equals("all"))
            {
                // Check the validity of the username.
                if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
                {
                    UsernameFrameColor = "#F23333";
                    UsernameIsNotValid = true;
                    dataCorrect = false;
                }
                else
                {
                    UsernameFrameColor = "#131C74";
                    UsernameIsNotValid = false;
                }
            }

            if (type.Equals("password") || type.Equals("all"))
            {
                // Check the validity of the password.
                if (string.IsNullOrWhiteSpace(Password) || Password.Length < 3)
                {
                    PasswordFrameColor = "#F23333";
                    PasswordIsNotValid = true;
                    dataCorrect = false;
                }
                else
                {
                    PasswordFrameColor = "#131C74";
                    PasswordIsNotValid = false;
                }
            }

            if (type.Equals("name") || type.Equals("all"))
            {
                // Check the validity of the name.
                if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3)
                {
                    NameFrameColor = "#F23333";
                    NameIsNotValid = true;
                    dataCorrect = false;
                }
                else
                {
                    NameFrameColor = "#131C74";
                    NameIsNotValid = false;
                }
            }

            if (type.Equals("lastname") || type.Equals("all"))
            {
                // Check the validity of the last name.
                if (string.IsNullOrWhiteSpace(Lastname) || Lastname.Length < 3)
                {
                    LastnameFrameColor = "#F23333";
                    LastnameIsNotValid = true;
                    dataCorrect = false;
                }
                else
                {
                    LastnameFrameColor = "#131C74";
                    LastnameIsNotValid = false;
                }
            }

            return dataCorrect;
        }
    }
}
