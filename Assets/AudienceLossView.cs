using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceLossView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var product = Flagship;

        var changeBonus = Marketing.GetAudienceChange(product, Q, true);
        var change = changeBonus.Sum();

        var gain = Marketing.GetAudienceGrowth(product, Q);
        var loss = Marketing.GetChurnClients(product);

        var baseChurn = Marketing.GetBaseChurnRate(product, true);

        var text = "";

        if (change > 0)
        {
            text = $"We {Visuals.Positive("GAIN")} {Format.Minify(change)} users weekly\n-------------------";
        }
        else if (change < 0)
        {
            text = $"We are {Visuals.Negative("LOSING")} {Format.Minify(-change)} users weekly\n--------------------";
        }

        if (gain > 0)
        {
            text += $"\nWe {Visuals.Positive("GAIN")} {Format.Minify(gain)} users from our marketing activities";
        }

        if (loss > 0)
        {
            text += $"\nWe {Visuals.Negative("LOSE")} {Format.Minify(loss)} users, cause";

            if (baseChurn.Sum() > 0)
            {
                text += $"\nBase churn={baseChurn.Sum()}%, cause {baseChurn.ToString()}\n";
            }

            var segments = Marketing.GetAudienceInfos();

            foreach (var s in segments)
            {
                var churn = Marketing.GetSegmentSpecificChurnBonus(new Bonus<long>("segment churn"), product, s.ID, true);

                var segmentChurn = Marketing.GetChurnClients(product, s.ID);

                text += $"\n * {s.Name}: {churn.ToString()}";
            }
        }

        return text;
    }

    public override string RenderValue()
    {
        var change = Marketing.GetAudienceChange(Flagship, Q);

        if (change > 0)
        {
            return $"<size=64><b>{Visuals.Positive("+" + Format.Minify(change))}</b></size>\nusers weekly";
            //return $"We {Visuals.Positive("GAIN")} {Format.Minify(change)} users weekly";
        }
        else if (change < 0)
        {
            var loss = Marketing.GetChurnClients(Flagship);

            return $"<size=64><b>{Visuals.Negative(Format.Minify(-change))}</b></size>\nusers weekly";
            return $"We are {Visuals.Negative("LOSING")} {Format.Minify(-change)} users weekly";
        }
        else
        {
            return "<size=64><b>0</b></size>";
        }
    }
}
