using UnityEngine;

public class CanvasSortingOrderFix : MonoBehaviour
{
    public Canvas Canvas;

    void Start()
    {
        Canvas.enabled = false;
    }

    void Update()
    {
        //StartCoroutine(ExecuteAfterTime(0.01f));
        Canvas.enabled = true;
    }

    //IEnumerator ExecuteAfterTime(float time)
    //{
    //    //yield return new WaitForSeconds(time);
    //    //Canvas.enabled = false;
    //    Canvas.enabled = true;

    //    //Destroy(this);
    //}
}
