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
using System.Threading;
using MobileApp.Methods;
using Acr.UserDialogs;

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
            GetInfo.page = this;
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


        async System.Threading.Tasks.Task UploadAsync(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if ((GetInfo.tasks.Where(x => x.Compl == true).ToList()).Count==0)
                {
                    DependencyService.Get<IMessage>().LongAlert("Не выбранные данные для отправки!");
                    return;
                }
                UserDialogs.Instance.ShowLoading("Выполняется отправка данных...");
                await GetInfo.SendInfoAsync();
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IMessage>().LongAlert("Удачно отправлено!");
            }
            catch (Exception err)
            {
                DependencyService.Get<IMessage>().LongAlert("Ошибка при отправке!");
            }
            //FTPClient.UploadFile();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            string res1 = JsonConvert.SerializeObject(GetInfo.tasks);
            App.Current.Properties["obj"] = res1;
        }

        public ICommand RefreshCommand
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
