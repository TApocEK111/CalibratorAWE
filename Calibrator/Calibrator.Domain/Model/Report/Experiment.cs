﻿namespace Calibrator.Domain.Model.Report;

internal class Experiment
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double ReferenceValue { get; set; }
}
