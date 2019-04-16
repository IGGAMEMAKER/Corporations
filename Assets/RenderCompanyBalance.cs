﻿using Assets.Utils;

public class RenderCompanyBalance : ParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ValueFormatter.Shorten(SelectedCompany.companyResource.Resources.money);
    }
}
