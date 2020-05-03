using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticallyContinueGame : View
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.55f);
        Debug.Log(this.name + ": loading");

        State.LoadGameData(Q);
        State.LoadGameScene();

        Debug.Log(this.name + ": loaded");
    }
}
