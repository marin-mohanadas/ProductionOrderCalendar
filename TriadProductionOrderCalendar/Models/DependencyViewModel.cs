using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriadProductionOrderCalendar.Models
{
    public class DependencyViewModel : IGanttDependency
    {
        //public DependencyType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int DependencyID { get; set; }
        public int PredecessorID { get; set; }
        public int SuccessorID { get; set; }
        public DependencyType Type { get; set; }

        public GanttDependency ToEntity()
        {
            return new GanttDependency
            {
                ID = DependencyID,
                PredecessorID = PredecessorID,
                SuccessorID = SuccessorID,
                Type = (int)Type
            };
        }
    }
}