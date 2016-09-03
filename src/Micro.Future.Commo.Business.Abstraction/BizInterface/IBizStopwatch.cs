using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IBizStopwatch
    {
        void Start();

        void Stop();

        void Restart();

        int GetElapsedMillseconds();
    }
}
