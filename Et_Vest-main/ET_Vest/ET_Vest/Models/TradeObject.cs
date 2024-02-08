using System.ComponentModel.DataAnnotations;

namespace ET_Vest.Models
{
    public class TradeObject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Employee { get; set; }
        public List<Request> Requests { get; set; }
    }
}
