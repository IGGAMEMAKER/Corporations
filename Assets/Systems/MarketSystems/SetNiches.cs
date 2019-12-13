using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;


}
