using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public SceneManager sceneManager;

    public Slider ui_throttle;
    public Slider ui_balance;
    public Text ui_status;
    private string ui_status_text = "";
    
	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
        
        ui_status_text = ui_status.text;
	}
    
    private bool reset_hold = false;
	void Update()
	{
        SetTextInfo();

        if (Input.GetAxis("Reset") == 1)
        {
            if (!reset_hold)
                ResetBike();
            reset_hold = true;
        }
        else
            reset_hold = false;

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

    public void SetTextInfo()
    {
		float msec = Time.smoothDeltaTime * 1000.0f;
		float fps = 1.0f / Time.smoothDeltaTime;
		string fpsText = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        
        string target = Application.targetFrameRate.ToString() + " fps [";
        target += QualitySettings.vSyncCount == 0 ? "NO VSYNC" : 
                  QualitySettings.vSyncCount == 1 ? "SINGLE VSYNC" : 
                  QualitySettings.vSyncCount == 2 ? "DOUBLE VSYNC" : 
                  QualitySettings.vSyncCount == 3 ? "TRIPLE VSYNC" : 
                  QualitySettings.vSyncCount == 4 ? "QUAD VSYNC" : "UNKNOWN VSYNC";
        target += "]";

        float mps = sceneManager.bikeManager.body.transform.GetComponent<Rigidbody>().velocity.magnitude;
        string mpsText = string.Format("{0:0.0} mp/s", mps);
        string kmhText = string.Format("{0:0.0} km/h", mps*3.6);

        bool alive = sceneManager.bikeManager.isAlive();

        ui_status.text = ui_status_text
            .Replace("{FPS}", fpsText)
            .Replace("{TGR}", target)
            .Replace("{MPS}", mpsText)
            .Replace("{KMH}", kmhText)
            .Replace("{ALV}", alive.ToString());
    }

    public void SelectLevel(int index)
    {
        Application.LoadLevel(index);
    }
}