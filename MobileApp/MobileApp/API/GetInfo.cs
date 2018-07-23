using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Android.Widget;
using MobileApp.Models;
using RestSharp;
using System.Net.Http;
using Xamarin.Forms;
using System.Linq;
using System.IO;

namespace MobileApp.API
{
    public static class GetInfo
    {
        public static List<Task> tasks;
        public static MainPage page;
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
            if (App.Current.Properties["obj"].ToString()=="")
                App.Current.Properties["obj"] = obj;
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
                string rr = response.Content;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    tcs = null;
                else
                    tcs.TrySetResult(response.Data);
            });

            return tcs.Task;
        }


/* Необъединенное слияние из проекта "MobileApp.Android"
До:
        public static async System.Threading.Tasks.Task SendInfoAsync()
После:
        public static async System.Threading.Tasks.Task SendInfo()
*/
        public static async System.Threading.Tasks.Task SendInfoAsync()
        {
            var client = new RestClient("http://185.26.171.127:2756/se_mosin/hs/api/v1/RepairTask?userid=842a3691-8bae-11e2-bc34-5ef3fcdca583");
            var request = new RestRequest(Method.POST);
            string sl = Newtonsoft.Json.JsonConvert.SerializeObject(tasks.Where(x => x.Compl == true).ToList());
            request.AddParameter("application/json", sl,ParameterType.RequestBody);
            //request.AddJsonBody(sl);
            //try
            //{
            //    var obj = await ExecuteTaskAsync<RestResponse>(client,request);
            //    if (obj.StatusCode == System.Net.HttpStatusCode.OK)
            //        success();
            //}
            //catch
            //{
            //    notsuccess();
            //}
            //var client2 = new RestClient("http://185.26.171.127:2756/se_mosin/hs/api/v1/RepairTaskPhoto?userid=842a3691-8bae-11e2-bc34-5ef3fcdca583");
            //var request2 = new RestRequest(Method.POST);
            var taskList = tasks.Where(x => x.Compl == true).ToList();
            //for (int i = 0; i < taskList.Count; i++)
            //    if (taskList[i].Photos != null)
            //        for (int j = 0; j < taskList[i].Photos.Count; j++)
            //        {
            //            request2.AddFile("Файлиг", taskList[i].Photos[j], "image/jpeg");
            //        }
            //try
            //{
            //    var obj2 = client2.Execute(request2);
            //    string sss = obj2.Content;
            //    if (obj2 != null)
            //    {
            //        if (obj2.StatusCode == System.Net.HttpStatusCode.OK)
            //            success();
            //    }
            //    else
            //        notsuccess();
            //}
            //catch
            //{
            //
            //}

            for (int i = 0; i < taskList.Count; i++)
                if (taskList[i].Photos != null)
                    for (int j = 0; j < taskList[i].Photos.Count; j++)
                    {
                        var file = taskList[i].Photos[j];

                        try
                        {
                            var upfilebytes = File.ReadAllBytes(file);

                            HttpClient client3 = new HttpClient();
                            ByteArrayContent content = new ByteArrayContent(upfilebytes);
                            HttpRequestMessage reqe = new HttpRequestMessage(HttpMethod.Post, "http://185.26.171.127:2756/se_mosin/hs/api/v1/RepairTaskPhoto?id=842a3691-8bae-11e2-bc34-5ef3fcdca583");
                            reqe.Content = content;
                            var response =
                                await client3.SendAsync(reqe);
                            var responsestr = response.Content.ReadAsStringAsync().Result;


                        }
                        catch (Exception e)
                        {

                            return;
                        }
                    }

        }
        private static void success()
        {
            DependencyService.Get<IMessage>().ShortAlert("Удачно отправлено данных!");
        }
        private static void notsuccess()
        {
            DependencyService.Get<IMessage>().ShortAlert("Неудачная отправка данных!");
        }
    }
}
