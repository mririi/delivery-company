using System;
using System.Collections.Generic;

namespace deliveryCompany.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int? VehicleId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string? DescriptionMaintenance { get; set; }

    public decimal? Cost { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
