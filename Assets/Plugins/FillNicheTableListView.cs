using Assets.Utils;
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
        var niches = Markets.GetPlayableNiches(GameContext);

        GetComponent<NicheTableListView>().SetItems(niches);
    }
}
