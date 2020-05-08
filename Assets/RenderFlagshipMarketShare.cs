using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderFlagshipMarketShare : ParameterView
{
    public override string RenderValue()
    {
        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(Flagship, Q);

        var position = Markets.GetPositionOnMarket(Q, Flagship);

        var playersOnMarket = Markets.GetProductsOnMarket(Q, Flagship).Count();

        Colorize(playersOnMarket - position, 0, playersOnMarket);

        return $"{share}%";
    }
}
