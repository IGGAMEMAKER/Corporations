using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateResourceChange : MonoBehaviour
{
    float period = 0.35f;
    float amplitude = 40;

    float duration = 0;

    void Update()
    {
        Render();
    }

    void Render()
    {
        duration += Time.deltaTime;

        float phase;

        if (duration < period)
            phase = duration / period;
        else
            phase = 2 - duration / period;



        //float scale = 1 + amplification * phase;

        transform.localPosition += new Vector3(0, amplitude * Time.deltaTime / period, 1);

        if (duration >= period * 2)
            Destroy(gameObject);
    }
}
