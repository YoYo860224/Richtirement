using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCreate : MonoBehaviour {

    private void Awake()
    {
        {
            int id = 0;
            string imageUrl = "0.png";                                             // 圖的url
            string content = "轉眼間也到了在十年就要退休的年紀呢！";                  // 開頭簡介

            Question q0 = new Question("有了一點存款，做點理財嗎？")
            {
                imageUrl = "",
                hint = "hint test",
                leftChoice = new Choice("Yes"),
                rightChoice = new Choice("No")
            };

            Question q0_1 = new Question("做點什麼？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("買股票"),
                rightChoice = new Choice("買儲蓄險")
            };

            ChoiceResult r0_1 = new ChoiceResult("結果")
            {
                imageUrl = "0_1.png",
                prob = 0.3f,
                valueChanges = new List<string> { "P + 5 8" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_2 = new ChoiceResult("結果")
            {
                imageUrl = "0_2.png",
                prob = 0.7f,
                valueChanges = new List<string> { "P - 5 8" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_3 = new ChoiceResult("結果")
            {
                imageUrl = "0_3.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 1 5" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_4 = new ChoiceResult("結果")
            {
                imageUrl = "0_4.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 2 5" },
                nextIds = new List<int> { 777 }
            };

            q0.leftChoice.AfterChoiceDo(q0_1);
            q0.rightChoice.AfterChoiceDo(r0_4);
            q0_1.leftChoice.AfterChoiceDo(r0_1);
            q0_1.leftChoice.AfterChoiceDo(r0_2);
            q0_1.rightChoice.AfterChoiceDo(r0_3);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q0));
            StoryManager.futureEventsID.Add(0);
        }
        // ============================================
        {
            int id = 1;
            string imageUrl = "1.png";
            string content = "60歲了，小孩也都長大了，再五年就可以享受退休生活了，在努力一下吧！";

            Question q1 = new Question("老闆叫你平日加班假日上班，你會？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("加到底"),
                rightChoice = new Choice("花時間陪陪家人")
            };

            Question q1_1 = new Question("老闆叫你去應酬？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("去"),
                rightChoice = new Choice("不去，回家")
            };

            Question q1_2 = new Question("做點什麼？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("踏青"),
                rightChoice = new Choice("看電影")
            };

            ChoiceResult r1_1 = new ChoiceResult("結果")
            {
                imageUrl = "1_1.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 6 15", "H - 6 15", "S + 5 8" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_2 = new ChoiceResult("結果2")
            {
                imageUrl = "1_2.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 1 3", "H - 1 3", "S - 5 8" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_3 = new ChoiceResult("結果3")
            {
                imageUrl = "1_3.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 3 6", "H + 2 5", "S + 3 6" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_4 = new ChoiceResult
            {
                imageUrl = "1_4.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 3 6", "H - 1 4", "S + 2 5" },
                nextIds = new List<int> { 2 }
            };

            q1.leftChoice.AfterChoiceDo(q1_1);
            q1.rightChoice.AfterChoiceDo(q1_2);
            q1_1.leftChoice.AfterChoiceDo(r1_1);
            q1_1.rightChoice.AfterChoiceDo(r1_2);
            q1_2.leftChoice.AfterChoiceDo(r1_3);
            q1_2.rightChoice.AfterChoiceDo(r1_4);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q1));
        }
        // ============================================
        {
            int id = 2;
            string imageUrl = "2.png";
            string content = "";

            Question q2 = new Question("公司倒閉，未來該何去何從？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("提前退休"),
                rightChoice = new Choice("找新工作")
            };

            ChoiceResult r2_1 = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { 777 }
            };

            ChoiceResult r2_2 = new ChoiceResult("結果2")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 777 }
            };

            q2.leftChoice.AfterChoiceDo(r2_1);
            q2.rightChoice.AfterChoiceDo(r2_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q2));
        }
        // ============================================
        {
            int id = 777;
            string imageUrl = "777.png";
            string content = "原本以為可以健健康康過完這一生，竟然在80歲這年...";

            Question q777 = new Question("123")
            {
                imageUrl = "",
                hint = "hint test",
                absoluteChoice = new Choice("Absolute")
            };

            ChoiceResult r777 = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 15" },
                nextIds = new List<int> { -1 }
            };

            q777.absoluteChoice.AfterChoiceDo(r777);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q777));
        }
    }
}
