using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using MobileApp.Models;
using MobileApp.API;
using Xamarin.Forms;
using MobileApp.Views;
using System.Windows.Input;

namespace MobileApp
{
	public partial class MainPage : ContentPage
    {
        private bool _isRefreshing = false;
        private bool isOnline = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public MainPage(bool isOnline)
		{
			InitializeComponent();
            this.isOnline = isOnline;
            listView.IsRefreshing = IsRefreshing;           
            listView.RefreshCommand = RefreshCommand;
            listView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                if (e == null) return;
                await Navigation.PushAsync(new TaskMainTabbed(listView.SelectedItem as Models.Task));// de-select the row
                ((ListView)sender).SelectedItem = null;
            };
            RefreshCommand.Execute(null);
        }


        void Upload(object sender, ItemTappedEventArgs e)
        {
            string result = "";
            for (int i = 0; i< GetInfo.tasks.Count; i ++)
                if (GetInfo.tasks[i].Photos != null)
                    for (int j = 0; j < GetInfo.tasks[i].Photos.Count; j++)
                        result += GetInfo.tasks[i].Photos[j] + "\r\n";
            DependencyService.Get<IMessage>().LongAlert(result);
            //FTPClient.UploadFile();
        }

        public  ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    listView.IsRefreshing = true;
                    if (isOnline)
                        await GetInfo.GetInfoAsync();
                    else
                        GetInfo.GetInfoOffline();
                    listView.ItemsSource = GetInfo.tasks;
                    listView.IsRefreshing = false;
                });
            }
        }

        void RefreshLv(object sender, ItemTappedEventArgs e)
        {
            RefreshCommand.Execute("");
        }

    }
}
