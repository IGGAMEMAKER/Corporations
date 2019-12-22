using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// copied from enlargeOnAppearance
// call StartAnimation() if necessary
public class EnlargeOnDemand : MonoBehaviour
{
    [Tooltip("Set only half of full animation length")]
    public float period = 0.15f;

    public float amplification = 0.2f;

    float duration = 0;
    bool animated;

    void Update()
    {
        if (animated)
            Render();
    }

    public void StartAnimation()
    {
        animated = true;
        duration = 0;
    }

    void Render()
    {
        duration += Time.deltaTime;

        float phase;

        if (duration < period)
            phase = duration / period;
        else
            phase = 2 - duration / period;

        float scale = 1 + amplification * phase;

        transform.localScale = new Vector3(scale, scale, 1);

        if (duration >= period * 2)
            animated = false;
    }
}
