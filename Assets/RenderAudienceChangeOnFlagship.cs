using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderAudienceChangeOnFlagship : View
{
    public UpgradedParameterView2<long> ObservableText;

    long previousValue;
    bool initialSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ObservableText == null)
        {
            ObservableText = GetComponent<UpgradedParameterView2<long>>();
        }
    }

    void Update()
    {
        var current = ObservableText.GetValue();
        if (current != previousValue)
        {
            var diff = current - previousValue;

            previousValue = current;

            if (initialSpawn)
                Anim("users", diff);
            else
            {
                initialSpawn = true;
            }
        }
    }

    void Anim<T>(string title, T change)
    {
        Animate(Visuals.Positive($"+{Format.Minify(change)} {title}"));
    }

    void Anim(string title, long change)
    {
        if (change > 0)
            Animate(Visuals.Positive("+" + change) + $" {title}");
        else
            Animate(Visuals.Colorize(change) + $" {title}");
    }
}
