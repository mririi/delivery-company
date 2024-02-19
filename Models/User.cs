using System;
using System.Collections.Generic;

namespace deliveryCompany.Models;

public partial class User
{
    public int UserId { get; set; }

    public int UserType { get; set; }

    public string UserName { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order> OrdersNavigation { get; set; } = new List<Order>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
