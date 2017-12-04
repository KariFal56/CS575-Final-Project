using System;
using SQLite;

namespace DefectReport
{
    public class DefectReportItem
    {
        //Class to contain defect report item and table setup information
        //name of the table is DefectReportItem
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public int Count { get; set; }
        public string Defect { get; set; }
        public string Disposition { get; set; }
        public DateTime Date { get; set; }
        public bool UseDefect { get; set; }  //if defect should be used to generate quick select list - Default = True
    }
}
