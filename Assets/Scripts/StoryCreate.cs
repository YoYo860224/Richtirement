using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCreate : MonoBehaviour {

    private void Awake()
    {
        int id = 0;
        string imageUrl = "0.png";                                            // 圖的url
        string content = "轉眼間也到了在十年就要退休的年紀呢！";                  // 開頭簡介

        Question q0 = new Question("有了一點存款，做點理財嗎？")
        {
            imageUrl = "0.png",
            hint = "",
            leftChoice = new Choice("Yes"),
            rightChoice = new Choice("No")
        };

        Question q0_1 = new Question("做點什麼？")
        {
            imageUrl = "",
            hint = "hint test",
            leftChoice = new Choice("買股票"),
            rightChoice = new Choice("買儲蓄險")
        };

        ChoiceResult r1 = new ChoiceResult("結果1")
        {
            imageUrl = "0_1.png",
            prob = 0.3f,
            valueChanges = new List<string> { "P + 5 8" },
            nextIds = new List<int> { 1 }
        };

        ChoiceResult r2 = new ChoiceResult("結果2")
        {
            imageUrl = "0_2.png",
            prob = 0.7f,
            valueChanges = new List<string> { "P - 5 8" },
            nextIds = new List<int> { 1 }
        };

        ChoiceResult r3 = new ChoiceResult("結果3")
        {
            imageUrl = "0_3.png",
            prob = 1.0f,
            valueChanges = new List<string> { "P + 1 5" },
            nextIds = new List<int> { 1 }
        };

        ChoiceResult r4 = new ChoiceResult
        {
            imageUrl = "0_4.png",
            prob = 1.0f,
            valueChanges = new List<string> { "P - 2 5" },
            nextIds = new List<int> { 1 }
        };

        q0.leftChoice.AfterChoiceDo(q0_1);
        q0.rightChoice.AfterChoiceDo(r4);
        q0_1.leftChoice.AfterChoiceDo(r1);
        q0_1.leftChoice.AfterChoiceDo(r2);
        q0_1.rightChoice.AfterChoiceDo(r3);

        StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q0));
        StoryManager.futureEventsID.Add(0);

        // ============================================

        //id = 1;
        //imageUrl = "1.png";                                              
        //content = "60歲了，小孩也都長大了，再五年就可以享受退休生活了，在努力一下吧！";   
        //questionText = "老闆叫你平日加班假日上班，你會？";                                  
        //hintText = "";                                                                     

        //leftChoice = new Choice("加到底"); // text
        //questionLeftChoice = new Choice("去");
        //questionLeftChoice.AddEventAtChoice(2, "1_1.png", 1.0f, "Result3", new List<string> { "H - 3 8", "P - 6 15", "S + 5 8" });
        //questionRightChoice = new Choice("不去，回家");
        //questionRightChoice.AddEventAtChoice(2, "1_2.png", 1.0f, "Result4", new List<string> { "P - 1 3", "H - 1 3", "S - 5 8" });
        //leftQuestion = new Question("老闆叫你去應酬?", "", "", questionLeftChoice, questionRightChoice);
        //leftChoice.question = leftQuestion;

        //rightChoice = new Choice("花時間陪陪家人"); // text
        //questionLeftChoice = new Choice("踏青");
        //questionLeftChoice.AddEventAtChoice(2, "1_3.png", 1.0f, "Result3", new List<string> { "H + 2 5", "P + 3 6", "S + 3 6" });
        //questionRightChoice = new Choice("看電影");
        //questionRightChoice.AddEventAtChoice(2, "1_4.png", 1.0f, "Result4", new List<string> { "P + 2 5", "H - 1 4", "S + 2 5" });
        //rightQuestion = new Question("做點什麼?", "", "", questionLeftChoice, questionRightChoice);
        //rightChoice.question = rightQuestion;

        //StoryManager.AddStory(new StoryEvent(id, imageUrl, questionText, content, hintText, leftChoice, rightChoice));

        //StoryManager.futureEvents.Add(0);
    }

}
