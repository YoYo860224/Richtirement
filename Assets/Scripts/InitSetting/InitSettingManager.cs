using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Content;
using Setting;
using UnityEngine.SceneManagement;

public class InitSettingManager : MonoBehaviour {
    //public Text titleText;
    public Text nameText;
    public Text spouseText;
    public Text kidText;
    public Text assetsText;
    public Text assetsSubText;

    public InputField nameInputField;
    public Text nameInputPlaceHold;
    public Toggle spouseYes;
    public Toggle spouseNo;
    public Dropdown kidDropdown;
    public Slider currentAssets;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Translate();
	}

    public void NextButtom()
    {
        Setting.CharacterSetting.name = nameInputField.text;
        if(Setting.CharacterSetting.name == "")
        {
            nameInputPlaceHold.color = Color.red;
            return;
        }
        Setting.CharacterSetting.hasSpouse = spouseYes.isOn;
        Setting.CharacterSetting.kidAmount = kidDropdown.value;
        Setting.CharacterSetting.Money = (int)(currentAssets.value);
        Debug.Log(Setting.CharacterSetting.name);
        Debug.Log(Setting.CharacterSetting.hasSpouse);
        Debug.Log(Setting.CharacterSetting.kidAmount);
        Debug.Log(Setting.CharacterSetting.Money);

        SceneManager.LoadScene("Story");
    }

    public void Translate() {
        nameText.text = Content.InitSetting.Name;
        nameInputPlaceHold.text = Content.InitSetting.NamePlaceHold;
        kidText.text = Content.InitSetting.Kids;
        assetsText.text = Content.InitSetting.CurrentAssets;
        assetsSubText.text = Content.InitSetting.CurrentAssets_subTitle;
    }
}
