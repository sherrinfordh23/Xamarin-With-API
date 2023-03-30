using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class PatchRequest : TaskModel
    {
        public PatchRequest(bool done) :
            base(done)
        {
        }
    }
}
