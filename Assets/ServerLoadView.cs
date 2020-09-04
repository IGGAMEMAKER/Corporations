﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLoadView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var load = Format.Minify(Products.GetServerLoad(Flagship));
        var capacity = Format.Minify(Products.GetServerCapacity(Flagship));

        bool overloaded = Products.IsNeedsMoreServers(Flagship);

        var str = $"Load: <b>{load} / {capacity}</b>";

        if (overloaded)
        {
            str += Visuals.Negative(" Servers are overloaded!");
        }

        return str;
    }
}
