using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PriceListsInfo.Models
{
    public enum ColumnType
    {
        Number,
        SingleLineText,
        MultiLineText
    }

    public class PriceListColumn
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название колонки обязательно")]
        public string ColumnName { get; set; }

        [Required(ErrorMessage = "Тип колонки обязателен")]
        public ColumnType ColumnType { get; set; }

        [ForeignKey("PriceLists")]
        public int PriceListId { get; set; }

        public PriceList PriceList { get; set; }

        public List<PriceListColumnValue> Values { get; set; } = new List<PriceListColumnValue>();
    }
}
