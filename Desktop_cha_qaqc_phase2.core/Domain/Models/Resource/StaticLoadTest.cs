using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class StaticLoadTest : Test
    {
        public IList<StaticLoadTestSample>? TestSheets { get; set; }

    }
}
