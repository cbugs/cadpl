using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount
{
    public class Codes
    {
        // Registration
        public const int EMAIL_ALREADY_USED = 409002;

        // Login & Trigger reset password
        public const int INVALID_CREDENTIALS = 400001;

        // Change password
        public const int OLD_PASSWORD_WRONG = 401001;

        // User info
        public const int ATTRIBUTES_NOT_ALLOWED = 403000;

        // Finalize Reset password
        public const int BAD_REQUEST = 400000;

        // Common
        public const int GENERAL_ERROR = 0;
        public const int UNAUTHORIZED = 403000;
    }
}
