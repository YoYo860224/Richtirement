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
                resultText.text = "恭喜你！\n不僅金融管理能力佳，還能兼顧健康與心靈平衡、維持良好社交生活，擁有著人人稱羨、豐餘富足、多采多姿的退休生活！\n\n請繼續保持下去，開創嶄新的退休人生扉頁。";
            }
            else if (Setting.CharacterSetting.Mental > 0
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Social
                && Setting.CharacterSetting.Mental > Setting.CharacterSetting.Hearth)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/VigorousLife");
                resultText.text = "或許累積資產不是那麼的多，但懂得如何維持健康心理狀態。\n\n若與人有不錯的社交交流，提升健康層面，這也不失為另一種富足的退休生活。";
            }
            else if (Setting.CharacterSetting.Hearth > 0
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Social
                && Setting.CharacterSetting.Hearth > Setting.CharacterSetting.Mental)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/HappyLife");
                resultText.text = "健康是良好退休生活的根本原則，掌握得相當好，擁有良好生理狀態！\n\n也別忘要多尋求各領域專業資源，累積更平穩資產收入，同時積極參與社交活動也是維持心理健康的秘方哦！";
            }
            else if (Setting.CharacterSetting.Social > 0
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Money
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Hearth
                && Setting.CharacterSetting.Social > Setting.CharacterSetting.Mental)
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/BalenceLife");
                resultText.text = "積極參與社交是一項無形的資產，相信已擁有許多交心朋友、職場貴人，同時也與最重要的親人有緊密的良好關係。\n\n且試著從社交人脈中去找尋專家，請益維持健康、累積資產、提升心理愉悅的方法，讓退休生活達到更佳的平衡。";
            }
            else
            {
                storyImage.sprite = Resources.Load<Sprite>("Story/NothingButMoney");
                resultText.text = "擁有足夠維持生活的良好條件與經濟狀態，卻因沒有良好運用導致過著沒有品質的生活，心理層面也未因資產而獲得滿足。\n\n建議嘗試更好的資產運用方式，讓生理、心理、社交層面為良好均衡狀態，提升退休生活的品質。";
            }
        }
    }
}
