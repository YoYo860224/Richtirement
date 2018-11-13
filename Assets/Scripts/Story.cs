using System;
using System.Collections.Generic;
using UnityEngine;

public static class StoryManager
{
    public static List<int> willHappenEventId = new List<int>();  // 有可能發生的事件LIST
    public static int nowId;            // 現在發生的事件
    public static int nextId;           // 下一個事件
    public static StoryContent nowStory;
    public static Choice nowChoice;
    public static List<EventLog> log = new List<EventLog>();       // 事件紀錄
    public static List<StoryContent> storyList = new List<StoryContent>(); // 事件List


    public static void AddStory(StoryContent story)   // 增加story
    {
        storyList.Add(story);
    }

    // 產生下一個事件
    public static void NextEvent()
    {       
        int randomIndex = RandomUtil.crandom.Next(0, willHappenEventId.Count);
        nowId = willHappenEventId[randomIndex];
        nowStory = storyList[FindStoryIndexById(nowId)];
    }

    // 選擇完根據log刪除現在的story,增加nextId進list
    public static void EndNowStory(EventLog log)   
    {
        int index = willHappenEventId.IndexOf(log.id);
        willHappenEventId.RemoveAt(index);
        if (log.nextId != -1)   // -1 代表後面沒有事件了
        {
            willHappenEventId.Add(log.nextId);
        }
    }

    public static int FindStoryIndexById(int id)
    {
        for (int i = 0; i < storyList.Count; i++)
        {
            if (storyList[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }

    public static void Clear()
    {
        willHappenEventId.Clear();
        nowId = 0;
        log.Clear();
        storyList.Clear();
    }
}

public class StoryContent
{
    public int id;
    public string imageUrl;                      // 圖的url
    public string questionText;                  // 問題
    public string content;                 // 用get分辨語言
    public string hintText;                      // hint的Text
    public Choice leftChoice;                      // True的Text
    public Choice rightChoice;                     // False的Text

    public StoryContent(int id,string imageUrl, string questionText, string content, string hintText, Choice leftChoice, Choice rightChoice)
    {
        this.id = id;
        this.imageUrl = imageUrl;
        this.questionText = questionText;
        this.content = content;
        this.hintText = hintText;
        this.leftChoice = leftChoice;
        this.rightChoice = rightChoice;
    }
}

public class Choice
{
    public string text;                          // Text
    public Question question;                    // nowQuestion
    public List<int> nextId;                     // 用list存接下來會連接到哪些id, 如果是-1代表事件結束
    public List<string> imageUrl;                // 圖的url
    public List<float> nextProb;                 // 各個事件的機率
    public List<string> nextResult;        // 各個事件的結果對話
    public List<List<string>> nextChangeValue;   // 各個事件會造成的

    public Choice(string text)
    {
        this.text = text;
        this.question = new Question();
        this.nextId = new List<int>();
        this.imageUrl = new List<string>();
        this.nextProb = new List<float>();
        this.nextResult = new List<string>();
        this.nextChangeValue = new List<List<string>>();
    }

    public void AddEventAtChoice(int id,string imageUrl, float nextProb, string result, List<string> changeValue)
    {
        this.nextId.Add(id);
        this.imageUrl.Add(imageUrl);
        this.nextProb.Add(nextProb);
        this.nextResult.Add(result);
        this.nextChangeValue.Add(changeValue);
    }

    public int NextEvent()
    {
        List<int> tempList = new List<int>();

        for(int i = 0; i < nextId.Count; i++)
        {
            int times = (int)(this.nextProb[i] * 99.9f + 1);
            for(int j = 0; j < times; j++)
            {
                tempList.Add(i);
            }
        }
        System.Random crandom = new System.Random();
        int index = crandom.Next(0, tempList.Count);
        return nextId[tempList[index]];
    }
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

public class Question
{
    public string content;
    public string imageUrl;
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