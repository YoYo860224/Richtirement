using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using UnityEngine.SceneManagement;
using System;

public class QuestionManager : MonoBehaviour {
    public Image MoneyFilled;
    public Image MentalFilled;
    public Image PhysicalHearthFilled;
    public Image SocialHearthFilled;

    private Color moneyColor;
    private Color mentalColor;
    private Color physicalHearthColor;
    private Color socialHearthColor;

    public Image questionImage;
    public Text questionContent;
    public Transform contentFadeOutPosition;
    public Transform contentFadeInPosition;

    public Text leftText;
    public GameObject leftCardImage; 
    public Transform leftCardImageFadeOutPosition;
    public Transform leftCardImageFadeInPosition;

    public Text rightText;
    public GameObject rightCardImage;
    public Transform rightCardImageFadeOutPosition;
    public Transform rightCardImageFadeInPosition;

    public Transform bigCardPosition;
    public Transform DoubleChoiceCardPosition;

    public GameObject helpField;
    public GameObject helpButton;
    public Image helpImage;
    public Text helpText;
    public Transform helpFadeOutPosition;
    public Transform helpFadeInPosition;

    public GameObject resultBox;
    public Text resultText;
    public Text attributeText;
    public Text totalAssetText;

    public GameObject whitePanel;

    private Choice trueChoice;
    private Choice falseChoice;

    private bool choiceCard = false;            // 有沒有選中卡
    private bool helpState = false;             // Help 是開是關

    private int canTouchToNextStory = 0;
    /* 
     * 0 : 正常情況，卡片確認退回用
     * 1 : 故事有結果了，顯示結果用
     * 2 : 
     * 3 : 
     */
    public float tweenTime = 0.4f;
    private int nextId;

    private void Awake()
    {
        moneyColor = MoneyFilled.color;
        mentalColor = MentalFilled.color;
        physicalHearthColor = PhysicalHearthFilled.color;
        socialHearthColor = SocialHearthFilled.color;

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
        MentalFilled.fillAmount = Setting.CharacterSetting.Mental / 100.0f;
        PhysicalHearthFilled.fillAmount = Setting.CharacterSetting.Hearth / 100.0f;
        SocialHearthFilled.fillAmount = Setting.CharacterSetting.Social / 100.0f;

        helpState = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    bool IsGameOver()
    {
        return Setting.CharacterSetting.Hearth <= 0 || Setting.CharacterSetting.Money <= 0 || Setting.CharacterSetting.Mental <= 0 || Setting.CharacterSetting.Social <= 0;
    }

    public void SetQuestion(Question q)
    {
        this.transform.parent.Find("QuestionImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(q.imageUrl);
        this.questionContent.text = q.content;
        this.leftText.text = q.leftChoice.content;
        this.rightText.text = q.rightChoice.content;
        this.helpText.text = q.hint;
        this.trueChoice = q.leftChoice;
        this.falseChoice = q.rightChoice;
        if (q.hint == "")
        {
            helpButton.SetActive(false);
        }
        else
        {
            helpButton.SetActive(true);
        }
    }

    public void StartChoice()
    {
        if (StoryManager.nowEvent.question.absoluteChoice == null)
        {
            StartCoroutine(StartChoiceAnimation());
        }
        else
        {
            StartCoroutine(AbsoluteResult());
        }
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
            StartCoroutine(AttributeChangedAnimation());
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
            CharacterSetting.nYearsLater(StoryManager.nowEvent.year);
            Debug.Log("Money " + Setting.CharacterSetting.Money);
            Debug.Log("Mental " + Setting.CharacterSetting.Mental);
            Debug.Log("Social " + Setting.CharacterSetting.Social);
            Debug.Log("Hearth " + Setting.CharacterSetting.Hearth);
            Debug.Log("IsGameOver " + IsGameOver().ToString());

            if (IsGameOver())
            {
                SceneManager.LoadScene("FinalResult");
            }
            //// 判斷是不是5年
            else if (StoryManager.futureEventsID.Count > 0 && Setting.CharacterSetting.age % 5 == 0)
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
        if (helpState)
        {
            StartCoroutine(HelpFade(helpState));
        }
        else
        {
            StartCoroutine(HelpFade(helpState));
        }
        helpState = !helpState;
    }

    IEnumerator StartChoiceAnimation()
    {
        leftCardImage.SetActive(true);
        rightCardImage.SetActive(true);
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        SetUIToFadeOutLoc();
        leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
        rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime * 2;

        while (Time.time < timeEnd)
        {
            // Set Background Color
            SetImageAlpha(this.GetComponent<Image>(), Time.time / timeEnd * 0.8f);
            SetImageAlpha(transform.parent.Find("QuestionImage").gameObject.GetComponent<Image>(), Time.time / timeEnd);

            UIFadeInTween(timeStart, timeEnd);
            yield return null;
        }

        SetUIToFadeInLoc();

        leftCardImage.GetComponent<Button>().enabled = true;
        rightCardImage.GetComponent<Button>().enabled = true;
        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator TweenOut(bool choice)
    {
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime;
        while (Time.time < timeEnd)
        {
            UIFadeOutTween(timeStart, timeEnd);
            yield return null;
        }

        SetUIToFadeOutLoc();

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
                var v = LinearEase(t);
                var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, bigCardPosition.localPosition, v);
                leftCardImage.transform.localPosition = position;
            }
            else
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = LinearEase(t);
                var position = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, bigCardPosition.localPosition, v);
                rightCardImage.transform.localPosition = position;
            }

            yield return null;
        }
        if (choice)
        {
            leftCardImage.transform.localPosition = bigCardPosition.localPosition;
        }
        else
        {
            rightCardImage.transform.localPosition = bigCardPosition.localPosition;
        }

        leftCardImage.GetComponent<Button>().enabled = true;
        rightCardImage.GetComponent<Button>().enabled = true;
        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator TweenIn()
    {
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + tweenTime;
        while (Time.time < timeEnd1)
        {
            var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
            var v = LinearEase(t);
            var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeOutPosition.localPosition, v);
            leftCardImage.transform.localPosition = position;

            var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeOutPosition.localPosition, v);
            rightCardImage.transform.localPosition = position1;

            yield return null;
        }

        SetUIToFadeOutLoc();

        leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
        rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

        var timeStart = Time.time;
        var timeEnd = timeStart + tweenTime;
        while (Time.time < timeEnd)
        {
            UIFadeInTween(timeStart, timeEnd);
            yield return null;
        }

        SetUIToFadeInLoc();

        leftCardImage.GetComponent<Button>().enabled = true;
        rightCardImage.GetComponent<Button>().enabled = true;
        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator DoubleChoiceCard(bool choice)
    {
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;

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
                    var v = LinearEase(t);
                    var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, DoubleChoiceCardPosition.localPosition, v);
                    leftCardImage.transform.localPosition = position;
                }
                else
                {
                    var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                    var v = LinearEase(t);
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

            StoryManager.nowEvent.question = StoryManager.nowChoice.nextQuestion;
            SetQuestion(StoryManager.nowEvent.question);

            SetUIToFadeOutLoc();
            leftCardImage.transform.localScale = new Vector3(1f, 1f, 1);
            rightCardImage.transform.localScale = new Vector3(1f, 1f, 1);

            timeStart = Time.time;
            timeEnd = timeStart + tweenTime;
            while (Time.time < timeEnd)
            {
                UIFadeInTween(timeStart, timeEnd);
                yield return null;
            }

            SetUIToFadeInLoc();

            leftCardImage.GetComponent<Button>().enabled = true;
            rightCardImage.GetComponent<Button>().enabled = true;
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
                    SetImageAlpha(leftCardImage.GetComponent<Image>(), 1 - i);
                    SetTextAlpha(leftText.GetComponent<Text>(), 1 - i);
                }
                else
                {
                    SetImageAlpha(rightCardImage.GetComponent<Image>(), 1 - i);
                    SetTextAlpha(rightText.GetComponent<Text>(), 1 - i);
                }

                SetImageAlpha(whitePanel.GetComponent<Image>(), i);

                yield return null;
            }

            transform.parent.Find("QuestionImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(StoryManager.nowChoice.choiceResults[nextId].imageUrl);
            this.GetComponent<Image>().sprite = null;

            SetImageAlpha(this.GetComponent<Image>(), 0);

            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                SetImageAlpha(whitePanel.GetComponent<Image>(), i);
                yield return null;
            }

            whitePanel.SetActive(false);
            leftCardImage.SetActive(false);
            rightCardImage.SetActive(false);

            canTouchToNextStory = 1;
        }
    }

    IEnumerator AbsoluteResult()
    {
        SetUIToFadeOutLoc();

        leftCardImage.SetActive(false);
        rightCardImage.SetActive(false);
        whitePanel.SetActive(true);

        StoryManager.nowChoice = StoryManager.nowEvent.question.absoluteChoice;

        choiceCard = false;

        // random result and nextId
        nextId = StoryManager.nowChoice.NextEvent();
        Debug.Log(nextId);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            SetImageAlpha(whitePanel.GetComponent<Image>(), i);
            yield return null;
        }

        transform.parent.Find("QuestionImage").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        transform.parent.Find("QuestionImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(StoryManager.nowChoice.choiceResults[nextId].imageUrl);
        this.GetComponent<Image>().sprite = null;
        var thisColor = this.GetComponent<Image>().color;
        thisColor.a = 0;
        this.GetComponent<Image>().color = thisColor;

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            SetImageAlpha(whitePanel.GetComponent<Image>(), i);
            yield return null;
        }
        whitePanel.SetActive(false);

        canTouchToNextStory = 1;
    }

    IEnumerator ShowResultBox()
    {
        // set result
        attributeText.text = AttributeChanged(StoryManager.nowChoice.choiceResults[nextId].valueChanges);
        
        resultText.text = StoryManager.nowChoice.choiceResults[nextId].content;

        resultBox.SetActive(true);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            if (i < 0.8f)
            {
                SetImageAlpha(resultBox.GetComponent<Image>(), i);
            }

            SetTextAlpha(attributeText, i);
            SetTextAlpha(totalAssetText, i);

            yield return null;
        }
    }

    IEnumerator ShowResultText()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            SetTextAlpha(attributeText, i);
            SetTextAlpha(totalAssetText, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            SetTextAlpha(resultText, i);
            yield return null;
        }
        canTouchToNextStory = 4;
    }

    IEnumerator HelpFade(bool fadeIn)
    {
        helpButton.transform.GetChild(0).GetComponent<Button>().enabled = false;
        if (fadeIn)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (i < 0.8f)
                {
                    SetImageAlpha(helpImage, i);
                }
                SetTextAlpha(helpText, i);
                yield return null;
            }
            helpField.SetActive(false);
        }
        else
        {
            helpField.SetActive(true);
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if(i< 0.8f)
                {
                    SetImageAlpha(helpImage, i);
                }
                SetTextAlpha(helpText, i);
                yield return null;
            }
        }
        helpButton.transform.GetChild(0).GetComponent<Button>().enabled = true;
    }

    IEnumerator AttributeChangedAnimation()
    {
        int brightTime = 30;
        float MoneyDiff = ((float)Setting.CharacterSetting.Money - (float)MoneyFilled.fillAmount * 100.0f) / (float)brightTime / 100.0f;
        float MentalDiff = ((float)Setting.CharacterSetting.Mental - (float)MentalFilled.fillAmount * 100.0f) / (float)brightTime / 100.0f;
        float SocialDiff = ((float)Setting.CharacterSetting.Social - (float)SocialHearthFilled.fillAmount * 100.0f) / (float)brightTime / 100.0f;
        float HearthDiff = ((float)Setting.CharacterSetting.Hearth - (float)PhysicalHearthFilled.fillAmount * 100.0f) / (float)brightTime / 100.0f;

        for (int i = 0; i < brightTime; i++)
        {
            MoneyFilled.fillAmount += MoneyDiff;
            MentalFilled.fillAmount += MentalDiff;
            SocialHearthFilled.fillAmount += SocialDiff;
            PhysicalHearthFilled.fillAmount += HearthDiff;


            if (i % 2 == 0)
            {
                for (float j = 1; j >= 0; j -= Time.deltaTime * 6)
                {
                    if (Setting.CharacterSetting.moneyHasChanged != 0)
                    {
                        SetImageAlpha(MoneyFilled, j);
                    }
                    if (Setting.CharacterSetting.mentalHasChanged != 0)
                    {
                        SetImageAlpha(MentalFilled, j);
                    }
                    if (Setting.CharacterSetting.socialHasChanged != 0)
                    {
                        SetImageAlpha(SocialHearthFilled, j);
                    }
                    if (Setting.CharacterSetting.hearthHasChanged != 0)
                    {
                        SetImageAlpha(PhysicalHearthFilled, j);
                    }
                    yield return null;
                }
            }
            else
            {
                for (float j = 0; j < 1; j += Time.deltaTime * 6)
                {
                    if (Setting.CharacterSetting.moneyHasChanged != 0)
                    {
                        SetImageAlpha(MoneyFilled, j);
                    }
                    if (Setting.CharacterSetting.mentalHasChanged != 0)
                    {
                        SetImageAlpha(MentalFilled, j);
                    }
                    if (Setting.CharacterSetting.socialHasChanged != 0)
                    {
                        SetImageAlpha(SocialHearthFilled, j);
                    }
                    if (Setting.CharacterSetting.hearthHasChanged != 0)
                    {
                        SetImageAlpha(PhysicalHearthFilled, j);
                    }
                    yield return null;
                }
            }
            yield return null;
        }
        SetImageAlpha(MoneyFilled, 1);
        SetImageAlpha(MentalFilled, 1);
        SetImageAlpha(SocialHearthFilled, 1);
        SetImageAlpha(PhysicalHearthFilled, 1);

    }

    /*
    * P心理Mental
    * S社交Social
    * H健康Hearth
    * $(萬)
    * example: "P + 50", "P - 3 5"
    */
    public string AttributeChanged(List<string> changed)
    {
        totalAssetText.text = "";
        Setting.CharacterSetting.moneyHasChanged = 0;
        Setting.CharacterSetting.mentalHasChanged = 0;
        Setting.CharacterSetting.hearthHasChanged = 0;
        Setting.CharacterSetting.socialHasChanged = 0;

        string mentalResult = "";
        string hearthResult = "";
        string socialResult = "";

        for (int i = 0; i < changed.Count; i++)
        {
            Debug.Log(changed[i]);

            // 解析字串
            // words[0]: Attribute
            // words[1]: Sign
            // words[2]: Value
            // words[3]: Value if random
            string[] words = changed[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // 得到 value
            int value = 0;
            if (words.Length == 3)
            {
                // example: "P + 50"
                value = int.Parse(words[2]);
            }
            else if (words.Length == 4)
            {
                // example: "P - 3 5"
                value = RandomUtil.random.Next(int.Parse(words[2]), int.Parse(words[3]));
            }
            // 判斷+-
            if (words[1] == "+")
            {
                // 判斷 Attribute
                switch (words[0])
                {
                    case "$":
                        Setting.CharacterSetting.moneyHasChanged = 1;
                        Setting.CharacterSetting.Money += value;
                        totalAssetText.text = "總財產 +" + value.ToString() + " 萬";
                        break;
                    case "P":
                        Setting.CharacterSetting.mentalHasChanged = 1;
                        //mentalResult = "心理指數 ++" + value.ToString() + "\n";
                        mentalResult = "心理指數 ++ \n";
                        Setting.CharacterSetting.Mental += value;
                        break;
                    case "S":
                        Setting.CharacterSetting.socialHasChanged = 1;
                        //socialResult = "Social Index + " + value.ToString() + "\n";
                        socialResult = "社交指數 ++ \n";
                        Setting.CharacterSetting.Social += value;
                        break;
                    case "H":
                        Setting.CharacterSetting.hearthHasChanged = 1;
                        //hearthResult = "Physiologic Index + " + value.ToString() + "\n";
                        hearthResult = "健康指數 ++ \n";
                        Setting.CharacterSetting.Hearth += value;
                        break;
                }
            }
            else if (words[1] == "-")
            {
                switch (words[0])
                {
                    case "$":
                        Setting.CharacterSetting.moneyHasChanged = -1;
                        Setting.CharacterSetting.Money -= value;
                        totalAssetText.text = "總財產 -" + value.ToString() + " 萬";
                        break;
                    case "P":
                        Setting.CharacterSetting.mentalHasChanged = -1;
                        //mentalResult = "Mental Index - " + value.ToString() + "\n";
                        mentalResult = "心理指數 -- \n";
                        Setting.CharacterSetting.Mental -= value;
                        break;
                    case "S":
                        Setting.CharacterSetting.socialHasChanged = -1;
                        //socialResult = "Social Index - " + value.ToString() + "\n";
                        socialResult = "社交指數 -- \n";
                        Setting.CharacterSetting.Social -= value;
                        break;
                    case "H":
                        Setting.CharacterSetting.hearthHasChanged = -1;
                        hearthResult = "Physiologic Index - " + value.ToString() + "\n";
                        hearthResult = "健康指數 -- \n";
                        Setting.CharacterSetting.Hearth -= value;
                        break;
                }
            }
        }

        return mentalResult + hearthResult + socialResult;
    }

    void UIFadeInTween(float timeStart, float timeEnd)
    {
        var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
        var v = LinearEase(t);
        var position = Vector3.LerpUnclamped(questionContent.transform.localPosition, contentFadeInPosition.localPosition, v);
        questionContent.transform.localPosition = position;

        var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeInPosition.localPosition, v);
        leftCardImage.transform.localPosition = position0;

        var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeInPosition.localPosition, v);
        rightCardImage.transform.localPosition = position1;

        var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeInPosition.localPosition, v);
        helpButton.transform.localPosition = position2;
    }

    void UIFadeOutTween(float timeStart, float timeEnd)
    {
        var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
        var v = LinearEase(t);
        var position = Vector3.LerpUnclamped(questionContent.transform.localPosition, contentFadeOutPosition.localPosition, v);
        questionContent.transform.localPosition = position;

        var position0 = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeOutPosition.localPosition, v);
        leftCardImage.transform.localPosition = position0;

        var position1 = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeOutPosition.localPosition, v);
        rightCardImage.transform.localPosition = position1;

        var position2 = Vector3.LerpUnclamped(helpButton.transform.localPosition, helpFadeOutPosition.localPosition, v);
        helpButton.transform.localPosition = position2;
    }

    void SetUIToFadeInLoc()
    {
        questionContent.transform.localPosition = contentFadeInPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeInPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeInPosition.localPosition;
        helpButton.transform.localPosition = helpFadeInPosition.localPosition;
    }

    void SetUIToFadeOutLoc()
    {
        questionContent.transform.localPosition = contentFadeOutPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeOutPosition.localPosition;
        leftCardImage.transform.localPosition = leftCardImageFadeOutPosition.localPosition;
        helpButton.transform.localPosition = helpFadeOutPosition.localPosition;
    }

    void SetImageAlpha(Image image, float value)
    {
        var tempColor = image.color;
        tempColor.a = value;
        image.color = tempColor;
    }

    void SetTextAlpha(Text text, float value)
    {
        var tempColor = text.color;
        tempColor.a = value;
        text.color = tempColor;
    }

    // Linear
    float LinearEase(float t)
    {
        return t;
    }
    // first first
    float CubicEaseOut(float t)
    {
        return ((t = t - 1) * t * t + 1);
    }
    // first slow
    float CubicEaseIn(float t)
    {
        return t * t * t;
    }
}
