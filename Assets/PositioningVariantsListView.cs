using Assets.Utils;
using System.Linq;
using UnityEngine;

public class PositioningVariantsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<PositioningView>().SetEntity((int)(object)entity);
    }

    private void OnEnable()
    {
        var niche = SelectedCompany.product.Niche;
        var positioningVariants = NicheUtils.GetNichePositionings(niche, GameContext);

        var list = positioningVariants.Keys.ToArray();

        SetItems(list);
    }
}
