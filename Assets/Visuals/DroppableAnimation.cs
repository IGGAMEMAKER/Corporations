using UnityEngine;

namespace Assets.Visuals
{
    public class DroppableAnimation : MonoBehaviour
    {
        float duration;

        Vector3 scale;

        void Start()
        {
            scale = new Vector3(1, 1, 1);
            duration = 1;
        }

        void Update()
        {
            transform.localScale = scale * (1 + 0.85f * duration);

            duration -= Time.deltaTime;

            if (duration < 0)
                Destroy(this);
        }
    }
}
