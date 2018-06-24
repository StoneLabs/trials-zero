using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that holds information on the current run (respawns, time, etc.)
 */
public class StatisticsManager : MonoBehaviour 
{
    public int Respawns
    {
        private set;
        get;
    }

    private float StartTime; // The start time in delta seconds since level load

    private float? FinishTime = null;
    // The current time in seconds
    public float CurrentTime
    {
        get { return (FinishTime ?? Time.time) - StartTime; }
    }

    public void HandleStart()   { StartTime = Time.time; }
    public void HandleRespawn() { Respawns++; }
    public void HandleFinish()  { FinishTime = Time.time; }

    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(10, 200, 100, 20), Respawns.ToString());
        GUI.Label(new Rect(10, 220, 100, 20), StartTime.ToString());
        GUI.Label(new Rect(10, 240, 100, 20), (FinishTime ?? -1.0f).ToString());
        GUI.Label(new Rect(10, 260, 100, 20), CurrentTime.ToString());
    }
}