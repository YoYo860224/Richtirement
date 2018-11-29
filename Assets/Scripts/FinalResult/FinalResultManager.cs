using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Setting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalResultManager : MonoBehaviour {
    public Image storyImage;
    public GameObject resultPanel;
    public Text resultText;

    private int backTouch = 0;
    private int resultPanelTouch = 0;

    private void Awake()
    {
        SetImageAlpha(storyImage, 0);

        SetImageAlpha(resultPanel.GetComponent<Image>(), 0);
        SetTextAlpha(resultText, 0);
        resultPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        SetContent();
        StartCoroutine(StartShow());
    }

    // Update is called once per frame
    void Update () {

	}

    bool IsGameOver()
    {
        return Setting.CharacterSetting.Hearth <= 0 || Setting.CharacterSetting.Money <= 0 || Setting.CharacterSetting.Mental <= 0 || Setting.CharacterSetting.Social <= 0;
    }

    public void BackGroundTouched()
    {
        if(backTouch == 1)
        {
            StartCoroutine(ShowResultBox());
            backTouch = 2;
        }
    }
    public void ResultBoxTouched()
    {
        if(resultPanelTouch == 1)
        {
            //StartCoroutine(EndResult());

            if (IsGameOver())
            {
                SceneManager.LoadScene("Splash");
            }
            else
            {
                SceneManager.LoadScene("AnalysisReport");
            }
        }
    }

    IEnumerator StartShow()
    {
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            SetImageAlpha(storyImage, i / 2);
            yield return null;
        }
        backTouch = 1;
    }

    IEnumerator ShowResultBox()
    {
        resultPanel.SetActive(true);
        for (float i = 0;i < 1; i += Time.deltaTime)
        {
            if(i < 0.8f)
            {
                SetImageAlpha(resultPanel.GetComponent<Image>(), i);
            }
            SetTextAlpha(resultText, i);
            yield return null;
        }
        resultPanelTouch = 1;
    }

    IEnumerator EndResult()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            SetImageAlpha(storyImage, i / 2);

            if (i < 1.6f)
            {
                SetImageAlpha(resultPanel.GetComponent<Image>(), i / 2);
            }
            SetTextAlpha(resultText, i / 2);
            yield return null;
        }
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

    void SetContent()
    {
        if (IsGameOver())
        {
            if(Setting.CharacterSetting.Money <= 0)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/Money0");
                resultText.text = "Money 0\nGameOver";
            }
            else if (Setting.CharacterSetting.Mental <= 0)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/Mental0");
                resultText.text = "Mental 0\nGameOver";
            }
            else if (Setting.CharacterSetting.Hearth <= 0)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/Hearth0");
                resultText.text = "Hearth 0\nGameOver";
            }
            else if (Setting.CharacterSetting.Social <= 0)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/Social0");
                resultText.text = "Social 0\nGameOver";
            }
        }
        else
        {
            if (Setting.CharacterSetting.Money > 50 && Setting.CharacterSetting.Mental > 50 && Setting.CharacterSetting.Social > 50 && Setting.CharacterSetting.Hearth > 50
            && Setting.CharacterSetting.Money > Setting.CharacterSetting.Mental
            && Setting.CharacterSetting.Money > Setting.CharacterSetting.Social
            && Setting.CharacterSetting.Money > Setting.CharacterSetting.Hearth)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/ColorfulLife");
                resultText.text = "身心裡與社交都保持優良金融管理良好，是個多彩多姿的人生呢！";
            }
            else if (Setting.CharacterSetting.Mental > 0
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Social
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Hearth)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/HappyLife");
                resultText.text = "心理狀態優良，人生不用過的太富有，但保持心情愉悅才是最重要的，是吧？";
            }
            else if (Setting.CharacterSetting.Hearth > 0
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Social
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Mental)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/VigorousLife");
                resultText.text = "健康狀態優良，人生果然要保有健康才能做更多想做的事呢！";
            }
            else if (Setting.CharacterSetting.Social > 0
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Hearth
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Mental)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/BalenceLife");
                resultText.text = "有良好的社交狀態，這一生中遇到許多交心的朋友、職場的恩人、甚至與最重要的親人也保有良好的關係。";
            }
            else
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/NothingButMoney");
                resultText.text = "即使擁有足夠生活的條件與經濟狀況，卻沒有良好的運用與有品質的生活，活著的意義到底是什麼呢？";
            }
        }
    }
}
