using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS_V2
{
    public static class UserSession
    {
        private static int userId;

        public static void Login(int loggedInUserId)
        {
            userId = loggedInUserId;
        }

        public static int GetUserId()
        {
            return userId;
        }
    }
}
