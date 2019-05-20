using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : ButtonController
{
    public GameObject Canvas;

    public override void Execute()
    {
        Canvas.SetActive(false);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
