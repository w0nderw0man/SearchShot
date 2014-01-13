using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    class WebService
    {
        public static Service1Client Service
        {
        get;
        set;
        }

        public async void InitService()
        {
            Service = new Service1Client();
        }

    }
}
