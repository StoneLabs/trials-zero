using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	public bool overrideWithKeyboardInput = true;
    
    public SceneManager sceneManager;

    public Slider ui_throttle;
    public Slider ui_balance;
    public Toggle ui_mobile;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
    
	void Update()
	{
		if (overrideWithKeyboardInput = !ui_mobile.isOn)
		{
            if (Input.GetKeyDown("r"))
                ResetBike();
            sceneManager.bikeManager.SetThrust(Input.GetAxis("Vertical"));
            ui_throttle.value = Input.GetAxis("Vertical");
			sceneManager.bikeManager.SetBalance(Input.GetAxis("Horizontal"));
            ui_balance.value = Input.GetAxis("Horizontal");
		}
	}

    public void ResetBike()
    {
        ui_throttle.value = 0;
        ui_balance.value = 0;
        sceneManager.Respawn();
    }

    public void ThrottleChanged()
    {
        sceneManager.bikeManager.SetThrust(ui_throttle.value);
    }
    public void BalanceChanged()
    {
        sceneManager.bikeManager.SetBalance(ui_balance.value);
    }
    public void ResetClicked()
    {
        ResetBike();
    }
}