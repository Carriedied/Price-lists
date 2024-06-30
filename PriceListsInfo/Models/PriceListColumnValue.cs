using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PriceListsInfo.Models
{
    public class PriceListColumnValue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PriceListColumns")]
        public int PriceListColumnId { get; set; }

        public int RowIndex { get; set; }

        public string Value { get; set; }

        public PriceListColumn PriceListColumn { get; set; }
    }
}
