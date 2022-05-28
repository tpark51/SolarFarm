using System;
using System.Collections.Generic;
using System.Text;


namespace SolarFarm.Core.DTO
{
    public class Panel
    {
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime YearInstalled { get; set; }
        public Material Material { get; set; }
        public bool IsTracking { get; set; }
        //public string LookUp { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($" {Row}    {Column}  {YearInstalled:yyyy}  {Material}  {IsTracking}");
            return builder.ToString();
        }
    }
}
