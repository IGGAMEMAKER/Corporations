using UnityEngine;

public class AnimateResourceChange : MonoBehaviour
{
    float period = 0.35f;
    float amplitude = 40;

    float duration = 0;
    public bool Renewable = false;

    float distance = 0;

    void Update()
    {
        Render();
    }

    void Render()
    {
        duration += Time.deltaTime;

        var movement = amplitude * Time.deltaTime / period;

        distance += movement;
        transform.localPosition += new Vector3(0, movement, 1);

        if (duration >= period * 2)
        {
            duration = 0;

            if (!Renewable)
                Destroy(gameObject);
            else
            {
                transform.localPosition -= new Vector3(0, distance, 1);
                Destroy(this);
                gameObject.SetActive(false);
            }
        }
    }
}
