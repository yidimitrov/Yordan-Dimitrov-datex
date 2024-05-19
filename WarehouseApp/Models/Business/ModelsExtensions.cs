namespace WarehouseWebApi.Models.Business
{
    internal static class ModelsExtensions
    {
        internal static void BuidDataIntegrity(IEnumerable<Box> boxes, Box owner, Pallet pallet)
        {
            foreach (var box in boxes)
            {
                BuidDataIntegrity(box.Boxes, box, pallet);

                box.Owner = owner;
                box.Pallet = pallet;
            }
        }
    }
}
