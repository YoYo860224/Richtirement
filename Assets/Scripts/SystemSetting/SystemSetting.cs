using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Content;
using Setting;
using System;
using UnityEngine.SceneManagement;

public class SystemSetting : MonoBehaviour {
    public Text language;
    public Dropdown languageDropDown;

	// Use this for initialization
	void Start () {

        languageDropDown.ClearOptions();
        foreach (Localize locLanguage in (Localize[]) Enum.GetValues(typeof(Localize)))
        {
            Dropdown.OptionData newOption = new Dropdown.OptionData();
            newOption.text = locLanguage.ToString();
            languageDropDown.options.Add(newOption);
        }

        languageDropDown.onValueChanged.AddListener(delegate { LanguageOptionListen(); });
    }

    // Update is called once per frame
    void Update () {
		
	}

    void LanguageOptionListen()
    {
        // TODO: 更新頁面
        Setting.SystemSetting.nowLanguage = (Localize)(Enum.GetValues(typeof(Localize))).GetValue(languageDropDown.value);
        Debug.Log("update");
    }

    public void ReturnStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Default()
    {
        // TODO: Default
        Debug.Log("Default");
    }
}
