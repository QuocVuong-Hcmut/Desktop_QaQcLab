using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class EnduranceSettingParameter
    {
        public int Id { get; set; }
        public Single CompressionForceSettingsystem12 { get; set; }
        public Single CompressionForceSettingsystem3 { get; set; }
        public int TimeOccupying12 { get; set; }
        public int TimeOccupying3 { get; set; }
        public Int16 NumberClick12 { get; set; }
        public Int16 NumberClick3 { get; set; }
        public bool system12 { get; set; }
        public bool system3 { get; set; }

        public void Clone(EnduranceSettingParameter entry)
        {
            this.Id = entry.Id;
            this.CompressionForceSettingsystem12 = entry.CompressionForceSettingsystem12;
            this.CompressionForceSettingsystem3 = entry.CompressionForceSettingsystem3;
            this.TimeOccupying12 = entry.TimeOccupying12;
            this.TimeOccupying3 = entry.TimeOccupying3;
            this.NumberClick12 = entry.NumberClick12;
            this.NumberClick3 = entry.NumberClick3;
            this.system12 = entry.system12;
            this.system3 = entry.system3;
        }
    }
}
