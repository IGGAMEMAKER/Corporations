using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNextNiche : ButtonController
{
    public override void Execute()
    {
        var focus = MyCompany.companyFocus.Niches;

        var index = focus.FindIndex(n => n == SelectedNiche);

        // choose next index
        if (index < focus.Count - 1)
            index++;
        else
            index = 0;

        NavigateToNiche(focus[index]);
    }
}
