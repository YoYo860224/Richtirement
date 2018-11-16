﻿using System.Collections;
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

    public GameObject leftCardImage;
    public Transform leftCardImageFadeOutPosition;
    public Transform leftCardImageFadeInPosition;

    public GameObject rightCardImage;
    public Transform rightCardImageFadeOutPosition;
    public Transform rightCardImageFadeInPosition;

    public Transform bigPosition;

    public Text leftText;
    public Text rightText;
    
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

    public float tweenTime = 0.4f;
    private int nextId;
    private void Awake()
    {
        resultBox.SetActive(false);
        var tempColor = resultText.color;
        tempColor.a = 0;
        resultText.color = tempColor;

        whitePanel.SetActive(false);
        helpField.SetActive(false);
    }


    void SetQuestion()
    {
        // TODO: 圖 = nowStory.imageUrl
        this.content.text = StoryManager.nowChoice.nextQuestion.content;
        this.leftText.text = StoryManager.nowChoice.nextQuestion.leftChoice.content;
        this.rightText.text = StoryManager.nowChoice.nextQuestion.rightChoice.content;
        this.helpText.text = StoryManager.nowChoice.nextQuestion.hint;
        this.trueChoice = StoryManager.nowChoice.nextQuestion.leftChoice;
        this.falseChoice = StoryManager.nowChoice.nextQuestion.rightChoice;
        if (StoryManager.nowEvent.question.hint == "")
        {
            helpButton.SetActive(false);
        }
        else
        {
            helpButton.SetActive(true);
        }
    }


    // Use this for initialization
    void Start () {
        if (StoryManager.nowEvent.question.hint == "")
        {
            helpButton.SetActive(false);
        }
        // TODO: set money value
        // MoneyFilled.fillAmount = Setting.CharacterSetting.??;
        MentalFilled.fillAmount = Setting.CharacterSetting.Mental / 100.0f;
        PhysicalHearthFilled.fillAmount = Setting.CharacterSetting.Hearth / 100.0f;
        SocialHearthFilled.fillAmount = Setting.CharacterSetting.Social / 100.0f;


        canTouch = true;
        helpState = false;
    }

    public void StartChoice()
    {
        StartCoroutine(StartChoiceAnimation());
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
        if (canTouchToNextStory == 1)
        {
            canTouchToNextStory = 2;
            StartCoroutine(ShowResultBox());
        }
    }

    public void TouchToNextStory()
    {
        if (canTouchToNextStory == 2)
        {
            canTouchToNextStory = 3;
            StartCoroutine(ShowResultText());
        }
        if (canTouchToNextStory == 4)
        {
            //// 判斷是不是5年
            if(Setting.CharacterSetting.age != 55 && Setting.CharacterSetting.age % 5 == 0)
            {
                SceneManager.LoadScene("Redeploy");
            }
            else
            {
                SceneManager.LoadScene("Story");
            }
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

    IEnumerator StartChoiceAnimation()
    {
        content.transform.localPosition = contentFadeOutPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
        helpButton.transform.localPosition = helpFadeOutPosition.localPosition;

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            if (i < 0.8f)
            {
                var tempColor = this.GetComponent<Image>().color;
                tempColor.a = i;
                this.GetComponent<Image>().color = tempColor;
            }
        }

        leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;
        leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
        rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime * 2;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseOut(t);
            var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeInPosition.localPosition, v);
            content.transform.localPosition = position;

            var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeInPosition.localPosition, v);
            leftCardImage.transform.localPosition = position0;

            var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeInPosition.localPosition, v);
            rightCardImage.transform.localPosition = position1;

            var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeInPosition.localPosition, v);
            helpButton.transform.localPosition = position2;

            yield return null;
        }

        content.transform.localPosition = contentFadeInPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeInPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeInPosition.localPosition;
        helpButton.transform.localPosition = helpFadeInPosition.localPosition;
    }

    IEnumerator ShowResultBox()
    {
        // set result
        attributeText.text = Setting.CharacterSetting.AttributeChanged(StoryManager.nowChoice.choiceResults[nextId].valueChanges);

        // TODO : Total Assets

        resultText.text = StoryManager.nowChoice.choiceResults[nextId].content;


        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            yield return null;
        }

        resultBox.SetActive(true);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            var tempColor = resultBox.GetComponent<Image>().color;
            tempColor.a = i * 0.8f;
            resultBox.GetComponent<Image>().color = tempColor;

            tempColor = attributeText.color;
            tempColor.a = i;
            attributeText.color = tempColor;

            tempColor = totalAssetText.color;
            tempColor.a = i;
            totalAssetText.color = tempColor;

            yield return null;
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
        canTouchToNextStory = 4;
    }

    IEnumerator TweenOut(bool choice)
    {
        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseIn(t);
            var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeOutPosition.localPosition, v);
            content.transform.localPosition = position;

            var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeOutPosition.localPosition, v);
            leftCardImage.transform.localPosition = position0;

            var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeOutPosition.localPosition, v);
            rightCardImage.transform.localPosition = position1;

            var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeOutPosition.localPosition, v);
            helpButton.transform.localPosition = position2;

            yield return null;
        }

        content.transform.localPosition = contentFadeOutPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
        helpButton.transform.localPosition = helpFadeOutPosition.localPosition;

        if (choice)
        {
            leftCardImage.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        }
        else
        {
            rightCardImage.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        }

        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + tweenTime;
        while (Time.time < timeEnd1)
        {
            if (choice)
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, bigPosition.localPosition, v);
                leftCardImage.transform.localPosition = position;
            }
            else
            {

                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, bigPosition.localPosition, v);
                rightCardImage.transform.localPosition = position;
            }

            yield return null;
        }
        if (choice)
        {
            leftCardImage.transform.localPosition = bigPosition.localPosition;
        }
        else
        {
            rightCardImage.transform.localPosition = bigPosition.localPosition;
        }
    }

    IEnumerator TweenIn()
    {
        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + tweenTime;
        while (Time.time < timeEnd1)
        {
            var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
            var v = CubicEaseIn(t);
            var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeOutPosition.localPosition, v);
            leftCardImage.transform.localPosition = position;

            var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeOutPosition.localPosition, v);
            rightCardImage.transform.localPosition = position1;

            yield return null;
        }

        leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;

        leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
        rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime;
        while (Time.time < timeEnd)
        {
            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = CubicEaseOut(t);
            var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeInPosition.localPosition, v);
            content.transform.localPosition = position;

            var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeInPosition.localPosition, v);
            leftCardImage.transform.localPosition = position0;

            var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeInPosition.localPosition, v);
            rightCardImage.transform.localPosition = position1;

            var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeInPosition.localPosition, v);
            helpButton.transform.localPosition = position2;

            yield return null;
        }

        content.transform.localPosition = contentFadeInPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeInPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeInPosition.localPosition;
        helpButton.transform.localPosition = helpFadeInPosition.localPosition;
    }

    IEnumerator DoubleChoiceCard(bool choice)
    {
        if (choice)
        {
            StoryManager.nowChoice = StoryManager.nowEvent.question.leftChoice;
        }
        else
        {
            StoryManager.nowChoice = StoryManager.nowEvent.question.rightChoice;
        }

        choiceCard = false;

        // random result and nextId
        nextId = StoryManager.nowChoice.NextEvent();
        Debug.Log(nextId);

        // nextQuestion
        if (nextId == -1)
        {
            var timeStart = Time.time;
            var timeEnd = timeStart + 0.3f;
            while (Time.time < timeEnd)
            {
                if (choice)
                {
                    var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                    var v = CubicEaseIn(t);
                    var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, DoubleChoiceCardPosition.localPosition, v);
                    leftCardImage.transform.localPosition = position;
                }
                else
                {
                    var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                    var v = CubicEaseIn(t);
                    var position = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, DoubleChoiceCardPosition.localPosition, v);
                    rightCardImage.transform.localPosition = position;
                }
                yield return null;
            }
            if (choice)
            {
                leftCardImage.transform.localPosition = DoubleChoiceCardPosition.localPosition;
            }
            else
            {
                rightCardImage.transform.localPosition = DoubleChoiceCardPosition.localPosition;
            }


            StoryManager.nowEvent.content = StoryManager.nowChoice.nextQuestion.content;
            // TODO: ChangeImg
            StoryManager.nowEvent.question = StoryManager.nowChoice.nextQuestion;
            SetQuestion();

            leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
            rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;
            leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
            rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

            timeStart = Time.time;
            timeEnd = timeStart + tweenTime;
            while (Time.time < timeEnd)
            {
                var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                var v = CubicEaseOut(t);
                var position = Vector3.LerpUnclamped(content.transform.localPosition, contentFadeInPosition.localPosition, v);
                content.transform.localPosition = position;

                var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeInPosition.localPosition, v);
                leftCardImage.transform.localPosition = position0;

                var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeInPosition.localPosition, v);
                rightCardImage.transform.localPosition = position1;

                var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeInPosition.localPosition, v);
                helpButton.transform.localPosition = position2;

                yield return null;
            }

            content.transform.localPosition = contentFadeInPosition.localPosition;
            rightCardImage.transform.localPosition = rightCardImageFadeInPosition.localPosition;
            leftCardImage.transform.localPosition = leftCardImageFadeInPosition.localPosition;
            helpButton.transform.localPosition = helpFadeInPosition.localPosition;

            canTouch = true;
            helpState = false;
        }
        // showResult
        else
        {
            whitePanel.SetActive(true);

            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (choice)
                {
                    var tempColor1 = leftCardImage.GetComponent<Image>().color;
                    tempColor1.a = 1 - i;
                    leftCardImage.GetComponent<Image>().color = tempColor1;
                    tempColor1 = leftText.GetComponent<Text>().color;
                    tempColor1.a = 1 - i;
                    leftText.GetComponent<Text>().color = tempColor1;
                }
                else
                {
                    var tempColor1 = rightCardImage.GetComponent<Image>().color;
                    tempColor1.a = 1 - i;
                    rightCardImage.GetComponent<Image>().color = tempColor1;
                    tempColor1 = rightText.GetComponent<Text>().color;
                    tempColor1.a = 1 - i;
                    rightText.GetComponent<Text>().color = tempColor1;
                }

                var tempColor = whitePanel.GetComponent<Image>().color;
                tempColor.a = i;
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

            canTouchToNextStory = 1;
        }
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
