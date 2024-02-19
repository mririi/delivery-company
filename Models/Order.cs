using System;
using System.Collections.Generic;

namespace deliveryCompany.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public int? AssignedDriverId { get; set; }

    public virtual User? AssignedDriver { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<User> Drivers { get; set; } = new List<User>();
}
