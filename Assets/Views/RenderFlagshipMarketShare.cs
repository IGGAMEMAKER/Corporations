using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderFlagshipMarketShare : ParameterView
{
    public override string RenderValue()
    {
        var company = GetFollowableCompany();

        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q);

        var position = Markets.GetPositionOnMarket(Q, company);

        var playersOnMarket = Markets.GetProductsOnMarket(Q, company).Count();

        Colorize(playersOnMarket - position, 0, playersOnMarket);

        return $"{share}%";
    }
}
