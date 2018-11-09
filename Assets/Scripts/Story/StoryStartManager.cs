using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryStartManager : MonoBehaviour {
    public Image storyImage;
    public GameObject storyChoice;

    private void Awake()
    {
        Debug.Log("new Story, now ID = " + StoryManager.nowId);
        StoryManager.NextEvent();   // 選出此事件

        SetChoice();
        storyChoice.SetActive(false);

    }

    // Use this for initialization
    void Start () {

        // TODO: 對話

        storyChoice.SetActive(true);
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
}
