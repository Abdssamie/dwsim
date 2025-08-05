using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.UI.Web.Settings
{
    public static class DashboardSettings
    {
        public static string DashboardServiceUrl = "https://dashboard-service.simulate365.com";
        public static string ExcelRunnerServiceUrl = "https://excel-runner-service.simulate365.com";
        public static string SensitivityStudiesServiceUrl = "https://sensitivity-study-service.simulate365.com";
        public static string TakeHomeExamsServiceUrl = "https://take-home-exams-service.simulate365.com";
        public static string Environment = "Production";

        static DashboardSettings()
        {

            var s365Environment = ConfigurationManager.AppSettings.Get("S365Environment");
            if (!String.IsNullOrEmpty(s365Environment) && s365Environment.ToLowerInvariant() == "staging")
            {
                DashboardServiceUrl = "https://dashboard-service-staging.simulate365.com";
                ExcelRunnerServiceUrl = "https://excel-runner-service.staging.simulate365.com";
                SensitivityStudiesServiceUrl = "https://sensitivity-study-service.staging.simulate365.com";
                TakeHomeExamsServiceUrl = "https://take-home-exams-service.staging.simulate365.com";
                Environment = "Staging";
            }

        }
    }
}
