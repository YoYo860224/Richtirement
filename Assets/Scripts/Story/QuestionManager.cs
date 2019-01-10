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

    public Text MoneyText;
    public Text MentalText;
    public Text PhysicalHearthText;
    public Text SocialHearthText;

    private Color moneyColor;
    private Color mentalColor;
    private Color physicalHearthColor;
    private Color socialHearthColor;

    public Image questionImage;
    public Image questionFrame;
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

    private List<bool> choiceLog;

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

        MoneyText.text = "";
        MentalText.text = "";
        PhysicalHearthText.text = "";
        SocialHearthText.text = "";
    }

    // Use this for initialization
    void Start () {
        choiceLog = new List<bool>();

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
        this.transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(q.imageUrl);
        this.questionFrame.enabled = true;
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
            // helpButton.SetActive(true);
        }
    }

    public void StartChoice()
    {
        if (StoryManager.nowEvent.question.absoluteChoice == null)
        {
            //StartCoroutine(StartChoiceAnimation());
        }
        else
        {
            StartCoroutine(AbsoluteResult());
        }
    }

    public void choiceLeft()
    {
        StartCoroutine(DoubleChoiceCard(true));

        //if (!choiceCard)
        //{
        //    choiceCard = true;
        //    StartCoroutine(ChoiceCard(true));
        //}
        //else
        //{
        //    StartCoroutine(DoubleChoiceCard(true));
        //}
    }

    public void choiceRight()
    {
        StartCoroutine(DoubleChoiceCard(false));

        //if (!choiceCard)
        //{
        //    choiceCard = true;
        //    StartCoroutine(ChoiceCard(false));
        //}
        //else
        //{
        //    StartCoroutine(DoubleChoiceCard(false));
        //}
    }

    public void TouchBackGround()
    {
        if(choiceCard)
        {
            StartCoroutine(CancelChoiceCard());
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

        if (canTouchToNextStory == 3)
        {
            StoryManager.log.Add(new EventLog(StoryManager.nowEvent.id, choiceLog));

            //for(int i = 0;i < StoryManager.log.Count; i++)
            //{
            //    Debug.Log(StoryManager.log[i].id);
            //    for (int j = 0;j < StoryManager.log[i].choice.Count; j++)
            //    {
            //        Debug.Log(StoryManager.log[i].choice[j]);
            //    }
            //}

            CharacterSetting.nYearsLater(StoryManager.nowEvent.year);
            //Debug.Log("Money " + Setting.CharacterSetting.Money);
            //Debug.Log("Mental " + Setting.CharacterSetting.Mental);
            //Debug.Log("Social " + Setting.CharacterSetting.Social);
            //Debug.Log("Hearth " + Setting.CharacterSetting.Hearth);
            //Debug.Log("IsGameOver " + IsGameOver().ToString());

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
            SetImageAlpha(transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>(), Time.time / timeEnd);

            UIFadeInTween(timeStart, timeEnd);
            yield return null;
        }

        SetUIToFadeInLoc();

        leftCardImage.GetComponent<Button>().enabled = true;
        rightCardImage.GetComponent<Button>().enabled = true;
        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator ChoiceCard(bool choice)
    {
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + tweenTime;

        if (choice)
        {
            rightCardImage.SetActive(false);
        }
        else
        {
            leftCardImage.SetActive(false);
        }


        while (Time.time < timeEnd1)
        {
            if (choice)
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = LinearEase(t);
                if (leftCardImage.transform.localScale.x < 1.4f)
                {
                    leftCardImage.transform.localScale = new Vector3(1 + v, 1 + v, 1);
                }
                var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, bigCardPosition.localPosition, v);
                leftCardImage.transform.localPosition = position;
            }
            else
            {
                var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
                var v = LinearEase(t);
                if (rightCardImage.transform.localScale.x < 1.4f)
                {
                    rightCardImage.transform.localScale = new Vector3(1 + v, 1 + v, 1);
                }
                var position = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, bigCardPosition.localPosition, v);
                rightCardImage.transform.localPosition = position;
            }

            yield return null;
        }
        if (choice)
        {
            leftCardImage.transform.localPosition = bigCardPosition.localPosition;
            leftCardImage.GetComponent<Button>().enabled = true;
        }
        else
        {
            rightCardImage.transform.localPosition = bigCardPosition.localPosition;
            rightCardImage.GetComponent<Button>().enabled = true;
        }

        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator CancelChoiceCard()
    {
        rightCardImage.SetActive(true);
        leftCardImage.SetActive(true);
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        var timeStart1 = Time.time;
        var timeEnd1 = timeStart1 + tweenTime;

        leftCardImage.transform.SetParent(this.transform);


        while (Time.time < timeEnd1)
        {
            var t = Mathf.InverseLerp(timeStart1, timeEnd1, Time.time);
            var v = LinearEase(t);
            if (leftCardImage.transform.localScale.x > 1.0f)
            {
                leftCardImage.transform.localScale = new Vector3(1.4f - v, 1.4f - v, 1);
            }
            var position = Vector3.LerpUnclamped(leftCardImage.transform.localPosition, leftCardImageFadeInPosition.localPosition, v);
            leftCardImage.transform.localPosition = position;


            if (rightCardImage.transform.localScale.x > 1.0f)
            {
                rightCardImage.transform.localScale = new Vector3(1.4f - v, 1.4f - v, 1);
            }
            position = Vector3.LerpUnclamped(rightCardImage.transform.localPosition, rightCardImageFadeInPosition.localPosition, v);
            rightCardImage.transform.localPosition = position;

            yield return null;
        }
        leftCardImage.transform.localPosition = leftCardImageFadeInPosition.localPosition;
        rightCardImage.transform.localPosition = rightCardImageFadeInPosition.localPosition;

        rightCardImage.GetComponent<Button>().enabled = true;
        leftCardImage.GetComponent<Button>().enabled = true;
        questionImage.GetComponent<Button>().enabled = true;
    }

    IEnumerator TweenIn()
    {
        rightCardImage.SetActive(true);
        leftCardImage.SetActive(true);
        leftCardImage.GetComponent<Button>().enabled = false;
        rightCardImage.GetComponent<Button>().enabled = false;
        questionImage.GetComponent<Button>().enabled = false;

        leftCardImage.transform.SetParent(this.transform);
        rightCardImage.transform.SetParent(this.transform);

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

        choiceLog.Add(choice);

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

        // nextQuestion
        if (nextId == -1)
        {
            StoryManager.nowEvent.question = StoryManager.nowChoice.nextQuestion;
            SetQuestion(StoryManager.nowEvent.question);

            leftCardImage.GetComponent<Button>().enabled = true;
            rightCardImage.GetComponent<Button>().enabled = true;
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

            leftCardImage.SetActive(false);
            rightCardImage.SetActive(false);
            questionFrame.enabled = false;
            questionContent.text = "";

            transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(StoryManager.nowChoice.choiceResults[nextId].imageUrl);
            transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            this.GetComponent<Image>().sprite = null;

            SetImageAlpha(this.GetComponent<Image>(), 0);

            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                SetImageAlpha(whitePanel.GetComponent<Image>(), i);
                yield return null;
            }

            whitePanel.SetActive(false);

            canTouchToNextStory = 1;
        }

    }

    IEnumerator AbsoluteResult()
    {
        SetUIToFadeOutLoc();

        questionFrame.enabled = false;
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

        transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        transform.parent.Find("BackGroundImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(StoryManager.nowChoice.choiceResults[nextId].imageUrl);
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
        AttributeChanged(StoryManager.nowChoice.choiceResults[nextId].valueChanges);
        resultText.text = StoryManager.nowChoice.choiceResults[nextId].content;

        SetImageAlpha(resultBox.GetComponent<Image>(), 0);
        resultBox.SetActive(true);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            if (i < 0.8f)
            {
                SetImageAlpha(resultBox.GetComponent<Image>(), i);
            }
            SetTextAlpha(resultText, i);
            yield return null;
        }
        canTouchToNextStory = 3;

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
        

        int brightTime = 10;
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
                        if (i == 1)
                        {
                            SetTextAlpha(MoneyText, j);
                        }
                    }
                    if (Setting.CharacterSetting.mentalHasChanged != 0)
                    {
                        SetImageAlpha(MentalFilled, j);
                        if (i == 1)
                        {
                            SetTextAlpha(MentalText, j);
                        }
                    }
                    if (Setting.CharacterSetting.socialHasChanged != 0)
                    {
                        SetImageAlpha(SocialHearthFilled, j);
                        if (i == 1)
                        {
                            SetTextAlpha(SocialHearthText, j);
                        }
                    }
                    if (Setting.CharacterSetting.hearthHasChanged != 0)
                    {
                        SetImageAlpha(PhysicalHearthFilled, j);
                        if (i == 1)
                        {
                            SetTextAlpha(PhysicalHearthText, j);
                        }
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

        for (float j = 1; j >= 0; j -= Time.deltaTime * 0.5f)
        {
            if (Setting.CharacterSetting.moneyHasChanged != 0)
            {
                SetTextAlpha(MoneyText, j);
            }
            if (Setting.CharacterSetting.mentalHasChanged != 0)
            {
                SetTextAlpha(MentalText, j);
            }
            if (Setting.CharacterSetting.socialHasChanged != 0)
            {
                SetTextAlpha(SocialHearthText, j);
            }
            if (Setting.CharacterSetting.hearthHasChanged != 0)
            {
                SetTextAlpha(PhysicalHearthText, j);
            }
            yield return null;
        }

    }

    /*
    * P心理Mental
    * S社交Social
    * H健康Hearth
    * $(萬)
    * example: "P + 50", "P - 3 5"
    */
    public void AttributeChanged(List<string> changed)
    {
        Setting.CharacterSetting.moneyHasChanged = 0;
        Setting.CharacterSetting.mentalHasChanged = 0;
        Setting.CharacterSetting.hearthHasChanged = 0;
        Setting.CharacterSetting.socialHasChanged = 0;

        SetTextAlpha(MoneyText, 0);
        SetTextAlpha(MentalText, 0);
        SetTextAlpha(SocialHearthText, 0);
        SetTextAlpha(PhysicalHearthText, 0);


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
                        SetTextAlpha(MoneyText, 0);
                        MoneyText.text = "+" + value.ToString();
                        break;
                    case "P":
                        Setting.CharacterSetting.mentalHasChanged = 1;
                        //mentalResult = "心理指數 ++" + value.ToString() + "\n";
                        MentalText.text = "+" + value.ToString();
                        Setting.CharacterSetting.Mental += value;
                        break;
                    case "S":
                        Setting.CharacterSetting.socialHasChanged = 1;
                        //socialResult = "Social Index + " + value.ToString() + "\n";
                        SocialHearthText.text = "+" + value.ToString();
                        Setting.CharacterSetting.Social += value;
                        break;
                    case "H":
                        Setting.CharacterSetting.hearthHasChanged = 1;
                        //hearthResult = "Physiologic Index + " + value.ToString() + "\n";
                        PhysicalHearthText.text = "+" + value.ToString();
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
                        MoneyText.text = "-" + value.ToString();
                        break;
                    case "P":
                        Setting.CharacterSetting.mentalHasChanged = -1;
                        //mentalResult = "Mental Index - " + value.ToString() + "\n";
                        MentalText.text = "-" + value.ToString();
                        Setting.CharacterSetting.Mental -= value;
                        break;
                    case "S":
                        Setting.CharacterSetting.socialHasChanged = -1;
                        //socialResult = "Social Index - " + value.ToString() + "\n";
                        SocialHearthText.text = "-" + value.ToString();
                        Setting.CharacterSetting.Social -= value;
                        break;
                    case "H":
                        Setting.CharacterSetting.hearthHasChanged = -1;
                        PhysicalHearthText.text = "-" + value.ToString();
                        Setting.CharacterSetting.Hearth -= value;
                        break;
                }
            }
        }
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
