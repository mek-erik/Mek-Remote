using System;
using System.Collections.Generic;
using System.Text;

namespace TVRequest
{
    interface ITeamViewer
    {
        void Start();
        bool IsRunning();
        void Stop();
        void Restart();

    }
}
