using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {
    public const float interval = 5f;

    float totalTime = interval; //2 minutes

    private float spacing = 50f;
    GameObject DangerInterrupt;
    GameObject Canvas;
    int Cell;

    List<GameObject> Interrupts;


    AudioSource audioData;


    // Use this for initialization
    void Start () {
        DangerInterrupt = GameObject.Find("DangerInterrupt");
        Canvas = GameObject.Find("Canvas");

        Interrupts = new List<GameObject>();

        Cell = 0;

        // sounds
        audioData = this.gameObject.AddComponent<AudioSource>();
        audioData.clip = Resources.Load<AudioClip>("Sounds/Coin");

        audioData.Play();
        //audioData.loop = true;
    }

    // Update is called once per frame
    void Update () {
        totalTime -= Time.deltaTime;

        if (totalTime < 0)
        {
            UpdateLevelTimer(totalTime);

            totalTime = interval;
            Cell++;
        }
    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        for (int y = 0; y < 1; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Vector3 pos = new Vector3(x, (y - Cell), 0) * spacing;
                GameObject g = Instantiate(DangerInterrupt, pos, Quaternion.identity);

                g.transform.SetParent(Canvas.transform, false);

                Interrupts.Add(g);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            var g = Interrupts[i];
            Destroy(g);
        }

        Interrupts.RemoveRange(0, 4);
    }
}
