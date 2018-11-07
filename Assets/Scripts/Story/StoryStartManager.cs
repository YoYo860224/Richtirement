using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStartManager : MonoBehaviour {
    public GameObject storyChoicePrefab;
    public Canvas canvas;
    GameObject storyChoice;
	// Use this for initialization
	void Start () {
        // TODO: 對話
        ShowChoice();
    }

    // Update is called once per frame
    void Update () {
		
	}


    void ShowChoice()
    {
        storyChoice = Instantiate(storyChoicePrefab);
        storyChoice.GetComponent<Transform>().SetParent(canvas.transform);
        storyChoice.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        storyChoice.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        storyChoice.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
    }
}
