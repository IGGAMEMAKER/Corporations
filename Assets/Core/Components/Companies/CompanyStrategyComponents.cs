using Entitas;
using System.Collections.Generic;

public enum CompanySettingGrowthType
{
    Builder,
    Balanced,
    Aggressive,
}

public enum CompanySettingAttitudeToWorkers
{
    Family,
    Balanced,
    Professionalism,
}

public enum CompanySettingControlDesire
{
    ControlFreak,
    Balanced,
    GrowthOriented,
}

// --- attitude to managers ---
// keep workers
// hire new

// --- raising investments -----
// take as much cash as you can
// take minimum required amount of cash

public class CompanyStrategiesComponent : IComponent
{
    public CompanySettingGrowthType GrowthType;
    public CompanySettingAttitudeToWorkers AttitudeToWorkers;
    public CompanySettingControlDesire ControlDesire;
}
