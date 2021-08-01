using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem
{
    private static ProfilingComponent _profilingComponent;
    public static ProfilingComponent MyProfiler
    {
        get
        {
            if (_profilingComponent == null)
                _profilingComponent = Companies.GetProfilingComponent(Contexts.sharedInstance.game);

            return _profilingComponent;
        }
    }

    public void Measure(string name, GameEntity product, DateTime time)
    {
        Measure(name + " " + product.company.Name, time);
    }

    public void Measure(string name, DateTime time, string tag = "", bool countInTagOnly = false)
    {
        Companies.Measure(name, time, MyProfiler, tag, countInTagOnly);
    }

    public void MeasureTag(string name, DateTime time)
    {
        Measure(name, time, name, true);
    }

    void Markup(string text = "-----------")
    {
        Companies.MeasureMarkup(MyProfiler, text);
    }
}
