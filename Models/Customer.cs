using System;
using System.Collections.Generic;

namespace deliveryCompany.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
