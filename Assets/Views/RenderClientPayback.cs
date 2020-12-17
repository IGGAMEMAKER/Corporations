public class RenderClientPayback : UpgradedParameterView
{
    public override string RenderHint()
    {
        //var product = SelectedCompany;

        //var ads = Markets.GetClientAcquisitionCost(product.product.Niche, Q);
        //var income = Economy.GetIncomePerUser(Q, product);

        //var text = "=\nNew client marketing cost: " + ads;

        //text += "\n/\nIncome per user: " + income;

        return "RenderClientPayback"; // text;
    }

    public override string RenderValue()
    {
        //var product = SelectedCompany;

        //var ads = Markets.GetClientAcquisitionCost(product.product.Niche, Q);
        //var income = Economy.GetIncomePerUser(Q, product);

        //var period = ads / income;

        return "RenderClientPayback";
        //return period.ToString("0.00") + " months";
    }
}
