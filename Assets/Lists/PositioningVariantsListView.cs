using Assets.Core;
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
        if (!SelectedCompany.hasProduct)
        {
            SetItems(new GameEntity[0]);
            return;
        }

        var niche = SelectedCompany.product.Niche;
        var positioningVariants = Markets.GetNichePositionings(niche, Q);

        var list = positioningVariants.Keys.ToArray();

        SetItems(list);
    }
}
