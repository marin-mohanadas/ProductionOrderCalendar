using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriadProductionOrderCalendar.Models
{
    public class TaskViewModel : IGanttTask
    {
        /**** Default Properties ****/
        public int TaskID { get; set; }
        public int? ParentID { get; set; }

        public string Title { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public bool Summary { get; set; }
        public bool Expanded { get; set; }
        public decimal PercentComplete { get; set; }
        public int OrderId { get; set; }

        /****  Custom Properties ****/

        public decimal ProductionOrder { get; set; }
        public string OrderNo { get; set; }
        public string Parent_ID { get; set; }

        public mh_tbl_gannt_chart ToEntity()
        {
            return new mh_tbl_gannt_chart
            {
                prod_order_number = ProductionOrder,
                order_date = start,
                required_date = end,
                name = Title,
                perc_ready = PercentComplete   
            };
        }

    }
}