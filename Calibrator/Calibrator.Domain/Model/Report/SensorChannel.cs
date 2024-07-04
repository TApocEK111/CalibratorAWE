﻿namespace Calibrator.Domain.Model.Report;

public class SensorChannel
{
    public List<Sample> Samples { get; set; }
    public PhisicalQuantity PhisicalQuantity { get; set; }
}
