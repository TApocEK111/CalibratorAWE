﻿using Calibrator.Infrastructure.Data;
using Calibrator.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calibrator.Tests.Repository;

public class RepositoryTestHelper
{
    private readonly Context _context;
    public Report TestReport2 = new Report
    {
        Sensors = new List<Sensor>
            {
                new Sensor
                {
                    Channels = new List<SensorChannel>()
                    {
                        new SensorChannel()
                        {
                            Samples = new List<Sample>()
                            {
                                new Sample() { MeasurementTime = DateTime.Now, ReferenceValue = 50, Parameter = 51 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(1), ReferenceValue = 50, Parameter = 48 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(2), ReferenceValue = 60, Parameter = 62 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(3), ReferenceValue = 60, Parameter = 59 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(4), ReferenceValue = 70, Parameter = 70 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(5), ReferenceValue = 50, Parameter = 51 }
                            }
                        }
                    }
                }
            }
    };

    public Report TestReport1 = new Report
    {
        Date = DateTime.MinValue.ToUniversalTime(),
        Sensors = new List<Sensor>
            {
                new Sensor
                {
                    Channels = new List<SensorChannel>()
                    {
                        new SensorChannel()
                        {
                            Samples = new List<Sample>()
                            {
                                new Sample() { MeasurementTime = DateTime.Now, ReferenceValue = -10, Parameter = -1 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(1), ReferenceValue = -10, Parameter = -0.9 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(2), ReferenceValue = -20, Parameter = -2 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(3), ReferenceValue = -30, Parameter = -2.9 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(4), ReferenceValue = -20, Parameter = -2.1 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(5), ReferenceValue = -20, Parameter = -1.9 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(6), ReferenceValue = -20, Parameter = -2 },
                                new Sample() { MeasurementTime = DateTime.Now.AddSeconds(7), ReferenceValue = -10, Parameter = -1.1 }
                            }
                        }
                    }
                }
            }
    };

    public ReportRepository ReportRepository
    {
        get
        {
            return new ReportRepository(_context);
        }
    }

    public RepositoryTestHelper()
    {
        var contextOptions = new DbContextOptionsBuilder<Context>()
            .UseNpgsql("Host=localhost;Port=5432;Database=TestReportDB;Username=postgres;Password=admin")
            .Options;

        _context = new Context(contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        TestReport1.Sensors[0].Channels[0].CalculateAverageSamples();
        TestReport2.Sensors[0].Channels[0].CalculateAverageSamples();

        var calibrator = new Domain.Model.Calibrator.Calibrator();
        calibrator.CalculatePhysicalQuantitylValues(TestReport1);

        _context.Add(TestReport1);
        _context.SaveChanges();

        _context.ChangeTracker.Clear();
    }
}
