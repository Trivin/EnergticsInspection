using MobileApp.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Models;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using MobileApp.Methods;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskMainTabbed : TabbedPage
    {

        Models.Task Source;
        public TaskMainTabbed (Models.Task Source)
        {
            InitializeComponent();
            this.Source = Source;
            BindingContext = this.Source;
            Children.Add(new DefectsTabbed(Source));
            TabbedPage ResourcesTabbedPage = new TabbedPage() {Title = "Ресурсы" };
            ResourcesTabbedPage.Children.Add(new ResourcePage(Source.Resources.Where(x => (x.ResourceType == ResourceType.Materials)).ToList())
            {
                Title = "Материалы"
            }
            );
            ResourcesTabbedPage.Children.Add(new ResourcePage(Source.Resources.Where(x => (x.ResourceType == ResourceType.Device)).ToList())
            {
                Title = "Машины"
            }
            );
            ResourcesTabbedPage.Children.Add(new ResourcePage(Source.Resources.Where(x => (x.ResourceType == ResourceType.Workforce)).ToList())
            {
                Title = "Трудовые ресурсы",
            }
            );
            Children.Add(ResourcesTabbedPage);
            //ToolbarItems.Add(new ToolbarItem {Text = "Сделать фото", Order = ToolbarItemOrder.Primary, Priority=1, Parent = this, Icon= "photo.png" });
            ToolbarItem tl = new ToolbarItem { Text = "Начать", Order = ToolbarItemOrder.Primary, Priority = 0, Parent = this, Icon = Source.Started?"stop.png":"start.png" };
            tl.Clicked += Start; 
            ToolbarItems.Add(tl);
            ToolbarItems[0].Clicked += async (o, e) =>
              {
                  try
                  {
                      var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                      var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                      if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                      {
                          var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                          cameraStatus = results[Permission.Camera];
                          storageStatus = results[Permission.Storage];
                      }

                      if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                      {
                          if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                          {
                              MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                              {
                                  SaveToAlbum = true,
                                  Directory = "Sample",
                                  Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                              });

                              if (file == null)
                                  return;
                              else
                                  Source.Photos.Add(file.AlbumPath);
                          }
                      }
                      else
                      {
                          //On iOS you may want to send your user to the settings screen.
                          //CrossPermissions.Current.OpenAppSettings();
                      }
                  }
                  catch
                  {

                  }
              };
            JobsListView.ItemsSource = this.Source.Jobs;
            listView.ItemsSource = this.Source.Units;
            JobsListView.ItemTapped += GetInfo.TappedItem;
            listView.ItemTapped += GetInfo.TappedItem;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void Photo(object sender, ItemTappedEventArgs e)
        {
           
        }


            private void Start(object sender, EventArgs e)
        {
            if (!Source.Started)
            {
                (sender as ToolbarItem).Icon = "stop.png";
                (sender as ToolbarItem).Text = "Стоп";
            }
            else
            {
                (sender as ToolbarItem).Icon = "start.png";
                (sender as ToolbarItem).Text = "Старт";
                Source.FactDate = DateTime.Now;
                factDate.Text = DateTime.Now.ToString();
            }
            Source.Started = !Source.Started;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as Entry).Parent.ToString();
        }
    }
}