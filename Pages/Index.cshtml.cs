using deliveryCompany.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace deliveryCompany.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryCompanyContext _deliveryCompanyContext;

        // Constructor injection for EcommerceContext
        public IndexModel(DeliveryCompanyContext deliveryCompanyContext)
        {
            _deliveryCompanyContext = deliveryCompanyContext;
        }

        public List<Order> Orders { get; set; }

        public void OnGet(int? orderId, string? customerName, string? orderStatus, DateOnly? deliveryDate,int? driverRef, int? vehicleRef)
        {
            IQueryable<Order> ordersQuery = _deliveryCompanyContext.Orders
                .Include(p => p.Customer)
            .Include(p => p.AssignedDriver).ThenInclude(p => p.Vehicles).ThenInclude(p => p.Maintenances);
          
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                ordersQuery = ordersQuery.Where(o => EF.Functions.Like(o.Customer.CustomerName, "%" + customerName + "%"));
            }

            if (!string.IsNullOrWhiteSpace(orderStatus))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderStatus == orderStatus);
            }

            if (deliveryDate.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate == deliveryDate.Value);
            }

            if (driverRef.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.AssignedDriverId == driverRef);
            }
            if (orderId.HasValue)
            {
                ordersQuery = ordersQuery.Where(p => p.OrderId == orderId);
            }
            if (vehicleRef.HasValue)
            {
                ordersQuery = ordersQuery.Where(p => p.AssignedDriver.Vehicles.Where(v => v.VehicleId == vehicleRef).Count()>0);
            }
            Orders = ordersQuery.ToList();
        }

    }
}
