using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

namespace BiPoints.Popups.Base
{
    public partial class SuccessScanPopup : PopupPage
    {
        private string _name { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _image { get; set; }
        public string Image { get { return _image; } set { _image = value; OnPropertyChanged(); } }
        public SuccessScanPopup(string name, string image)
        {
            Name = name;
            Image = image;
            InitializeComponent();
        }
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}