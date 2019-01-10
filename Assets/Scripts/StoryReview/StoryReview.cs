using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Setting;
using UnityEngine.SceneManagement;

public class StoryReview : MonoBehaviour {
    public GameObject content;
    public GameObject StoryBoxPrefab;

    private List<string> question = new List<string>();
    private List<string> choice = new List<string>();
    private void Awake()
    {
        StoryManager.log.Add(new EventLog(0, new List<bool>() { true, true }));
        StoryManager.log.Add(new EventLog(1, new List<bool>() { false, true }));
        StoryManager.log.Add(new EventLog(2, new List<bool>() { false }));
        StoryManager.log.Add(new EventLog(3, new List<bool>() { true, true }));
        StoryManager.log.Add(new EventLog(4, new List<bool>() { true }));

        int eventAmount = 0;
        for (int i = 0; i < StoryManager.log.Count; i++)
        {
            Debug.Log(StoryManager.log[i].id);
            eventAmount += StoryManager.log[i].choice.Count;

            Question tempQuestion = StoryManager.AllSEventList[StoryManager.log[i].id].question;
            for (int j = 0; j < StoryManager.log[i].choice.Count; j++)
            {
                question.Add(tempQuestion.content);
                Debug.Log(tempQuestion.content);
                if (tempQuestion.absoluteChoice == null)
                {
                    if (StoryManager.log[i].choice[j])
                    {
                        choice.Add(tempQuestion.leftChoice.content);
                        Debug.Log(tempQuestion.leftChoice.content);

                        tempQuestion = tempQuestion.leftChoice.nextQuestion;
                    }
                    else
                    {
                        choice.Add(tempQuestion.rightChoice.content);
                        Debug.Log(tempQuestion.rightChoice.content);

                        tempQuestion = tempQuestion.rightChoice.nextQuestion;
                    }
                }
                else
                {
                    choice.Add("");
                }

            }
        }
        // 使用 vertical layout 修正看看

        //RectTransform rt = content.GetComponent<RectTransform>();
        //rt.sizeDelta = new Vector2(rt.sizeDelta.x, 520 * eventAmount + 250);
        // content.transform.localPosition = new Vector2(0, 0);
        for (int i = 0; i < eventAmount; i++)
        {
            GameObject instance = Instantiate(StoryBoxPrefab) as GameObject;  
            instance.transform.SetParent(content.transform);
            instance.transform.localScale = new Vector3(1, 1, 1);
            instance.GetComponent<StoryBox>().SetText(question[i], choice[i]);
            // instance.transform.localPosition = new Vector2(0, i * (-520));
        }
        content.transform.Find("Button").transform.SetSiblingIndex(eventAmount + 1);
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ToAnalysis()
    {
        SceneManager.LoadScene("Splash");
    }
}
