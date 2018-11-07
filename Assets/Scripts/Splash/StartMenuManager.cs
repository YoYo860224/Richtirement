using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Setting;
using Content;

public class StartMenuManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGameButton()
    {
        SceneManager.LoadScene("InitSetting");
    }

    public void SettingButton()
    {
        SceneManager.LoadScene("SystemSetting");
    }
}
