using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseWebApi.Models.Db.Entity
{
    [Table("pallets")]
    public class PalletEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("barcode")]
        public string Barcode { get; set; }

        public virtual List<BoxEntity> Boxes { get; set; } = new List<BoxEntity>();
    }
}
