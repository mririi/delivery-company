using System;
using System.Collections.Generic;

namespace deliveryCompany.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int? DriverId { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? VehicleStatus { get; set; }

    public int? YearModel { get; set; }

    public virtual User? Driver { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
}
