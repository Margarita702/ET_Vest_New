using System.ComponentModel.DataAnnotations;

namespace ET_Vest.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public List<PrintedEditionProvider> PrintEditionProviders { get; set; }
    }

}
