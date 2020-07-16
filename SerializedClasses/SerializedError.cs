using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDashboard
{
    class SerializedError
    {
        int errorType = 404;
        string errorMessage;

        public SerializedError(int errorType, string errorMessage)
        {
            this.errorType      = errorType;
            this.errorMessage   = errorMessage;
        }

    }
}
