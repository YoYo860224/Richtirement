using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Content;
using Setting;
using UnityEngine.SceneManagement;

public class InitSettingManager : MonoBehaviour {
    public Text titleText;
    public Text nameText;
    public Text kidText;
    public Text assetsText;

    public InputField nameInputField;
    public Text nameInputPlaceHold;
    public Toggle kidZero;
    public Toggle kidNonzero;
    public Dropdown kidDropdown;
    public Slider currentAssets;
    // Use this for initialization
    void Start () {
        titleText.text = Content.InitSetting.Title;
        nameText.text = Content.InitSetting.Name;
        kidText.text = Content.InitSetting.Kids;
        assetsText.text = Content.InitSetting.CurrentAssets;
        nameInputPlaceHold.text = Content.InitSetting.NamePlaceHold;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextButtom()
    {
        Setting.CharacterSetting.name = nameInputField.text;
        if(Setting.CharacterSetting.name == "")
        {
            nameInputPlaceHold.color = Color.red;
            return;
        }
        if (kidZero.isOn)
        {
            Setting.CharacterSetting.kidAmount = 0;
        }
        else
        {
            Setting.CharacterSetting.kidAmount = kidDropdown.value + 1;
        }
        Setting.CharacterSetting.Money = (int)(currentAssets.value);

        SceneManager.LoadScene("Story");
    }
}
