
namespace WonderPlane.Client.Utils
{
    public class FlightDuration
    {
        private static Dictionary<string, int> internationalFlightDurations = new Dictionary<string, int>
        {
            { "Londres (LON) - Reino Unido (UK):Bogotá (BOG) - Colombia (CO)", 658 },
            { "Londres (LON) - Reino Unido (UK):Cali (CLO) - Colombia (CO)", 760 },
            { "Londres (LON) - Reino Unido (UK):Medellín (MDE) - Colombia (CO)", 960 },
            { "Londres (LON) - Reino Unido (UK):Cartagena (CTG) - Colombia (CO)", 840 },
            { "Londres (LON) - Reino Unido (UK):Pereira (PEI) - Colombia (CO)", 1020 },
            { "New York (NYC) - Estados Unidos (US):Bogotá (BOG) - Colombia (CO)", 360 },
            { "New York (NYC) - Estados Unidos (US):Cali (CLO) - Colombia (CO)", 343 },
            { "New York (NYC) - Estados Unidos (US):Medellín (MDE) - Colombia (CO)", 345 },
            { "New York (NYC) - Estados Unidos (US):Cartagena (CTG) - Colombia (CO)", 300 },
            { "New York (NYC) - Estados Unidos (US):Pereira (PEI) - Colombia (CO)", 343 },
            { "Madrid (MAD) - España (ES):Bogotá (BOG) - Colombia (CO)", 625 },
            { "Madrid (MAD) - España (ES):Cali (CLO) - Colombia (CO)", 780  },
            { "Madrid (MAD) - España (ES):Medellín (MDE) - Colombia (CO)", 600 },
            { "Madrid (MAD) - España (ES):Cartagena (CTG) - Colombia (CO)", 800 },
            { "Madrid (MAD) - España (ES):Pereira (PEI) - Colombia (CO)", 720 },
            { "Buenos Aires (BUE) - Argentina (AR):Bogotá (BOG) - Colombia (CO)", 390  },
            { "Buenos Aires (BUE) - Argentina (AR):Cali (CLO) - Colombia (CO)", 510 },
            { "Buenos Aires (BUE) - Argentina (AR):Medellín (MDE) - Colombia (CO)", 400 },
            { "Buenos Aires (BUE) - Argentina (AR):Cartagena (CTG) - Colombia (CO)", 680 },
            { "Buenos Aires (BUE) - Argentina (AR):Pereira (PEI) - Colombia (CO)", 540  },
            { "Miami (MIA) - Estados Unidos (US):Bogotá (BOG) - Colombia (CO)", 220 },
            { "Miami (MIA) - Estados Unidos (US):Cali (CLO) - Colombia (CO)", 235 },
            { "Miami (MIA) - Estados Unidos (US):Medellín (MDE) - Colombia (CO)", 220 },
            { "Miami (MIA) - Estados Unidos (US):Cartagena (CTG) - Colombia (CO)", 180 },
            { "Miami (MIA) - Estados Unidos (US):Pereira (PEI) - Colombia (CO)", 180 },
        };

        public static int CalculateDuration(string origin, string destination, bool isInternational)
        {
            if (isInternational)
            {
                var routeKey1 = $"{origin}:{destination}";
                var routeKey2 = $"{destination}:{origin}";

                if (internationalFlightDurations.TryGetValue(routeKey1, out int durationHours) ||
                    internationalFlightDurations.TryGetValue(routeKey2, out durationHours))
                {
                    return durationHours;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 60; 
            }
        }

        public static string FormatDuration(int durationMinutes)
        {
            int hours = durationMinutes / 60;
            int minutes = durationMinutes % 60;
            if (minutes > 0)
            {
                return $"{hours} h {minutes} min";
            }
            else if (durationMinutes == 0)
            {
                return "";
            }
            else
            {
                return $"{hours} h";
            }
        }
    }
}
