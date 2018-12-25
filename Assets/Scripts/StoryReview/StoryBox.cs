using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBox : MonoBehaviour {
    public Text Question;
    public Text Choice;

    public void SetText(string question, string choice)
    {
        Question.text = question;
        Choice.text = choice;
    }

}
