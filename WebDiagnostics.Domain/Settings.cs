using System.Collections.Generic;
using System.Linq;

namespace WebDiagnostics.Domain
{
    public static class Settings
    {
        private static string _adminUsers;

        public static List<string> AdminUsers
        {
            get
            {
                _adminUsers = System.Configuration.ConfigurationManager.AppSettings["AdminUsers"];

                //parse the users list
                return _adminUsers.Split(';').Select(x => x).ToList();


            }

        }

        private static string _smtpHost;
        public static string SmtpHost
        {
            get
            {
                return _smtpHost ??
                       (_smtpHost =
                           System.Configuration.ConfigurationManager.AppSettings["SmtpHost"]);
            }
            set { _smtpHost = value; }
        }


        private static string _smtpUser;
        public static string SmtpUser
        {
            get
            {
                return _smtpUser ??
                       (_smtpUser =
                           System.Configuration.ConfigurationManager.AppSettings["SmtpUser"]);
            }
            set { _smtpUser = value; }
        }


        private static string _smtpPassword;
        public static string SmtpPassword
        {
            get
            {
                return _smtpPassword ??
                       (_smtpPassword =
                           System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]);
            }
            set { _smtpPassword = value; }
        }

        private static string _defaultEmailFromAddress;
        public static string DefaultEmailFromAddress
        {
            get
            {
                return _defaultEmailFromAddress ??
                       (_defaultEmailFromAddress =
                           System.Configuration.ConfigurationManager.AppSettings["DefaultEmailFromAddress"]);
            }
            set { _defaultEmailFromAddress = value; }
        }

        private static string _defaultEmailToAddress;
        public static string DefaultEmailToAddress
        {
            get
            {
                return _defaultEmailToAddress ??
                       (_defaultEmailToAddress =
                           System.Configuration.ConfigurationManager.AppSettings["DefaultEmailToAddress"]);
            }
            set { _defaultEmailToAddress = value; }
        }

    }
}
