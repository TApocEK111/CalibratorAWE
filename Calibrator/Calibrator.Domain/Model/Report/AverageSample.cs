﻿namespace Calibrator.Domain.Model.Report;

public class AverageSample
{
    public Guid Id { get; set; }
    public double ReferenceValue { get; set; }
    public double Parameter { get; set; }
    public double PhysicalQuantity { get; set; }

    public Guid ChannelId { get; set; }
    public SensorChannel Channel { get; set; }
}
