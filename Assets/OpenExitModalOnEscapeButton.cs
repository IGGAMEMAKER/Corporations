using UnityEngine;

public class OpenExitModalOnEscapeButton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var w = GetComponent<OpenMyModalWindow>();

            w.Execute();
        }
    }
}
