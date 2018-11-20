using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCreate : MonoBehaviour {

    private void Awake()
    {
        {
            int id = 0;
            string imageUrl = "Story/01";                                          // 圖的url
            string content = "轉眼間也到了在十年就要退休的年紀呢！";                  // 開頭簡介

            Question q0 = new Question("有了一點存款，做點理財嗎？")
            {
                imageUrl = "Story/02",
                hint = "hint test",
                leftChoice = new Choice("Yes"),
                rightChoice = new Choice("No")
            };

            Question q0_1 = new Question("做點什麼？")
            {
                imageUrl = "Story/03",
                hint = "",
                leftChoice = new Choice("買股票"),
                rightChoice = new Choice("買儲蓄險")
            };

            ChoiceResult r0_1 = new ChoiceResult("結果")
            {
                imageUrl = "Story/03",
                prob = 0.3f,
                valueChanges = new List<string> { "P + 5 8" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_2 = new ChoiceResult("結果")
            {
                imageUrl = "Story/03",
                prob = 0.7f,
                valueChanges = new List<string> { "P - 5 8" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_3 = new ChoiceResult("結果")
            {
                imageUrl = "Story/03",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 1 5" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_4 = new ChoiceResult("結果")
            {
                imageUrl = "Story/03",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 2 5" },
                nextIds = new List<int> { 1 }
            };

            q0.leftChoice.AfterChoiceDo(q0_1);
            q0.rightChoice.AfterChoiceDo(r0_4);
            q0_1.leftChoice.AfterChoiceDo(r0_1);
            q0_1.leftChoice.AfterChoiceDo(r0_2);
            q0_1.rightChoice.AfterChoiceDo(r0_3);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q0, 5));
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

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q1, 5));
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
                nextIds = new List<int> { 3 }
            };

            ChoiceResult r2_2 = new ChoiceResult("結果2")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 3 }
            };

            q2.leftChoice.AfterChoiceDo(r2_1);
            q2.rightChoice.AfterChoiceDo(r2_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q2, 5));
        }
        // ============================================
        {
            int id = 3;
            string imageUrl = "3.png";
            string content = "恭喜你退休了！準備好過想過的人生嗎：）";

            Question q = new Question("小孩要創業跟你借200萬")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("借"),
                rightChoice = new Choice("不借")
            };

            Question q_1 = new Question("小孩生意失敗，要跟他要錢嗎？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("要"),
                rightChoice = new Choice("不要")
            };

            ChoiceResult r_1 = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { 4 }
            };

            ChoiceResult r_2 = new ChoiceResult("結果")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 4 }
            };

            ChoiceResult r_3 = new ChoiceResult("結果")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 4 }
            };

            q.leftChoice.AfterChoiceDo(q_1);
            q.rightChoice.AfterChoiceDo(r_3);
            q_1.leftChoice.AfterChoiceDo(r_1);
            q_1.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 5));
        }
        // ============================================
        {
            int id = 4;
            string imageUrl = "3.png";
            string content = "退休也五年了，時間真的不等人呢．．．";

            Question q = new Question("培養個興趣吧？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("好，多出去走走"),
                rightChoice = new Choice("不了，很花錢的")
            };

            ChoiceResult r_1 = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { 5 }
            };

            ChoiceResult r_2 = new ChoiceResult("結果")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 5 }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 5));
        }
        // ============================================
        {
            int id = 5;
            string imageUrl = "3.png";
            string content = "";

            Question q = new Question("跟老朋友聚聚嗎？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("好！"),
                rightChoice = new Choice("不了。")
            };

            ChoiceResult r_1 = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { 6 }
            };

            ChoiceResult r_2 = new ChoiceResult("結果")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { 6 }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 5));
        }
        // ============================================
        {
            int id = 6;
            string imageUrl = "./Story/01.png";                                             
            string content = "恭喜你退休了！準備好過想過的人生嗎：）";               

            Question q0 = new Question("朋友找你投資150萬創業，你會？")
            {
                imageUrl = "",
                hint = "hint test",
                leftChoice = new Choice("投資"),
                rightChoice = new Choice("不投資")
            };

            Question q0_1 = new Question("面對生意的經營，你會？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("隨時到場監督"),
                rightChoice = new Choice("每月查看報表")
            };

            ChoiceResult r0_1 = new ChoiceResult("結果")
            {
                imageUrl = "0_1.png",
                prob = 0.5f,
                valueChanges = new List<string> { "P + 5 8" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_2 = new ChoiceResult("結果")
            {
                imageUrl = "0_2.png",
                prob = 0.5f,
                valueChanges = new List<string> { "P - 5 8" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_3 = new ChoiceResult("結果")
            {
                imageUrl = "0_3.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 1 5" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_4 = new ChoiceResult("結果")
            {
                imageUrl = "0_4.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 2 5" },
                nextIds = new List<int> { 7 }
            };

            q0.leftChoice.AfterChoiceDo(q0_1);
            q0.rightChoice.AfterChoiceDo(r0_4);
            q0_1.leftChoice.AfterChoiceDo(r0_1);
            q0_1.leftChoice.AfterChoiceDo(r0_2);
            q0_1.rightChoice.AfterChoiceDo(r0_3);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q0, 5));
        }
        // ============================================
        {
            int id = 7;
            string imageUrl = "777.png";
            string content = "原本以為可以健健康康過完這一生，竟然在80歲這年...";

            Question q = new Question("123")
            {
                imageUrl = "",
                hint = "hint test",
                absoluteChoice = new Choice("Absolute")
            };

            ChoiceResult r = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 15" },
                nextIds = new List<int> { 8 }
            };

            q.absoluteChoice.AfterChoiceDo(r);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 1));
        }
        // ============================================
        {
            int id = 8;
            string imageUrl = "3.png";
            string content = "";

            Question q = new Question("大病一場痊癒後，做點什麼？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("好，多出去走走"),
                rightChoice = new Choice("不了，很花錢的")
            };

            ChoiceResult r_1 = new ChoiceResult("培養運動習慣")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { 9 }
            };

            ChoiceResult r_2 = new ChoiceResult("行動不便，待在家")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 4));
        }
        // ============================================
        {
            int id = 9;
            string imageUrl = "777.png";
            string content = "85歲，是個含飴弄孫的年紀呢！等等，老伴！老伴？老伴...";

            Question q = new Question("123")
            {
                imageUrl = "",
                hint = "hint test",
                absoluteChoice = new Choice("Absolute")
            };

            ChoiceResult r = new ChoiceResult("結果")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 15" },
                nextIds = new List<int> { 10 }
            };

            q.absoluteChoice.AfterChoiceDo(r);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 1));
        }
        // ============================================
        {
            int id = 8;
            string imageUrl = "3.png";
            string content = "";

            Question q = new Question("走出傷痛吧，做點什麼？")
            {
                imageUrl = "",
                hint = "",
                leftChoice = new Choice("好，多出去走走"),
                rightChoice = new Choice("不了，很花錢的")
            };

            ChoiceResult r_1 = new ChoiceResult("活到老學到老")
            {
                imageUrl = "1_5.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "S - 2 5" },
                nextIds = new List<int> { }
            };

            ChoiceResult r_2 = new ChoiceResult("花點時間陪家人")
            {
                imageUrl = "1_6.png",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 3 8", "H - 1 3" },
                nextIds = new List<int> { }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 4));
        }
    }
}
