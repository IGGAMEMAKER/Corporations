using Assets.Core;

public class BrandChangeDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        //if (Companies.IsExploredCompany(Q, SelectedCompany))
        return Marketing.GetBrandChange(SelectedCompany, Q).ToString();
    }
}
