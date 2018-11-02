using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoardAPI
{
    class NonCashCollectionException : Exception
    {
        public string ErrorDesc = "NonCashCollectionException";
    }
}
