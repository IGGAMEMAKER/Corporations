using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToFlagshipCoompany : ButtonController
{
    public override void Execute()
    {
        NavigateToProjectScreen(Flagship.company.Id);
    }
}
