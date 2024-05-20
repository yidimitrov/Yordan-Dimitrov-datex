namespace WarehouseWebApi.Models.Business
{
    public class Pallet
    {
        public string Barcode { get; set; }

        public List<Box> Boxes { get; set; }

        public override string ToString()
        {
            return $"pallet: {Barcode}";
        }
    }
}
