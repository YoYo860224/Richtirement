using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCreate : MonoBehaviour {

    private void Awake()
    {
        {
            int id = 0;
            string imageUrl = "Story/0";                                          // 圖的url
            string content = "轉眼間也到了再十年就要退休的年紀呢！";                  // 開頭簡介

            Question q0 = new Question("工作好一段時間了，有了一些存款，做點理財嗎？")
            {
                imageUrl = "Story/0",
                hint = "hint test",
                leftChoice = new Choice("好啊"),
                rightChoice = new Choice("不了")
            };

            Question q0_1 = new Question("想要做什麼理財呢？")
            {
                imageUrl = "Story/0",
                hint = "",
                leftChoice = new Choice("買股票"),
                rightChoice = new Choice("買儲蓄險")
            };

            ChoiceResult r0_1 = new ChoiceResult("恭喜！跟上這波漲潮，存款增加，龍心大悅")
            {
                imageUrl = "Story/0_2",
                prob = 0.3f,
                valueChanges = new List<string> { "P + 15 18", "$ + 30 80"},
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_2 = new ChoiceResult("啊！真不巧，遭遇股市大跌！財富損失，心情受影響")
            {
                imageUrl = "Story/0_1",
                prob = 0.7f,
                valueChanges = new List<string> { "P - 15 18", "$ - 30 80" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_3 = new ChoiceResult("成功與保險人購買保險")
            {
                imageUrl = "Story/0_3",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 11 15" },
                nextIds = new List<int> { 1 }
            };

            ChoiceResult r0_4 = new ChoiceResult("將部分存款放入定存")
            {
                imageUrl = "Story/0_4",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 12 15" },
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
            string imageUrl = "Story/1";
            string content = "60歲了，小孩也都長大了，再五年就可以享受退休生活了，再努力一下吧！";

            Question q1 = new Question("老闆請你平日加班假日上班，你會如何選擇？")
            {
                imageUrl = "Story/1",
                hint = "",
                leftChoice = new Choice("加班加到底"),
                rightChoice = new Choice("花時間陪陪家人")
            };

            Question q1_1 = new Question("這個時候，老闆叫你代替他去應酬，你會如何選擇？")
            {
                imageUrl = "Story/1",
                hint = "",
                leftChoice = new Choice("去"),
                rightChoice = new Choice("不去，回家")
            };

            Question q1_2 = new Question("想與家人一起做些什麼呢？")
            {
                imageUrl = "Story/1",
                hint = "",
                leftChoice = new Choice("踏青"),
                rightChoice = new Choice("看電影")
            };

            ChoiceResult r1_1 = new ChoiceResult("經常與廠商應酬傷害身心理健康")
            {
                imageUrl = "Story/1_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 15 20", "H - 10 15", "S + 8 12" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_2 = new ChoiceResult("在每天繁忙的生活中，偶爾偷閒，可能損失社交力，但減少更多身心理的損失")
            {
                imageUrl = "Story/1_2",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 8 13", "H - 8 13", "S - 15 18" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_3 = new ChoiceResult("偶爾和家人去郊外走走，增進感情也更健康！")
            {
                imageUrl = "Story/1_3",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 12 15", "H + 12 15", "S + 12 15" },
                nextIds = new List<int> { 2 }
            };

            ChoiceResult r1_4 = new ChoiceResult("偶爾與家人出去放鬆、看看電影，增進感情，但也要注意健康喔～")
            {
                imageUrl = "Story/1_4",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 12 15", "H - 10 15", "S + 12 15" },
                nextIds = new List<int> { 2 }
            };

            q1.leftChoice.AfterChoiceDo(q1_1);
            q1.rightChoice.AfterChoiceDo(q1_2);
            q1_1.leftChoice.AfterChoiceDo(r1_1);
            q1_1.rightChoice.AfterChoiceDo(r1_2);
            q1_2.leftChoice.AfterChoiceDo(r1_3);
            q1_2.rightChoice.AfterChoiceDo(r1_4);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q1, 4));
        }
        // ============================================
        {
            int id = 2;
            string imageUrl = "Story/company";
            string content = "";

            Question q2 = new Question("公司突然倒閉，未來妳決定該何去何從呢？")
            {
                imageUrl = "Story/company",
                hint = "",
                leftChoice = new Choice("提前退休"),
                rightChoice = new Choice("找新工作")
            };

            ChoiceResult r2_1 = new ChoiceResult("迎接退休生活，要好好規劃呢！")
            {
                imageUrl = "Story/1_5",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 13 18", "S - 12 15" },
                nextIds = new List<int> { 3 }
            };

            ChoiceResult r2_2 = new ChoiceResult("進入新公司，準備迎接全新的挑戰吧！")
            {
                imageUrl = "Story/1_6",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 13 18", "H - 10 13" },
                nextIds = new List<int> { 3 }
            };

            q2.leftChoice.AfterChoiceDo(r2_1);
            q2.rightChoice.AfterChoiceDo(r2_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q2, 1));
        }
        // ============================================
        {
            int id = 3;
            string imageUrl = "Story/2";
            string content = "恭喜你退休了！準備好過想過的人生嗎：）";

            Question q = new Question("這時小孩突然告知要創業，想跟你借500萬，借給他嗎？")
            {
                imageUrl = "Story/2",
                hint = "",
                leftChoice = new Choice("借"),
                rightChoice = new Choice("不借")
            };

            Question q_1 = new Question("小孩生意失敗，堅持要他還錢嗎？")
            {
                imageUrl = "Story/2",
                hint = "",
                leftChoice = new Choice("要"),
                rightChoice = new Choice("不要")
            };

            ChoiceResult r_1 = new ChoiceResult("和小孩大吵一架，心理指數大減。")
            {
                imageUrl = "Story/2_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 15 20", "$ - 100" },
                nextIds = new List<int> { 4 }
            };

            ChoiceResult r_2 = new ChoiceResult("孩子非常感謝你的幫助，感情加深")
            {
                imageUrl = "Story/2_2",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 10 13", "S + 10", "$ - 200" },
                nextIds = new List<int> { 4 }
            };

            ChoiceResult r_3 = new ChoiceResult("和小孩大吵一架，心理指數大減。")
            {
                imageUrl = "Story/2_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 12 15", "S - 5 8" },
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
            string imageUrl = "Story/livingroom";
            string content = "退休也五年了，時間真的不等人呢．．．";

            Question q = new Question("退休後有許多空閒的時間，要不要培養個興趣呢？")
            {
                imageUrl = "Story/livingroom",
                hint = "",
                leftChoice = new Choice("好，多出去走走"),
                rightChoice = new Choice("不了，很花錢的")
            };

            ChoiceResult r_1 = new ChoiceResult("偶爾出遊看看世界、認識新朋友，心情也跟著變好了呢！")
            {
                imageUrl = "Story/3_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 13 15", "H + 13 15", "S + 10 12" },
                nextIds = new List<int> { 5 }
            };

            ChoiceResult r_2 = new ChoiceResult("選擇待在家中，身心健康與社交能力大量下降。")
            {
                imageUrl = "Story/3_2",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 15 18", "S - 15", "H - 15 18" },
                nextIds = new List<int> { 5 }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 4));
        }
        // ============================================
        {
            int id = 5;
            string imageUrl = "Story/livingroom";
            string content = "";

            Question q = new Question("還是跟一些老朋友聚聚聊聊嗎？")
            {
                imageUrl = "Story/livingroom",
                hint = "",
                leftChoice = new Choice("好！"),
                rightChoice = new Choice("不了。")
            };

            ChoiceResult r_1 = new ChoiceResult("與老朋友聊聊近況與往事，心情大好")
            {
                imageUrl = "Story/3_3",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 10 15", "S + 10 15" },
                nextIds = new List<int> { 6 }
            };

            ChoiceResult r_2 = new ChoiceResult("一直待在家中，要注意健康...")
            {
                imageUrl = "Story/3_4",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 10 15", "S - 15 18", "H - 10 15" },
                nextIds = new List<int> { 6 }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 1));
        }
        // ============================================
        {
            int id = 6;
            string imageUrl = "Story/4";                                             
            string content = "恭喜你退休了！準備好過想過的人生嗎：）";               

            Question q0 = new Question("朋友找你投資150萬創業，你會？")
            {
                imageUrl = "Story/4",
                hint = "hint test",
                leftChoice = new Choice("投資"),
                rightChoice = new Choice("不投資")
            };

            Question q0_1 = new Question("開了間餐廳也裝潢好了；面對生意的經營，你會怎麼做呢？")
            {
                imageUrl = "Story/4",
                hint = "",
                leftChoice = new Choice("隨時到場監督"),
                rightChoice = new Choice("每月查看報表")
            };

            ChoiceResult r0_1 = new ChoiceResult("餐廳生意興隆，存款跟心情都大增！")
            {
                imageUrl = "Story/4_1",
                prob = 0.5f,
                valueChanges = new List<string> {"$ + 50", "P + 13 15", "H - 15 18", "S + 15 18" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_2 = new ChoiceResult("餐廳營運因缺乏管理不如預期。")
            {
                imageUrl = "Story/4_2",
                prob = 0.5f,
                valueChanges = new List<string> {"$ - 150", "P - 15 18", "H - 12 15", "S + 10 15" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_3 = new ChoiceResult("餐廳營運因缺乏管理不如預期。")
            {
                imageUrl = "Story/4_2",
                prob = 1.0f,
                valueChanges = new List<string> {"$ - 150", "P - 10 15", "H - 12", "S + 12" },
                nextIds = new List<int> { 7 }
            };

            ChoiceResult r0_4 = new ChoiceResult("還是覺得多放點錢在定存比較保險呢，但與朋友的友情指數下降")
            {
                imageUrl = "Story/4_3",
                prob = 1.0f,
                valueChanges = new List<string> { "P - 8 12", "S - 10 13" },
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
            string imageUrl = "Story/livingroom";
            string content = "原本以為可以健健康康過完這一生，竟然在80歲這年...";

            Question q = new Question("123")
            {
                imageUrl = "",
                hint = "hint test",
                absoluteChoice = new Choice("Absolute")
            };

            ChoiceResult r = new ChoiceResult("生大病住院治療，花費驚人")
            {
                imageUrl = "Story/5",
                prob = 1.0f,
                valueChanges = new List<string> {"$ - 50 80", "P - 13 18", "H - 25" },
                nextIds = new List<int> { 8 }
            };

            q.absoluteChoice.AfterChoiceDo(r);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 1));
        }
        // ============================================
        {
            int id = 8;
            string imageUrl = "Story/livingroom";
            string content = "";

            Question q = new Question("好不容易大病一場痊癒後，對於未來有什麼規劃？")
            {
                imageUrl = "Story/livingroom",
                hint = "",
                leftChoice = new Choice("培養運動習慣"),
                rightChoice = new Choice("行動不便，待在家")
            };

            ChoiceResult r_1 = new ChoiceResult("多多運動，出去走走，恢復元氣也認識新朋友")
            {
                imageUrl = "Story/5_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 13 15" , "H + 15", "S + 10 12" },
                nextIds = new List<int> { 9 }
            };

            ChoiceResult r_2 = new ChoiceResult("行動不便，待在家")
            {
                imageUrl = "",
                prob = 1.0f,
                valueChanges = new List<string> { },
                nextIds = new List<int> { }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 4));
        }
        // ============================================
        {
            int id = 9;
            string imageUrl = "Story/livingroom";
            string content = "85歲，是個含飴弄孫的年紀呢！等等，老伴！老伴？老伴...";

            Question q = new Question("123")
            {
                imageUrl = "Story/livingroom",
                hint = "hint test",
                absoluteChoice = new Choice("Absolute")
            };

            ChoiceResult r = new ChoiceResult("老伴提前走了，心情大受影響，儀式也花了不少錢...")
            {
                imageUrl = "Story/6",
                prob = 1.0f,
                valueChanges = new List<string> {"$ - 80", "P - 20", "H - 10 12" },
                nextIds = new List<int> { 10 }
            };

            q.absoluteChoice.AfterChoiceDo(r);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 1));
        }
        // ============================================
        {
            int id = 10;
            string imageUrl = "Story/livingroom";
            string content = "";

            Question q = new Question("老伴走了，但一直難過下去也不是辦法呢...走出傷痛吧，做點什麼呢？")
            {
                imageUrl = "Story/livingroom",
                hint = "",
                leftChoice = new Choice("活到老學到老"),
                rightChoice = new Choice("花點時間陪家人")
            };

            ChoiceResult r_1 = new ChoiceResult("培養閱讀習慣，陶冶心靈，耶參加讀書會，認識新朋友")
            {
                imageUrl = "Story/6_1",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 13 18", "H + 10", "S + 10 15" },
                nextIds = new List<int> { }
            };

            ChoiceResult r_2 = new ChoiceResult("含飴弄孫，與孩子有也保有良好關係")
            {
                imageUrl = "Story/6_2",
                prob = 1.0f,
                valueChanges = new List<string> { "P + 13 15", "H + 12 15", "S + 8 12" },
                nextIds = new List<int> { }
            };

            q.leftChoice.AfterChoiceDo(r_1);
            q.rightChoice.AfterChoiceDo(r_2);

            StoryManager.AddStory(new StoryEvent(id, imageUrl, content, q, 4));
        }
    }
}
