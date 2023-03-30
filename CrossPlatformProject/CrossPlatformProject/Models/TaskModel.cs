using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class TaskModel
    {
        public TaskModel(string assignedToName, string assignedToUid, string createdByName, string createdByUid, string description, bool done, string taskUid)
        {
            this.assignedToName = assignedToName;
            this.assignedToUid = assignedToUid;
            this.createdByName = createdByName;
            this.createdByUid = createdByUid;
            this.description = description;
            this.done = done;
            this.taskUid = taskUid;
        }

        public TaskModel(string description, bool done)
        {
            this.description = description;
            this.done = done;
        }

        public TaskModel(string description, string assignedToUid)
        {
            this.description = description;
            this.assignedToUid = assignedToUid;
        }

        public TaskModel(bool done)
        {
            this.done = done;
        }

        public TaskModel()
        {

        }


        public string assignedToName { get; set; }
        public string assignedToUid { get; set; }
        public string createdByName { get; set; }
        public string createdByUid { get; set; }
        public string description { get; set; }
        public bool done { get; set; }
        public string taskUid { get; set; }

    }
}
