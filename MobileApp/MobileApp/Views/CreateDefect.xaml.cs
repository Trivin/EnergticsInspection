
using MobileApp.API;
using MobileApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateDefect : ContentPage
	{
        private Defect defect;
        List<Defect> task;
        DefectsTab page;
        List<string> ks = new List<string>()
            {
                "Оборудование №1","Оборудование №2","Оборудование №3","Оборудование №4","Оборудование №5"
            };

        List<string> kss = new List<string>()
            {
                "Дефект №1","Дефект №2","Дефект №3","Дефект №4","Дефект №5"
            };
        public CreateDefect (Models.Task task, DefectsTab page, Defect defect = null)
		{
			InitializeComponent ();
            this.task = task.Defects;
            this.page = page;
            sads.ItemsSource = task.Units.Select(x=>x.Value);
            Defect.TextChanged += Defect_TextChanged;
            Defect.ItemsSource = kss;
            sads.SortingAlgorithm = SortingAlgorithm;
            Defect.SortingAlgorithm = SortingAlgorithm;
            if (defect!=null)
            {
                this.defect = defect;
                Defect.Text = this.defect.DefectType.Value;
                sads.Text = this.defect.Unit.Value;
                Group.Text =  this.defect.MeasurementGroup.Value;
                Kolvo.Text =  this.defect.QuantityFact.ToString();
            }
            RegistrationBtn.Clicked += RegistrationBtn_Clicked;
            Defect.Threshold = 3;
            Delete.Clicked += Delete_Clicked;
		}

        private void Defect_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Defect.Text.Length>3 && kss.Where(x => x.ToLower().StartsWith(e.NewTextValue.ToLower())).ToList().Count==0)
            {
                DependencyService.Get<IMessage>().ShortAlert(string.Format("\"{0}\" нет в списке!",Defect.Text));
                Defect.Text = "";
                return;
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            if (defect!=null)
            { 
                task.Remove(defect);
                page.Refresh();
                Navigation.PopAsync();
            }
        }

        private void RegistrationBtn_Clicked(object sender, EventArgs e)
        {

             if (Defect.Text == null || sads.Text == null || Group.Text == null || Kolvo.Text == null)
             {
                 DependencyService.Get<IMessage>().LongAlert("Не все поля заполнены!");
                 return;
             }
             if (defect!=null)
             {
                 this.defect.DefectType.Value = Defect.Text;
                 this.defect.Unit.Value = sads.Text;
                 this.defect.MeasurementGroup.Value = Group.Text;
                 this.defect.QuantityFact = Convert.ToInt32(Kolvo.Text);
                 page.Refresh();
                 Navigation.PopAsync();
             }
             else
             {
                 Defect def = new Defect();
                 def.DefectType = new SimpleRef() { Value = Defect.Text };
                 def.Unit= new SimpleRef() { Value = sads.Text                       } ;
                 def.MeasurementGroup= new SimpleRef() { Value = Group.Text          } ;
                 def.QuantityFact = 0;
                 def.IsNew = true;
                 def.Job = new SimpleRef() { Value = "" };
                 def.QuantityPlan = Convert.ToInt32(Kolvo.Text); ;
                 def.Completed = false;
                 task.Add(def);
                 page.Refresh();
                 Navigation.PopAsync();
             }
        }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
    .Where(x => x.ToLower().StartsWith(text.ToLower()))
    .OrderBy(x => x)
    .ToList();
    }
}