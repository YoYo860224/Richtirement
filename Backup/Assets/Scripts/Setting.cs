using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

namespace Setting
{
    public enum Localize
    {
        en,
        zh_tw
    }

    public class SystemSetting
    {

        public static Localize nowLanguage = Localize.en;
        public static List<int> story = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    public class CharacterSetting
    {
        public static System.Random crandom = new System.Random();

        public static string name;
        public static int kidAmount;
        public static int currentAssets = 5730;

        public static int deposit = 1200;
        public static int stock = 1450;
        public static int foreignCurrency = 400;
        public static int estateAndRent = 50;
        public static int dividend = 600;
        public static int annuity = 30;
        public static int criticalIllnessInsurance = 500;
        public static int longTermCareInsurance = 1500;

        public static void CalcTotalAssets()
        {
            currentAssets = deposit + stock + foreignCurrency + estateAndRent + dividend + annuity + criticalIllnessInsurance + longTermCareInsurance;
        }

        public static int Money
        {
            get
            {
                return currentAssets;
            }
        }
        public static int Mental = 50;
        public static int Hearth = 50;
        public static int Social = 50;
        /*
         * P心理
         * S社交
         * H健康
         * $(萬)
        */
        public static string AttributeChanged(List<string> changed)
        {
            string mentalResult = "";
            string physiologicResult = "";
            string SocialResult = "";
            for(int i = 0; i < changed.Count; i++)
            {
                Debug.Log(changed[i]);
                string[] words = changed[i].Split(' ');
                int value = 0;
                // 判斷value
                if(words.Length == 4)
                {
                    value = crandom.Next(int.Parse(words[2]), int.Parse(words[3]));
                }
                else if(words.Length == 3)
                {
                    value = int.Parse(words[2]);
                }
                // 判斷+-
                if(words[1] == "+")
                {
                    // 判斷attribute
                    switch (words[0])
                    {
                        case "P":
                            mentalResult = "Mental Index + " + value.ToString() + "\n";
                            Mental += value;
                            break;
                        case "S":
                            SocialResult = "Social Index + " + value.ToString() + "\n";
                            Social += value;
                            break;
                        case "H":
                            physiologicResult = "Physiologic Index + " + value.ToString() + "\n";
                            Hearth += value;
                            break;
                    }
                }
                else if (words[1] == "-")
                {
                    switch (words[0])
                    {
                        case "P":
                            mentalResult = "Mental Index - " + value.ToString() + "\n";
                            Mental -= value;
                            break;
                        case "S":
                            SocialResult = "Social Index - " + value.ToString() + "\n";
                            Social -= value;
                            break;
                        case "H":
                            physiologicResult = "Physiologic Index - " + value.ToString() + "\n";
                            Hearth -= value;
                            break;
                    }
                }

            }

            return mentalResult + physiologicResult + SocialResult;
        }
    }
}

