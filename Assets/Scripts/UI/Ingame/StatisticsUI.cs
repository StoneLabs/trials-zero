using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Class to manage the visualization of all statistical information
public class StatisticsUI : GenericUI 
{
    public SceneManager sceneManager;

    // References to the GUI elements
    public Text ui_time;
    public Text ui_respawns;
    
	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}

	void Update()
	{
		TimeSpan time = TimeSpan.FromSeconds(sceneManager.statisticsManager.CurrentTime);
		ui_time.text = string.Format("{0:D2}:{1:D2}.{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
		ui_respawns.text = "Tries: " + sceneManager.statisticsManager.Respawns.ToString();
	}
}