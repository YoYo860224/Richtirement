using System;
using System.Collections.Generic;
using UnityEngine;

public static class StoryManager
{
    public static int nowId;                                                // 當前事件 ID
    public static int nextId;                                               // 下一個事件 ID
    public static List<int> futureEvents = new List<int>();                 // 有可能發生的事件LIST
    public static StoryEvent nowEvent;                                      // 當前事件
    public static Choice nowChoice;                                         // 當前選擇
    public static List<EventLog> log = new List<EventLog>();                // 事件紀錄
    public static List<StoryEvent> storyList = new List<StoryEvent>();      // 事件List

    // 增加story
    public static void AddStory(StoryEvent story)   
    {
        storyList.Add(story);
    }

    // 產生下一個事件
    public static void NextEvent()
    {       
        int randomIndex = RandomUtil.random.Next(0, futureEvents.Count);
        nowId = futureEvents[randomIndex];
        nowEvent = storyList.Find(x => x.id == nowId);
    }

    // 選擇完根據 log 刪除現在的 story, 增加 nextId 進 list
    public static void EndNowStory(EventLog log)   
    {
        int index = futureEvents.IndexOf(log.id);
        futureEvents.RemoveAt(index);
        if (log.nextId != -1)   // -1 代表後面沒有事件了
        {
            futureEvents.Add(log.nextId);
        }
    }

    public static void Clear()
    {
        nowId = 0;
        futureEvents.Clear();
        log.Clear();
        storyList.Clear();
    }
}

public class StoryEvent
{
    public int id;
    public string imageUrl;                      // 圖的url
    public string content;                       // 內容
    public Question question;                    // 問題

    public StoryEvent(int id, string imageUrl, string content, Question q)
    {
        this.id = id;
        this.imageUrl = imageUrl;
        this.content = content;

        this.question = new Question();
        this.question.content = q.content;
        this.question.hint = q.hint;
        this.question.leftChoice = q.leftChoice;
        this.question.rightChoice = q.rightChoice;
    }
}

public class Question
{
    public string imageUrl;
    public string content;
    public string hint;
    public Choice leftChoice;
    public Choice rightChoice;

    public Question()
    {
        content = "";
    }

    public Question(string content, string imageUrl, string hint, Choice leftChoice, Choice rightChoice)
    {
        this.content = content;
        this.imageUrl = imageUrl;
        this.hint = hint;
        this.leftChoice = leftChoice;
        this.rightChoice = rightChoice;
    }
}

public class Choice
{
    public string content;                      // Text
    public Question nowQuestion;                // nowQuestion

    public Question nextQuestion;               // 如果有 nextQuestion，就沒 choiceResult
    public List<ChoiceResult> choiceResults;    // 如果有 choiceResults 要去選一種結果

    public Choice(string content)
    {
        this.content = content;
        this.nextQuestion = null;
        this.choiceResults = new List<ChoiceResult>();
    }

    public void AfterChoiceDo(ChoiceResult cr)
    {
        var choiceResult = new ChoiceResult();
        choiceResult.nextId = cr.nextId;
        choiceResult.imageUrl = cr.imageUrl;
        choiceResult.prob = cr.prob;
        choiceResult.content = cr.content;
        choiceResult.valueChanges = cr.valueChanges;

        choiceResults.Add(choiceResult);
    }

    public void AfterChoiceDo(Question q)
    {
        this.nextQuestion = new Question();
        this.nextQuestion.imageUrl = q.imageUrl;
        this.nextQuestion.content = q.content;
        this.nextQuestion.hint = q.hint;
        this.nextQuestion.leftChoice = q.leftChoice;
        this.nextQuestion.rightChoice = q.rightChoice;
    }

    public int NextEvent()
    {
        if (nextQuestion == null)   // 執行結果
        {
            // 抽獎選事件
            List<int> tempList = new List<int>();
            for (int i = 0; i < choiceResults.Count; i++)
            {
                int times = (int)(choiceResults[i].prob * 99.9f + 1);
                for (int j = 0; j < times; j++)
                {
                    tempList.Add(i);
                }
            }
            int index = RandomUtil.random.Next(0, tempList.Count);
            return choiceResults[tempList[index]].nextId;
        }
        else
        {
            // 改做 nextQuestion
            return -1;
        }
        
    }
}

public class ChoiceResult
{
    public string content;                  // 結果對話
    public string imageUrl;                 // 圖的url
    public float prob;                      // 發生機率
    public List<string> valueChanges;       // 此結果造成的數值改變
    public int nextId;                      // 追加的 Event 是什麼  ##感覺要用 List 可能有多個##
}

public class EventLog
{
    public int id;
    public bool choice;
    public int nextId;

    public EventLog(int id, bool choice, int nextId)
    {
        this.id = id;
        this.choice = choice;
        this.nextId = nextId;
    }
}
