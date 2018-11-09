using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareController : BaseCommandHandler
{
    public override void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        switch (eventName)
        {
            case Commands.SHARES_BUY:
                ExchangeShare(parameters);
                break;

            case Commands.SHARES_SELL:
                ExchangeShare(parameters);
                break;
        }
    }

    void ExchangeShare(Dictionary<string, object> parameters)
    {
        int sellerId = (int)parameters["sellerId"];
        int buyerId = (int)parameters["buyerId"];
        int share = (int)parameters["share"];

        application.ExchangeShare(sellerId, buyerId, share);
    }
}
