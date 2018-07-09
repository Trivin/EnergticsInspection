using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{ 
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent();
		}

        private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(true));
        }

        private async Task Button_Clicked2Async(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(false));
        }
    }
}