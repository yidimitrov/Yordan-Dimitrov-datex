using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseWebApi.Models.Db.Entity
{
    [Table("boxes")]
    public class BoxEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("barcode")]
        public string Barcode { get; set; }

        public ICollection<BoxEntity> Boxes { get; set; } = new List<BoxEntity>();

        [Column("owner_id")]
        public int? OwnerId { get; set; }

        public BoxEntity Owner { get; set; }

        [Column("pallet_id")]
        public int PalletId { get; set; }

        public PalletEntity Pallet { get; set; } = null!;
    }
}
