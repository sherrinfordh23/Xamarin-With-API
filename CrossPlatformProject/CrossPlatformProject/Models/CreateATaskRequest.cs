using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class CreateATaskRequest : TaskModel
    {

        public CreateATaskRequest(string description, string assignedToUid) : 
            base (description, assignedToUid)
        {
        }

    }
}
