using MobileApp.API;
using MobileApp.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MobileApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var nav = new NavigationPage(new LoginPage());
            MainPage = nav;
		}

		protected override void OnStart ()
		{

        }

       
		protected override void OnSleep ()
		{
           //string res = JsonConvert.SerializeObject(GetInfo.tasks);
           //App.Current.Properties["obj"] = res;
        }

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
