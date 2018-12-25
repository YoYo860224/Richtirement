using System;
using System.Collections.Generic;
using UnityEngine;

public static class StoryManager
{
    public static List<StoryEvent> AllSEventList = new List<StoryEvent>();  // 所有的事件從這裡來選
    public static List<int> futureEventsID = new List<int>();               // 有可能發生的事件 LIST

    public static StoryEvent nowEvent;                                      // 當前事件
    public static Question nowQuestion;
    public static Choice nowChoice;                                         // 當前選擇

    public static List<EventLog> log = new List<EventLog>();                // 事件紀錄
    
    // 增加story
    public static void AddStory(StoryEvent story)   
    {
        AllSEventList.Add(story);
    }

    // 產生下一個事件
    public static bool NextEvent()
    {
        if (futureEventsID.Count > 0)
        {

            int randomIndex = RandomUtil.random.Next(0, futureEventsID.Count);
            nowEvent = AllSEventList.Find(x => x.id == futureEventsID[randomIndex]).Copy();
            return true;
        }
        return false;
    }

    // 選擇完根據 log 刪除現在的 story, 增加 nextId 進 list
    public static void EndNowStory(List<int> nextIds)   
    {
        int index = futureEventsID.IndexOf(nowEvent.id);
        futureEventsID.RemoveAt(index);

        foreach (var nextId in nextIds)
        {
            futureEventsID.Add(nextId);
        }
    }
}

public class StoryEvent
{
    public int id;
    public string content;                       // 內容
    public string imageUrl;                      // 圖的url
    public Question question;                    // 問題
    public int year;

    public StoryEvent(int id, string imageUrl, string content, Question q, int year)
    {
        this.id = id;
        this.imageUrl = imageUrl;
        this.content = content;
        this.question = q;
        this.year = year;
    }

    public StoryEvent Copy()
    {
        return new StoryEvent(id, imageUrl,content, question.Copy(), year);
    }
}

public class Question
{
    public string content;
    public string imageUrl;
    public string hint;
    public Choice leftChoice;
    public Choice rightChoice;
    public Choice absoluteChoice;

    public Question()
    {
        content = "";
    }

    public Question(string c)
    {
        content = c;
    }

    public Question(string content, string imageUrl, string hint, Choice leftChoice, Choice rightChoice, Choice absoluteChoice)
    {
        this.content = content;
        this.imageUrl = imageUrl;
        this.hint = hint;
        this.leftChoice = leftChoice;
        this.rightChoice = rightChoice;
        this.absoluteChoice = absoluteChoice;
    }

    public Question Copy()
    {
        if(absoluteChoice != null)
        {
            return new Question(content, imageUrl, hint, leftChoice, rightChoice, absoluteChoice.Copy());
        }
        return new Question(content, imageUrl, hint, leftChoice.Copy(), rightChoice.Copy(), absoluteChoice);
    }
}

public class Choice
{
    public string content;                      // Text

    public Question nextQuestion;               // 如果有 nextQuestion，就沒 choiceResult
    public List<ChoiceResult> choiceResults;    // 如果有 choiceResults 要去選一種結果

    public Choice()
    {
        this.content = "";
    }

    public Choice(string content)
    {
        this.content = content;
        this.nextQuestion = null;
        this.choiceResults = new List<ChoiceResult>();
    }

    public Choice(string content, Question nextQuestion, List<ChoiceResult> choiceResults)
    {
        this.content = content;
        this.nextQuestion = nextQuestion;
        this.choiceResults = choiceResults;
    }

    public void AfterChoiceDo(ChoiceResult cr)
    {
        choiceResults.Add(cr);
    }

    public void AfterChoiceDo(Question q)
    {
        this.nextQuestion = q;
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
            StoryManager.EndNowStory(choiceResults[tempList[index]].nextIds);
            return tempList[index];
        }
        else
        {
            // 改做 nextQuestion
            return -1;
        }
    }
    public Choice Copy()
    {
        if (nextQuestion != null)
        {
            return new Choice(content, nextQuestion.Copy(), choiceResults);
        }
        return new Choice(content, nextQuestion, choiceResults);
    }
}

public class ChoiceResult
{
    public string content;                  // 結果對話
    public string imageUrl;                 // 圖的url
    public float prob;                      // 發生機率
    public List<string> valueChanges;       // 此結果造成的數值改變
    public List<int> nextIds;               // 追加的 Event 是什麼  ##感覺要用 List 可能有多個##

    public ChoiceResult()
    {
        content = "";
    }

    public ChoiceResult(string c)
    {
        content = c;
    }
}

public class EventLog
{
    public int id;
    public List<bool> choice;

    public EventLog(int id, List<bool> choice)
    {
        this.id = id;
        this.choice = choice;
    }
}
