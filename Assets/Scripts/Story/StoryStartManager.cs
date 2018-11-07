using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryStartManager : MonoBehaviour {
    public GameObject storyChoicePrefab;
    public GameObject panel;
    GameObject storyChoice;
    StoryContent nowStory;
    // Use this for initialization
    void Start () {
        Debug.Log("new Story");

        StoryManager.NextEvent();   // 選出下一個事件
        Debug.Log("now ID = " + StoryManager.nowId);
        // TODO: 對話
        nowStory = StoryManager.storyList[StoryManager.nowId];

        ShowChoice();
    }

    // Update is called once per frame
    void Update () {
		
	}
        

    void ShowChoice()
    {
        storyChoice = Instantiate(storyChoicePrefab);
        storyChoice.GetComponent<Transform>().SetParent(panel.transform);
        storyChoice.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        storyChoice.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        storyChoice.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

        // TODO: 圖 = nowStory.imageUrl
        storyChoice.GetComponent<StoryHappenManager>().content.text = nowStory.questionText;
        storyChoice.GetComponent<StoryHappenManager>().trueText.text = nowStory.trueChoice.text;
        storyChoice.GetComponent<StoryHappenManager>().falseText.text = nowStory.falseChoice.text;
        storyChoice.GetComponent<StoryHappenManager>().helpText.text = nowStory.hintText;
        storyChoice.GetComponent<StoryHappenManager>().trueChoice = nowStory.trueChoice;
        storyChoice.GetComponent<StoryHappenManager>().falseChoice = nowStory.falseChoice;
    }
}
