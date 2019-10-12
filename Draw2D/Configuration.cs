using System.Configuration;
using System.Globalization;

namespace Draw2D
{
    public class Configuration
    {
        public static double Epsilon { get; set; } = 0.0000001;
        public static string OutputNumberFormat { get; set; } = "#,###.##";

        public static void GetAppConfig()
        {
            string epsilonString = ConfigurationManager.AppSettings["Epsilon"];
            double.TryParse(epsilonString, NumberStyles.Number, CultureInfo.InvariantCulture, out double epsilon);
            Epsilon = epsilon;

            OutputNumberFormat = ConfigurationManager.AppSettings["OutputNumberFormat"];
        }
    }
}
