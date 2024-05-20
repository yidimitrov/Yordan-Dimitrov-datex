namespace WarehouseWebApi.Models.View
{
    public class Box
    {
        public string Barcode { get; set; }

        public Box[] Boxes { get; set; }

        public override string ToString()
        {
            return $"box: {Barcode}";
        }
    }
}
