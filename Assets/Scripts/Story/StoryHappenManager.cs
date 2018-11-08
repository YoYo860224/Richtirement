using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using UnityEngine.SceneManagement;

public class StoryHappenManager : MonoBehaviour {
    public Image MoneyFilled;
    public Image MentalFilled;
    public Image PhysicalHearthFilled;
    public Image SocialHearthFilled;

    public Text content;

    public Text trueText;
    public Text falseText;

    public GameObject helpField;
    public Image helpImage;
    public Text helpText;

    private static bool canTouch = true;
    private static bool helpState = false;
    public Choice trueChoice;
    public Choice falseChoice;

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

    //IEnumerator tween(GameObject gameObject, float duration, Vector3 posStart, Vector3 posEnd)
    //{
    //    var timeStart = Time.time;
    //    var timeEnd = timeStart + duration;
    //    while (Time.time < timeEnd)
    //    {
    //        var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
    //        var v = CubicEaseOut(t);
    //        var position = Vector3.LerpUnclamped(posStart, posEnd, v);
    //        gameObject.transform.localPosition = position;
    //        yield return null;
    //    }
    //    gameObject.transform.localPosition = posEnd;
    //}

    //// Linear
    //float LinearEase(float t)
    //{
    //    return t;
    //}

    //// CubicIn
    //float CubicEaseIn(float t)
    //{
    //    return t * t * t;
    //}

    //// CubicOut
    //float CubicEaseOut(float t)
    //{
    //    return ((t = t - 1) * t * t + 1);
    //}

    //IEnumerator SliderChange(Slider slider, float value)
    //{
    //    bool add = value > slider.value;
    //    // fade from opaque to transparent
    //    if (add)
    //    {
    //        // loop over 1 second backwards
    //        for (float i = 1; i >= 0; i -= Time.deltaTime)
    //        {
    //            // set color with i as alpha
    //            if(slider.value < value)
    //            {
    //                slider.value += i;
    //            }
    //            yield return null;
    //        }
    //    }
    //    // fade from transparent to opaque
    //    else
    //    {
    //        // loop over 1 second
    //        for (float i = 0; i <= 1; i += Time.deltaTime)
    //        {
    //            // set color with i as alpha
    //            if (slider.value > value)
    //            {
    //                slider.value -= i;
    //            }
    //            yield return null;
    //        }
    //    }
    //    slider.value = value;
    //}


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


    public void choiceYes()
    {
        //Setting.CharacterSetting.SocialHearth -= 5;
        //StartCoroutine(SliderChange(SocialHearth, Setting.CharacterSetting.SocialHearth));
        int nextId = trueChoice.NextEvent();

        // TODO: showResult

        EventLog log = new EventLog(StoryManager.nowId, true, nextId);
        StoryManager.EndNowStory(log);

        // 判斷是不是5年
        SceneManager.LoadScene("Story");
    }

    public void choiceNo()
    {
        int nextId = trueChoice.NextEvent();
        Debug.Log("next ID = " + nextId);
        // TODO: showResult

        EventLog log = new EventLog(StoryManager.nowId, true, nextId);
        StoryManager.EndNowStory(log);

        // 判斷是不是5年
        SceneManager.LoadScene("Story");
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
}
