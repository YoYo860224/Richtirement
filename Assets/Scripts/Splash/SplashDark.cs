using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashDark : MonoBehaviour {
    public string nextSceneName;
    public List<Image> startImage;
    public List<Text> startText;

    public List<GameObject> button;
    public List<Text> buttonText;

    int showFlag = 0;
    public float fadeSpeed = 0.4f;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].GetComponent<Button>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var nowColor = GetComponent<Image>().color;

        if (showFlag == 0) // black fade out
        {

            nowColor.a = nowColor.a - fadeSpeed * Time.deltaTime;
            GetComponent<Image>().color = nowColor;
            if (nowColor.a < 0f)
                showFlag = 1;
        }
        else if(showFlag == 1) { // logo and detail fade out

            for(int i = 0; i < startImage.Count; i++)
            {
                var tempColor = startImage[i].GetComponent<Image>().color;
                tempColor.a = tempColor.a - fadeSpeed * Time.deltaTime;
                startImage[i].GetComponent<Image>().color = tempColor;
            }
            for (int i = 0; i < startText.Count; i++)
            {
                var tempColor = startText[i].GetComponent<Text>().color;
                tempColor.a = tempColor.a - fadeSpeed * Time.deltaTime;
                startText[i].GetComponent<Text>().color = tempColor;
                if (tempColor.a < 0f)
                    showFlag = 2;
            }
        }
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
            }
        }
    }
}
