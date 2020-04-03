using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillNicheTableListView : View
{
    private void OnEnable()
    {
        Render();
    }

    void Render()
    {
        //var niches = NicheUtils.GetNiches(GameContext);
        var niches = Markets.GetPlayableNiches(Q);

        GetComponent<NicheTableListView>().SetItems(niches);
    }
}
