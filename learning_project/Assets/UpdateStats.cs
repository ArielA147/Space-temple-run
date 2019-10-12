using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateStats : MonoBehaviour
{
    // members //
    public Player player;
    public TextMeshPro scoreView;
    public TextMeshPro timeView;
    public TextMeshPro distanceView;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
        scoreView = GameObject.Find("stats/scoreView").GetComponent<TextMeshPro>();
        timeView = GameObject.Find("stats/timeView").GetComponent<TextMeshPro>();
        distanceView = GameObject.Find("stats/distanceView").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: fix print of time and score
        printTime();
        // update score
        printScore();
        // print distance
        printDistance();
    }

    public void printTime()
    {
        float timeLeftInSeconds = player.timeLeftInSeconds;
        if (timeLeftInSeconds <= 0)
        {
            timeLeftInSeconds = 0;
        }
        if (timeLeftInSeconds == 1)
        {
            timeView.SetText(((int)timeLeftInSeconds).ToString() + " Second");
        }
        else
        {
            timeView.SetText(((int)timeLeftInSeconds).ToString() + " Seconds");
        }
    }

    public void printScore()
    {
        float score = player.score;
        if (score == 1 || score == -1)
        {
            scoreView.SetText(score.ToString() + " Point");
        }
        else
        {
            scoreView.SetText(score.ToString() + " Points");
        }
    }

    public void printDistance() {
        float curr = player.GetDistance();
        distanceView.SetText("Distance: " + curr.ToString());
    }
}
