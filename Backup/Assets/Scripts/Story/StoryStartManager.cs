using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryStartManager : MonoBehaviour {
    public Image storyImage;
    public GameObject storyChoice;
    private float scale = 0.71f;
    private float scaleTime = 3.0f;
    private void Awake()
    {
        Debug.Log("new Story, now ID = " + StoryManager.nowId);
        StoryManager.NextEvent();   // 選出此事件

        SetChoice();
        storyChoice.SetActive(false);

        storyImage.transform.localScale = new Vector3(scale, scale, 0);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(StoryStart());
        // TODO: 對話
        
    }

    // Update is called once per frame
    void Update () {
		
	}
        
    void SetChoice()
    {
        // TODO: 圖 = nowStory.imageUrl
        storyChoice.GetComponent<StoryHappenManager>().content.text = StoryManager.nowStory.questionText;
        storyChoice.GetComponent<StoryHappenManager>().trueText.text = StoryManager.nowStory.trueChoice.text;
        storyChoice.GetComponent<StoryHappenManager>().falseText.text = StoryManager.nowStory.falseChoice.text;
        storyChoice.GetComponent<StoryHappenManager>().helpText.text = StoryManager.nowStory.hintText;
        storyChoice.GetComponent<StoryHappenManager>().trueChoice = StoryManager.nowStory.trueChoice;
        storyChoice.GetComponent<StoryHappenManager>().falseChoice = StoryManager.nowStory.falseChoice;
    }

    IEnumerator StoryStart()
    {
        for (float i = 0f; i <= scaleTime; i += Time.deltaTime)
        {
            if(i > scale * scaleTime)
            {
                storyImage.transform.localScale = new Vector3(i / scaleTime, i / scaleTime, 0);
            }
            yield return null;
        }
        storyImage.transform.localScale = new Vector3(1, 1, 0);

        for (float i = 0f; i <= 1; i += Time.deltaTime)
        {
            yield return null;
        }

        storyChoice.SetActive(true);
    }
}
