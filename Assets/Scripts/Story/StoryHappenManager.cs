using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using UnityEngine.SceneManagement;
using System;

public class StoryHappenManager : MonoBehaviour {
    public Image MoneyFilled;
    public Image MentalFilled;
    public Image PhysicalHearthFilled;
    public Image SocialHearthFilled;

    public Text content;
    public Transform contentFadeOutPosition;
    public Transform contentFadeInPosition;

    public GameObject trueCardImage;
    public Transform trueCardImageFadeOutPosition;
    public Transform trueCardImageFadeInPosition;

    public GameObject falseCardImage;
    public Transform falseCardImageFadeOutPosition;
    public Transform falseCardImageFadeInPosition;

    public Transform bigPosition;

    public Text trueText;
    public Text falseText;

    public GameObject helpField;
    public GameObject helpButton;
    public Image helpImage;
    public Text helpText;

    public Transform helpFadeOutPosition;
    public Transform helpFadeInPosition;

    private static bool canTouch = true;
    private static bool helpState = false;
    public Choice trueChoice;
    public Choice falseChoice;

    private static bool choiceCard = false;

    // Use this for initialization
    void Start () {
        // TODO: set money value
        // MoneyFilled.fillAmount = Setting.CharacterSetting.??;
        MentalFilled.fillAmount = Setting.CharacterSetting.Mental;
        PhysicalHearthFilled.fillAmount = Setting.CharacterSetting.PhysicalHearth;
        SocialHearthFilled.fillAmount = Setting.CharacterSetting.SocialHearth;

        // TODO: set content
        content.text = "content text";

        // TODO: set true text
        trueText.text = "true text";
        // TODO: set false text
        falseText.text = "false text";

        canTouch = true;
        helpState = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    float CubicEaseOut(float t)
    {
        return ((t = t - 1) * t * t + 1);
    }

    public void choiceYes()
    {
        //Setting.CharacterSetting.SocialHearth -= 5;
        //StartCoroutine(SliderChange(SocialHearth, Setting.CharacterSetting.SocialHearth));
        if (!choiceCard)
        {
            choiceCard = !choiceCard;
            StartCoroutine(TweenOut(true));

        }
        else
        {
            choiceCard = !choiceCard;
            StartCoroutine(TweenIn(true));
            //int nextId = trueChoice.NextEvent();

            //// TODO: showResult

            //EventLog log = new EventLog(StoryManager.nowId, true, nextId);
            //StoryManager.EndNowStory(log);

            //// 判斷是不是5年
            //SceneManager.LoadScene("Story");
        }
    }

    public void choiceNo()
    {
        if (!choiceCard)
        {
            choiceCard = !choiceCard;
            StartCoroutine(TweenOut(false));

        }
        else
        {
            choiceCard = !choiceCard;
            StartCoroutine(TweenIn(false));

            //int nextId = falseChoice.NextEvent();
            //Debug.Log("next ID = " + nextId);
            //// TODO: showResult

            //EventLog log = new EventLog(StoryManager.nowId, true, nextId);
            //StoryManager.EndNowStory(log);

            //// 判斷是不是5年
            //SceneManager.LoadScene("Story");
        }
    }

    public void help()
    {
        if (canTouch)
        {
            if (helpState)
            {
                //var tempTextColor = helpText.GetComponent<Text>().color;
                //tempTextColor.a = 0f;
                //helpText.GetComponent<Text>().color = tempTextColor;

                //var tempColor = helpImage.GetComponent<Image>().color;
                //tempColor.a = 0;
                //helpImage.GetComponent<Image>().color = tempColor;
                StartCoroutine(FadeImage(helpImage, true));
                StartCoroutine(FadeText(helpText, true));
                Debug.Log("fade out");
                //helpImage.transform.localPosition = new Vector3(0, -Screen.height, 0);
            }
            else
            {
                //tween(helpImage, 1, helpImage.transform.localPosition, new Vector3(0, Screen.height, 0));
                //helpImage.transform.localPosition = new Vector3(0, Screen.height / 2, 0);

                //var tempTextColor = helpText.GetComponent<Text>().color;
                //tempTextColor.a = 255f;
                //helpText.GetComponent<Text>().color = tempTextColor;
                StartCoroutine(FadeImage(helpImage, false));
                StartCoroutine(FadeText(helpText, false));

                Debug.Log("fade in");
                //var tempColor = helpImage.GetComponent<Image>().color;
                //tempColor.a = 180f;
                //helpImage.GetComponent<Image>().color = tempColor;

            }
            helpState = !helpState;
            Debug.Log("help");
        }
    }

    IEnumerator TweenOut(bool choice)
    {
        var timeStart = Time.time;
        var timeEnd = timeStart + 0.2f;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseOut(t);
            var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeOutPosition.localPosition, v);
            content.transform.localPosition = position;

            var position0 = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, trueCardImageFadeOutPosition.localPosition, v);
            trueCardImage.transform.localPosition = position0;

            var position1 = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, falseCardImageFadeOutPosition.localPosition, v);
            falseCardImage.transform.localPosition = position1;

            var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeOutPosition.localPosition, v);
            helpButton.transform.localPosition = position2;

            yield return null;
        }

        content.transform.localPosition = contentFadeOutPosition.localPosition;
        falseCardImage.transform.localPosition = falseCardImageFadeOutPosition.localPosition;
        trueCardImage.transform.localPosition = trueCardImageFadeOutPosition.localPosition;
        helpButton.transform.localPosition = helpFadeOutPosition.localPosition;

        if (choice)
        {
            trueCardImage.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        }
        else
        {
            falseCardImage.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        }

        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + 0.2f;
        while (Time.time < timeEnd1)
        {
            if (choice)
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, bigPosition.localPosition, v);
                trueCardImage.transform.localPosition = position;
            }
            else
            {

                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, bigPosition.localPosition, v);
                falseCardImage.transform.localPosition = position;
            }

            yield return null;
        }
        if (choice)
        {
            trueCardImage.transform.localPosition = bigPosition.localPosition;
        }
        else
        {
            falseCardImage.transform.localPosition = bigPosition.localPosition;
        }
    }

    IEnumerator TweenIn(bool choice)
    {
        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + 0.2f;
        while (Time.time < timeEnd1)
        {
            if (choice)
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, trueCardImageFadeOutPosition.localPosition, v);
                trueCardImage.transform.localPosition = position;
            }
            else
            {

                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, falseCardImageFadeOutPosition.localPosition, v);
                falseCardImage.transform.localPosition = position;
            }

            yield return null;
        }
        if (choice)
        {
            trueCardImage.transform.localPosition = trueCardImageFadeOutPosition.localPosition;
        }
        else
        {
            falseCardImage.transform.localPosition = falseCardImageFadeOutPosition.localPosition;
        }
        trueCardImage.transform.localScale = new Vector3(1f, 1f, 1);
        falseCardImage.transform.localScale = new Vector3(1f, 1f, 1);

        var timeStart = Time.time;
        var timeEnd = timeStart + 0.2f;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseOut(t);
            var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeInPosition.localPosition, v);
            content.transform.localPosition = position;

            var position0 = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, trueCardImageFadeInPosition.localPosition, v);
            trueCardImage.transform.localPosition = position0;

            var position1 = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, falseCardImageFadeInPosition.localPosition, v);
            falseCardImage.transform.localPosition = position1;

            var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeInPosition.localPosition, v);
            helpButton.transform.localPosition = position2;

            yield return null;
        }

        content.transform.localPosition = contentFadeInPosition.localPosition;
        falseCardImage.transform.localPosition = falseCardImageFadeInPosition.localPosition;
        trueCardImage.transform.localPosition = trueCardImageFadeInPosition.localPosition;
        helpButton.transform.localPosition = helpFadeInPosition.localPosition;

    }

    IEnumerator FadeImage(Image gameObject, bool fadeAway)
    {
        canTouch = false;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                var tempColor = gameObject.color;
                tempColor.a = i / 2.0f;
                gameObject.color = tempColor;
                yield return null;
            }
            helpField.SetActive(false);
        }
        // fade from transparent to opaque
        else
        {
            helpField.SetActive(true);
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                var tempColor = gameObject.color;
                tempColor.a = i / 2.0f;
                gameObject.color = tempColor;
                yield return null;
            }
        }
        canTouch = true;
    }

    IEnumerator FadeText(Text gameObject, bool fadeAway)
    {
        canTouch = false;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                var tempColor = gameObject.color;
                tempColor.a = i;
                gameObject.color = tempColor;
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                var tempColor = gameObject.color;
                tempColor.a = i;
                gameObject.color = tempColor;
                yield return null;
            }
        }
        canTouch = true;
    }

}
