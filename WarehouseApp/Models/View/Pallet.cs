namespace WarehouseWebApi.Models.View
{
    public class Pallet
    {
        public string Barcode { get; set; }

        public Box[] Boxes { get; set; }

        public override string ToString()
        {
            return $"pallet: {Barcode}";
        }
    }
}
