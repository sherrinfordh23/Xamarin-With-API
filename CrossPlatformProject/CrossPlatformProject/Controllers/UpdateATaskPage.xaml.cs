using CrossPlatformProject.Data;
using CrossPlatformProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformProject.Controllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateATaskPage : ContentPage
    {
        TaskModel clickedTask;
        public UpdateATaskPage(TaskModel tsk)
        {
            InitializeComponent();
            clickedTask = tsk;
            FillTaskDetails();
        }


        private void FillTaskDetails()
        {
            lblCreatedBy.Text = "Created by: " + clickedTask.createdByName;
            lblDescription.Text = "Task description: " + clickedTask.description;
            lblDone.Text = "Status: " + clickedTask.done.ToString();

            switchDone.IsToggled = clickedTask.done;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var method = new HttpMethod("PATCH");

            PatchRequest patchRequest = new PatchRequest(switchDone.IsToggled);
            StringContent jsonContent = new StringContent(
                 JsonConvert.SerializeObject(new PatchRequest(switchDone.IsToggled)),
                 Encoding.UTF8,
                 "application/json"
                 );

            clickedTask.done = switchDone.IsToggled;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            Uri uri = new Uri($"{DataSource.baseUrl}tasks/{clickedTask.taskUid}");
            var request = new HttpRequestMessage(method, uri);
            request.Content = jsonContent;
            HttpResponseMessage response = await client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var values = JObject.Parse(content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }

            foreach(var task in DataSource.listOfAssignedTasks)
            {
                if (task.taskUid.Equals(clickedTask.taskUid))
                    task.done = clickedTask.done;
            }
            await Navigation.PopAsync();
        }

        private void switchDone_Toggled(object sender, ToggledEventArgs e)
        {
            lblDone.Text = lblDone.Text = "Status: " + switchDone.IsToggled.ToString();
        }
    }
}