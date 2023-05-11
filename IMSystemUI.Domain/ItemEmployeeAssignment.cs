using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IMSystemUI.Domain
{
    public class ItemEmployeeAssignment
    {
        public Guid AssigmentId { get; set; }
        public Guid ItemEmployeeCode { get; set; }

        [DisplayName("IssuerBy")]
        public User? IssuerBy { get; set; }

        [DisplayName("ReceiverBy")]
        public User? ReceiverBy { get; set; }
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        [DisplayName("Taken Date"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateTaken { get; set; } = DateTime.Now;
        public string IssueSignature { get; set; } = string.Empty;

        public string ReceiverSignature { get; set; } = string.Empty;

        [DisplayName("Return Date"), DisplayFormat(DataFormatString = "{0:M/d/yy}")]
        public DateTime? DateReturned { get; set; }

        [DisplayName("Return Condition")]
        public string? ReturnedCondition { get; set; }

        [DisplayName("Reason For Not Return")]
        public string? ResonForNotReturn { get; set; }
        [DisplayName("Is Returned")]
        public bool IsReturned { get; set; } = false;
    }
}
