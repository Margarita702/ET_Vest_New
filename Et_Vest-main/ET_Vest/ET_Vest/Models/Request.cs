using ET_Vest.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ET_Vest.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }

        public int RequestedQuantity { get; set; }

        public string Status { get; set; }

        [EnumDataType(typeof(Cathegory))]
        public Cathegory Cathegory { get; set; }

        public int TradeObjectId { get; set; }
        public TradeObject TradeObject { get; set; }

        public int PrintedEditionId { get; set; }
        public PrintedEdition PrintedEdition { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

    }
}