using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Android.Widget;
using MobileApp.Models;
using RestSharp;
using Xamarin.Forms;

namespace MobileApp.API
{
    public static class GetInfo
    {
        public static List<Task> tasks;
        public static async System.Threading.Tasks.Task GetInfoAsync()
        {
            var client = new RestClient("http://185.26.171.127:2756/se_mosin/hs/api/v1/RepairTask?userid=842a3691-8bae-11e2-bc34-5ef3fcdca583");
            var request = new RestRequest(Method.GET);

            string obj = "[ { \"Id\": 1, \"Description\": \"Распоряжение №123 ВЛ 0,4кв от ТП Яндыки - выправка опоры\", \"MaintenanceType\": { \"key\": \"3\", \"value\": \"Капитальный ремонт\" }, \"PlainDate\": \"2018-12-21T00:00:00\", \"Gps\": { \"x\": \"\", \"y\": \"\" }, \"ComplexObject\": { \"key\": \"1\", \"value\": \"ВЛ 0,4кв от ТП Яндыки\" }, \"Resources\": [ { \"Resource\": { 	\"key\": \"1\", 		\"value\": \"Электромонтер второго разряд\" }, \"Job\": { \"key\": 1, \"value\": \"Выправка деревянной А-образной опоры при отклонении от вертикальной оси вдоль линии ВЛ напряжением 0,38 кВ\" }, \"ResourceType\": 1, \"QuantityPlain\": 3, \"QuantityFact\": 3, \"Measure\": \"чел/час\" }, { \"Resource\": { 	\"key\": \"1\", 		\"value\": \"Электромонтер второго разряд\" }, \"Job\": { \"key\": 1, \"value\": \"Выправка деревянной А-образной опоры при отклонении от вертикальной оси вдоль линии ВЛ напряжением 0,38 кВ\" }, \"ResourceType\": 1, \"QuantityPlain\": 3, \"QuantityFact\": 3, \"Measure\": \"чел/час\" }, { \"Resource\": { 	\"key\": \"1\", 		\"value\": \"Электромонтер второго разряд\" }, \"Job\": { \"key\": 1, \"value\": \"Выправка деревянной А-образной опоры при отклонении от вертикальной оси вдоль линии ВЛ напряжением 0,38 кВ\" }, \"ResourceType\": 1, \"QuantityPlain\": 3, \"QuantityFact\": 3, \"Measure\": \"чел/час\" } ], \"Jobs\": [ { \"Job\": { \"key\": \"1\", \"value\": \"Выправка деревянной А-образной опоры при отклонении от вертикальной оси вдоль линии ВЛ напряжением 0,38 кВ\" }, \"JobCode\": \"0001\", \"QuantityPlain\": \"2\", \"QuantityFact\": \"2\", \"Measure\": \"шт\" } ], \"Defects\": [ { \"Completed\": true, \"Job\": { \"key\": 1, \"value\": \"Выправка деревянной А-образной опоры при отклонении от вертикальной оси вдоль линии ВЛ напряжением 0,38 кВ\" }, \"Unit\": { \"key\": 1, \"value\": \"Опора №1\" }, \"MeasurementGroup\": { \"key\": 1, \"value\": \"Общие дефекты опор\" }, \"DefectType\": { \"key\": 1, \"value\": \"Отклонение опоры вдоль оси ВЛ\" }, \"Quantity\": 1 } ] } ]";
            var cancellationTokenSource = new CancellationTokenSource();
            try
            {
                tasks = await ExecuteTaskAsync<List<Task>>(client, request);
                obj = Newtonsoft.Json.JsonConvert.SerializeObject(tasks);
            }
            catch
            {

            }
            if (App.Current.Properties.ContainsKey("obj"))
                App.Current.Properties["obj"] = obj;
            else
                App.Current.Properties.Add("obj", obj);
        }

        public static void GetInfoOffline()
        {
            string obj = "";
            if (App.Current.Properties.ContainsKey("obj"))
                obj = App.Current.Properties["obj"].ToString();
            if (obj!="")
                tasks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Task>>(obj);
        }

        public static void TappedItem(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        public static System.Threading.Tasks.Task<T> ExecuteTaskAsync<T>(this RestClient @this, RestRequest request)
           where T : new()
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new System.Threading.Tasks.TaskCompletionSource<T>();

            @this.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ErrorException != null)
                    tcs.TrySetException(response.ErrorException);
                else
                    tcs.TrySetResult(response.Data);
            });

            return tcs.Task;
        }
    }
}
