using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PriceListsInfo.Models
{
    public class PriceList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PriceListID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Дата создания обязательна")]
        [Column("CreatedDate")]
        public DateTime Date { get; set; }

        public List<PriceListColumn> Columns { get; set; } = new List<PriceListColumn>();
    }
}
