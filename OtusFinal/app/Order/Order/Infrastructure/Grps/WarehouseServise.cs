

using Application.Interfaces;
using static Grps.BillingServiseGrps;
using static Grps.WarehouseServiseGrps;
using static System.Net.Mime.MediaTypeNames;

namespace Grps
{
    public class WarehouseServise : IWarehouse
    {
        readonly WarehouseServiseGrpsClient _warehouseServiseGrps;
        public WarehouseServise(WarehouseServiseGrpsClient warehouseServiseGrps)
        {
            _warehouseServiseGrps = warehouseServiseGrps;
        }

        public async Task<int> AddReserveAsync(int productId)
        {
            try
            {
                var result = await _warehouseServiseGrps.AddReserveAsync(
                     new ProductId
                     {
                         ProductId_ = productId
                     });
                return result.ReserveId;

            }
            catch (Exception ex) 
            {
                return -1;
            }

        }


        public async Task<int> DeleteReserveAsync(int reserveId)
        {
            try
            {
                var result = await _warehouseServiseGrps.DeleteReserveAsync(
                 new Reserve
                 {
                     ReserveId = reserveId,
                 });
                return result.Status;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

}
