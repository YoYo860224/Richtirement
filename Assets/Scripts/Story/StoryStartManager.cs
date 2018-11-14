using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryStartManager : MonoBehaviour {
    public Image storyImage;

    public Image storyContentImage;
    public Text storyContentText;

    public GameObject storyChoice;
    //private float scale = 0.71f;
    private float scaleTime = 3.0f;
    private void Awake()
    {
        StoryManager.NextEvent();   // 選出此事件
        Debug.Log("new Story, now ID = " + StoryManager.nowEvent.id);

        SetChoice();
        storyChoice.SetActive(false);

        //storyImage.transform.localScale = new Vector3(scale, scale, 0);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(StoryContentStart());
    }

    // Update is called once per frame
    void Update () {
		
	}
        
    void SetChoice()
    {
        // TODO: 圖 = nowStory.imageUrl
        storyChoice.GetComponent<StoryHappenManager>().content.text = StoryManager.nowEvent.question.content;
        storyChoice.GetComponent<StoryHappenManager>().leftText.text = StoryManager.nowEvent.question.leftChoice.content;
        storyChoice.GetComponent<StoryHappenManager>().rightText.text = StoryManager.nowEvent.question.rightChoice.content;
        storyChoice.GetComponent<StoryHappenManager>().helpText.text = StoryManager.nowEvent.question.hint;
        storyChoice.GetComponent<StoryHappenManager>().trueChoice = StoryManager.nowEvent.question.leftChoice;
        storyChoice.GetComponent<StoryHappenManager>().falseChoice = StoryManager.nowEvent.question.rightChoice;
    }

    public void ClickContent()
    {
        StartCoroutine(StartChoice());
    }

    IEnumerator StoryContentStart()
    {
        storyContentText.text = StoryManager.nowEvent.content;
        for (float i = 0f; i <= 1; i += Time.deltaTime)
        {
            var tempColor = storyContentText.color;
            tempColor.a = i;
            storyContentText.color = tempColor;

            if (i < 0.8f)
            {
                tempColor = storyContentImage.color;
                tempColor.a = i;
                storyContentImage.color = tempColor;
            }

            yield return null;
        }
    }

    IEnumerator StartChoice()
    {
        for (float i = 1f; i >= 0; i -= Time.deltaTime)
        {
            var tempColor = storyContentText.color;
            tempColor.a = i;
            storyContentText.color = tempColor;

            if (i < 0.8f)
            {
                tempColor = storyContentImage.color;
                tempColor.a = i;
                storyContentImage.color = tempColor;
            }
            yield return null;
        }

        storyChoice.SetActive(true);
        storyChoice.GetComponent<StoryHappenManager>().StartChoice();
    }

    //IEnumerator StoryStart()
    //{
    //    for (float i = 0f; i <= scaleTime; i += Time.deltaTime)
    //    {
    //        if(i > scale * scaleTime)
    //        {
    //            storyImage.transform.localScale = new Vector3(i / scaleTime, i / scaleTime, 0);
    //        }
    //        yield return null;
    //    }
    //    storyImage.transform.localScale = new Vector3(1, 1, 0);

    //    for (float i = 0f; i <= 1; i += Time.deltaTime)
    //    {
    //        yield return null;
    //    }

    //    storyChoice.SetActive(true);
    //}
}
