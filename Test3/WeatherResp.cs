using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
    class WeatherResp
    {
        public TempInfo Main { get; set; }
        public Times sys { get; set; }
        public string Name { get; set; }
        public double Timezone { get; set; }
    }
}
