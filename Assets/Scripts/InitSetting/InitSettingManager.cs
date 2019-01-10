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

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    // Use this for initialization
    void Start () {
        Setting.CharacterSetting.InitCharacter();
    }
	
	// Update is called once per frame
	void Update () {
        Translate();
	}

    public void NextButtom1()
    {
        Setting.CharacterSetting.name = nameInputField.text;
        if(Setting.CharacterSetting.name == "")
        {
            nameInputPlaceHold.color = Color.red;
            return;
        }
        var p = panel2.GetComponent<RectTransform>().localPosition;
        p.x = 0;
        panel2.GetComponent<RectTransform>().localPosition = p;
    }

    public void NextButtom2()
    {
        Setting.CharacterSetting.hasSpouse = spouseYes.isOn;
        var p = panel3.GetComponent<RectTransform>().localPosition;
        p.x = 0;
        panel3.GetComponent<RectTransform>().localPosition = p;
    }

    public void NextButtom3()
    {
        Setting.CharacterSetting.kidAmount = kidDropdown.value;
        var p = panel4.GetComponent<RectTransform>().localPosition;
        p.x = 0;
        panel4.GetComponent<RectTransform>().localPosition = p;
    }

    public void NextButtom4()
    {
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
