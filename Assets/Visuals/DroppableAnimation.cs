using UnityEngine;

namespace Assets.Visuals
{
    public class DroppableAnimation : MonoBehaviour
    {
        float duration;

        void Start()
        {
            duration = 1;
        }

        void Update()
        {
            float scale = (1 + 0.85f * duration);
            transform.localScale = new Vector3(scale, scale, 1);

            duration -= Time.deltaTime;

            if (duration < 0)
                Destroy(this);
        }
    }
}
