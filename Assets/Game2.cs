using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {
    World world;
    public const float interval = 5f;

    float totalTime = interval; //2 minutes

    GameObject DangerInterrupt;
    private float spacing = 50f;
    GameObject Canvas;
    int Cell;

    List<GameObject> Interrupts;

    // Use this for initialization
    void Start () {
        world = new World();
        DangerInterrupt = GameObject.Find("DangerInterrupt");
        Canvas = GameObject.Find("Canvas");

        Interrupts = new List<GameObject>();

        Cell = 0;

        int projectId = 0;
        int featureId = 0;
        int channelId = 0;
        int adCampaignDuration = 10;

        world.PrintResources(projectId);
        world.ExploreFeature(projectId, featureId);
        world.UpgradeFeature(projectId, featureId);
        world.PrintResources(projectId);
        world.PrintTechnologies(projectId);

        world.PrintProjectInfo(projectId);
        world.PrepareAd(projectId, channelId, adCampaignDuration);

        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);

        world.PrintProjectInfo(projectId);

        world.RaiseInvestments(projectId, 20, 150000);
        world.PrintShareholders(projectId);
        world.PeriodTick(32);


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
