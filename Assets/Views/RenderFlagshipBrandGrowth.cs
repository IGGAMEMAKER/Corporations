using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderFlagshipBrandGrowth : View
{
    public Text Text;

    public override void ViewRender()
    {
        base.ViewRender();

        var anim = GetComponent<Animation>();

        anim.Play();

        var growth = Marketing.GetBrandChange(Flagship, Q).Sum();

        Text.text = Format.Sign(growth, true);
        Text.color = Visuals.GetColorPositiveOrNegative(growth > 0);
    }
}
