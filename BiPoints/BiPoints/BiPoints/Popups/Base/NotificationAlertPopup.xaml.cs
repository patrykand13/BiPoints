using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

namespace BiPoints.Popups.Base
{
    public partial class NotificationAlertPopup : PopupPage
    {
        private string _message { get; set; }
        public string Message { get { return _message; } set { _message = value; OnPropertyChanged(); } }
        public NotificationAlertPopup(string message)
        {
            Message = message;
            InitializeComponent();
        }
        private void OnClose(object sender, EventArgs e)
        {
            try { PopupNavigation.Instance.PopAsync(); } catch { }
        }
    }
}