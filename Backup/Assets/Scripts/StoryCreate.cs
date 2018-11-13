using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCreate : MonoBehaviour {

    private void Awake()
    {
        int id = 0;
        string imageUrl = "0.png";                                                  // 圖的url
        List<string> content = new List<string> { ""};                              // 開頭簡介
        string questionText = "有了一點存款，做點理財嗎?";                          // 問題內容
        string hintText = "hint0";                                                  // hint的Text

        Choice trueChoice = new Choice("Yes"); // text
        // id, imageUrl, prob, result, changeValue
        trueChoice.AddEventAtChoice(0, "1.png", 0.5f, "$ -982塊 / 每月", new List<string> { "P + 1 3" });
        trueChoice.AddEventAtChoice(0, "2.png", 0.5f, "$ -982塊 / 每月", new List<string> { "P + 1 3" });

        Choice falseChoice = new Choice("No"); // text
        // id, imageUrl, prob, result, changeValue
        falseChoice.AddEventAtChoice(0, "0.png", 1.0f, "存一般定存", new List<string> { "P + 1", "S 1", "H 1" });

        StoryManager.AddStory(new StoryContent(id,imageUrl, questionText, content, hintText, trueChoice, falseChoice));

        // ============================================

        id = 1;
        imageUrl = "1.png";                                                  // 圖的url
        content = new List<string> { "hello", "I'm", "Peter Shen" };   // 開頭簡介
        questionText = "question 1";                                         // 問題內容
        hintText = "hint1";                                               // hint的Text

        trueChoice = new Choice("Yes"); // text
        // id, imageUrl, prob, result, changeValue
        trueChoice.AddEventAtChoice(0, "0.png", 1.0f,  "yesyesyes", new List<string> { "P -1", "S -1", "H -1" });
        trueChoice.AddEventAtChoice(1, "0.png", 0f, "yesyesyes", new List<string> { "P -1", "S -1", "H -1" });

        falseChoice = new Choice("No"); // text
        // id, imageUrl, prob, result, changeValue
        falseChoice.AddEventAtChoice(0, "0.png", 1.0f, "nonono idk" , new List<string> { "P 1", "S 1", "H 1" });

        StoryManager.AddStory(new StoryContent(id, imageUrl, questionText, content, hintText, trueChoice, falseChoice));


        StoryManager.willHappenEventId.Add(0);
    }

}
