using System;
using System.Collections.Generic;
using UnityEngine;

public static class StoryManager
{
    public static System.Random crandom = new System.Random();

    public static List<int> willHappenEventId = new List<int>();  // 有可能發生的事件LIST
    public static int nowId;            // 現在發生的事件
    public static List<EventLog> log = new List<EventLog>();       // 事件紀錄
    public static List<StoryContent> storyList = new List<StoryContent>(); // 事件List


    public static void AddStory(StoryContent story)   // 增加story
    {
        storyList.Add(story);
    }

    // 產生下一個事件
    public static int NextEvent()
    {       
        int randomId = crandom.Next(0, willHappenEventId.Count);
        nowId = willHappenEventId[randomId];

        return nowId;
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
    public List<string> content;                 // 用get分辨語言
    public string hintText;                      // hint的Text
    public Choice trueChoice;                      // True的Text
    public Choice falseChoice;                     // False的Text

    public StoryContent(int id,string imageUrl, string questionText, List<string> content, string hintText, Choice trueChoice, Choice falseChoice)
    {
        this.id = id;
        this.imageUrl = imageUrl;
        this.questionText = questionText;
        this.content = content;
        this.hintText = hintText;
        this.trueChoice = trueChoice;
        this.falseChoice = falseChoice;
    }
}

public class Choice
{
    public string text;                          // Text
    public List<int> nextId;                     // 用list存接下來會連接到哪些id, 如果是-1代表事件結束
    public List<string> imageUrl;                // 圖的url
    public List<float> nextProb;                 // 各個事件的機率
    public List<List<string>> nextResult;        // 各個事件的結果對話
    public List<List<string>> nextChangeValue;   // 各個事件會造成的

    public Choice(string text)
    {
        this.text = text;
        this.nextId = new List<int>();
        this.imageUrl = new List<string>();
        this.nextProb = new List<float>();
        this.nextResult = new List<List<string>>();
        this.nextChangeValue = new List<List<string>>();
    }

    public void AddEventAtChoice(int id,string imageUrl, float nextProb, List<string> result, List<string> changeValue)
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
            
            //Debug.Log(times);
            for(int j = 0; j < times; j++)
            {
                tempList.Add(i);
            }
        }

        //Debug.Log(tempList.Count);

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