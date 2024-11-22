using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonderPlane.Shared
{
    public class FlightsFoundDTO
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public DateTime ArriveDate { get; set; }
        public string ArriveTime { get; set; }
        public int FlightStatus { get; set; }
        public bool IsInternational { get; set; }
        public decimal BagPrice { get; set; }
        public string FlightCode { get; set; }
        public int Duration { get; set; }
        public decimal FirstClassPrice { get; set; }
        public decimal FirstClassPricePromotion { get; set; }
        public decimal EconomicClassPrice { get; set; }
        public decimal EconomicClassPricePromotion { get; set; }
        public bool HasPromotion { get; set; }
        public string CodePromotion { get; set; }
        public int DiscountPercentage { get; set; }
        public string PromotionDescription { get; set; }
        public int AvailableSeats { get; set; }
        public List<object> Promotions { get; set; }
        public List<object> News { get; set; }
        public List<object> Seats { get; set; }
        public List<object> FlightRecommendations { get; set; }
    }
}
