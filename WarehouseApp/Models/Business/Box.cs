namespace WarehouseWebApi.Models.Business
{
    public class Box
    {
        public int Id { get; set; }

        public string Barcode { get; set; }

        public Box[] Boxes { get; set; }

        public Box Owner { get; set; }

        public int? OwnerId { get; set; }

        public Pallet Pallet { get; set; }

        public override string ToString()
        {
            return $"box: {Barcode}, owner: {Owner}";
        }
    }
}
