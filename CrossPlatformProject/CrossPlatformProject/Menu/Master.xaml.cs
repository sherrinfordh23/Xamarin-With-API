using CrossPlatformProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformProject.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : FlyoutPage
    {
        public Master()
        {
            InitializeComponent();
        }

        private void btnGetAllUsers_Clicked(object sender, EventArgs e)
        {
            Detail = new GetAllUsersPage();
        }

        private void btnCreateATask_Clicked(object sender, EventArgs e)
        {
            Detail = new CreateATask();
        }

        private void btnGetTasksCreatedBy_Clicked(object sender, EventArgs e)
        {
            Detail = new GetTasksCreatedBy();
        }

        private void btnGetTasksAssignedTo_Clicked(object sender, EventArgs e)
        {
            Detail = new GetTasksAssignedTo();
        }
    }
}