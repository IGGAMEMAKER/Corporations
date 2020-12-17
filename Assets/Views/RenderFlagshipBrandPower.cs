public class RenderFlagshipBrandPower : ParameterView
{
    public override string RenderValue()
    {
        return (int)Flagship.branding.BrandPower + "";
    }
}
