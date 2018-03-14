using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public SceneManager sceneManager;

    public Slider ui_throttle;
    public Slider ui_balance;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
    
	void Update()
	{
        if (Input.GetKeyDown("r"))
            ResetBike();

        sceneManager.bikeManager.SetThrust(Input.GetAxis("Vertical"));
        sceneManager.bikeManager.SetBalance(Input.GetAxis("Horizontal"));
        
        ui_throttle.value = Input.GetAxis("Vertical");
        ui_balance.value = Input.GetAxis("Horizontal");
	}

    public void ResetBike()
    {
        ui_throttle.value = 0;
        ui_balance.value = 0;
        sceneManager.Respawn();
    }
}