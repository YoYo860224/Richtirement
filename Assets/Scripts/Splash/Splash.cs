using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour {
    public Text titleText;
    public List<Image> startImage;
    public List<Text> startText;
    public List<GameObject> button;
    public List<Text> buttonText;
    public float fadeSpeed = 0.4f;

    int showFlag = 0;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].GetComponent<Button>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showFlag == 0) // black fade out
        {
            var titleColor = titleText.color;
            titleColor.a = titleColor.a + fadeSpeed * Time.deltaTime;
            titleText.color = titleColor;

            for (int i = 0; i < startImage.Count; i++)
            {
                var tempColor = startImage[i].GetComponent<Image>().color;
                tempColor.a = tempColor.a + fadeSpeed * Time.deltaTime;
                startImage[i].GetComponent<Image>().color = tempColor;
            }
            for (int i = 0; i < startText.Count; i++)
            {
                var tempColor = startText[i].GetComponent<Text>().color;
                tempColor.a = tempColor.a + fadeSpeed * Time.deltaTime;
                startText[i].GetComponent<Text>().color = tempColor;
                if (tempColor.a > 1f)
                    showFlag = 2;
            }
        }
        //else if(showFlag == 1)  // logo and detail fade out
        //{  
        //    for(int i = 0; i < startImage.Count; i++)
        //    {
        //        var tempColor = startImage[i].GetComponent<Image>().color;
        //        tempColor.a = tempColor.a - fadeSpeed * Time.deltaTime;
        //        startImage[i].GetComponent<Image>().color = tempColor;
        //    }
        //    for (int i = 0; i < startText.Count; i++)
        //    {
        //        var tempColor = startText[i].GetComponent<Text>().color;
        //        tempColor.a = tempColor.a - fadeSpeed * Time.deltaTime;
        //        startText[i].GetComponent<Text>().color = tempColor;
        //        if (tempColor.a < 0f)
        //            showFlag = 2;
        //    }
        //}
        else if(showFlag == 2)
        {
            for (int i = 0; i < button.Count; i++)
            {
                button[i].GetComponent<Button>().enabled = true;
            }
            for (int i = 0; i < button.Count; i++)
            {
                var tempColor = button[i].GetComponent<Image>().color;
                tempColor.a = tempColor.a + fadeSpeed * Time.deltaTime;
                button[i].GetComponent<Image>().color = tempColor;
            }
            for (int i = 0; i < buttonText.Count; i++)
            {
                var tempColor = buttonText[i].color;
                tempColor.a = tempColor.a + fadeSpeed * Time.deltaTime;
                buttonText[i].color = tempColor;
                if (tempColor.a > 1f)
                    showFlag = 3;
            }
        }
    }


    // For buttom
    public void StartGameButton()
    {
        SceneManager.LoadScene("InitSetting");
    }

    public void SettingButton()
    {
        SceneManager.LoadScene("SystemSetting");
    }
}
