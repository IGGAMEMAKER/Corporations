﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCharacterScreenTabs : View
{
    public GameObject Ambitions;
    public GameObject Investments;

    public override void ViewRender()
    {
        base.ViewRender();

        Ambitions.SetActive(SelectedHuman.hasWorker);
        Investments.SetActive(SelectedHuman.hasShareholder);
    }
}