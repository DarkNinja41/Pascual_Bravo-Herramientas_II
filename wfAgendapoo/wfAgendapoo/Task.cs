using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfAgendapoo
{
    class Task
    {
        public string taskid { get; set; }
        public string description { get; set; }
        public string priority { get; set; }
        public DateTime duedate { get; set; }
        public string status { get; set; }

        public Task(string taskid, string description, string priority, DateTime duedate, string status)
        {
            this.taskid = taskid;
            this.description = description;
            this.priority = priority;
            this.duedate = duedate;
            this.status = status;
        }
    }
}
