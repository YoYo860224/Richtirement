using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using UnityEngine.SceneManagement;
using System;

public class StoryHappenManager : MonoBehaviour {
    public Image storyImage;

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

    private bool canTouch = true;
    private bool helpState = false;

    public Choice trueChoice;
    public Choice falseChoice;

    private bool choiceCard = false;

    public Transform DoubleChoiceCardPosition;

    public GameObject resultBox;
    public Text resultText;
    public Text attributeText;
    public Text totalAssetText;
    private int canTouchToNextStory = 0;

    public GameObject whitePanel;
    private void Awake()
    {
        resultBox.SetActive(false);
        var tempColor = resultText.color;
        tempColor.a = 0;
        resultText.color = tempColor;

        whitePanel.SetActive(false);
        helpField.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        // TODO: set money value
        // MoneyFilled.fillAmount = Setting.CharacterSetting.??;
        MentalFilled.fillAmount = Setting.CharacterSetting.Mental;
        PhysicalHearthFilled.fillAmount = Setting.CharacterSetting.PhysicalHearth;
        SocialHearthFilled.fillAmount = Setting.CharacterSetting.SocialHearth;


        canTouch = true;
        helpState = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void choiceYes()
    {
        if (!choiceCard)
        {
            choiceCard = true;
            StartCoroutine(TweenOut(true));
        }
        else
        {
            StartCoroutine(DoubleChoiceCard(true));
        }
    }

    public void choiceNo()
    {
        if (!choiceCard)
        {
            choiceCard = true;
            StartCoroutine(TweenOut(false));

        }
        else
        {
            StartCoroutine(DoubleChoiceCard(false));
        }
    }

    public void TouchBackGround()
    {
        if(choiceCard)
        {
            StartCoroutine(TweenIn());
            choiceCard = false;
        }
    }

    public void TouchToNextStory()
    {
        if (canTouchToNextStory == 0)
        {
            canTouchToNextStory = 1;
            StartCoroutine(ShowResultText());
        }
        if (canTouchToNextStory == 2)
        {
            //EventLog log = new EventLog(StoryManager.nowId, true, nextId);
            //StoryManager.EndNowStory(log);
            //// 判斷是不是5年
            SceneManager.LoadScene("Story");
        }
    }


    public void help()
    {
        if (canTouch)
        {
            if (helpState)
            {
                StartCoroutine(HelpFadeImage(helpImage, helpState));
                StartCoroutine(HelpFadeText(helpText, helpState));
            }
            else
            {
                StartCoroutine(HelpFadeImage(helpImage, helpState));
                StartCoroutine(HelpFadeText(helpText, helpState));
            }
            helpState = !helpState;
        }
    }

    IEnumerator TweenOut(bool choice)
    {
        var timeStart = Time.time;
        var timeEnd = timeStart + 0.2f;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseIn(t);
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

    IEnumerator ShowResultText()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            var tempColor = attributeText.color;
            tempColor.a = i;
            attributeText.color = tempColor;

            tempColor = totalAssetText.color;
            tempColor.a = i;
            totalAssetText.color = tempColor;
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            var tempColor = resultText.color;
            tempColor.a = i;
            resultText.color = tempColor;
            yield return null;
        }
        canTouchToNextStory = 2;
    }

    IEnumerator TweenIn()
    {
        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + 0.2f;
        while (Time.time < timeEnd1)
        {
            var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
            var v = CubicEaseIn(t);
            var position = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, trueCardImageFadeOutPosition.localPosition, v);
            trueCardImage.transform.localPosition = position;

            var position1 = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, falseCardImageFadeOutPosition.localPosition, v);
            falseCardImage.transform.localPosition = position1;

            yield return null;
        }

        trueCardImage.transform.localPosition = trueCardImageFadeOutPosition.localPosition;
        falseCardImage.transform.localPosition = falseCardImageFadeOutPosition.localPosition;

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

    IEnumerator DoubleChoiceCard(bool choice)
    {
        choiceCard = false;
        var timeStart = Time.time;
        var timeEnd = timeStart + 0.3f;
        while (Time.time < timeEnd)
        {
            if (choice)
            {
                var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                var v = CubicEaseIn(t);
                var position = Vector3.LerpUnclamped(trueCardImage.transform.localPosition, DoubleChoiceCardPosition.localPosition, v);
                trueCardImage.transform.localPosition = position;
            }
            else
            {
                var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                var v = CubicEaseIn(t);
                var position = Vector3.LerpUnclamped(falseCardImage.transform.localPosition, DoubleChoiceCardPosition.localPosition, v);
                falseCardImage.transform.localPosition = position;
            }
            yield return null;
        }
        if (choice)
        {
            trueCardImage.transform.localPosition = DoubleChoiceCardPosition.localPosition;

        }
        else
        {
            falseCardImage.transform.localPosition = DoubleChoiceCardPosition.localPosition;
        }

        // 轉場

        //for (float i = 1; i <= 0; i -= Time.deltaTime)
        //{
        //    var tempColor = this.GetComponent<Image>().color;
        //    tempColor.a = i;
        //    this.GetComponent<Image>().color = tempColor;
        //    yield return null;
        //}
        whitePanel.SetActive(true);
        
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            var tempColor = whitePanel.GetComponent<Image>().color;
            tempColor.a = i;
            tempColor.r = i;
            tempColor.g = i;
            tempColor.b = i;
            whitePanel.GetComponent<Image>().color = tempColor;
            yield return null;
        }

        this.GetComponent<Image>().sprite = null;
        var thisColor = this.GetComponent<Image>().color;
        thisColor.a = 0;
        this.GetComponent<Image>().color = thisColor;

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            var tempColor = whitePanel.GetComponent<Image>().color;
            tempColor.a = i;
            whitePanel.GetComponent<Image>().color = tempColor;
            yield return null;
        }
        whitePanel.SetActive(false);
        resultBox.SetActive(true);
    }

    IEnumerator HelpFadeImage(Image gameObject, bool fadeAway)
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
                tempColor.a = i;
                gameObject.color = tempColor;
                yield return null;
            }
        }
        canTouch = true;
    }

    IEnumerator HelpFadeText(Text gameObject, bool fadeAway)
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

    // first first
    float CubicEaseOut(float t)
    {
        return ((t = t - 1) * t * t + 1);
    }
    //first slow
    float CubicEaseIn(float t)
    {
        return t * t * t;
    }
}
