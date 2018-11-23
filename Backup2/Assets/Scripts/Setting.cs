using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;
using System;

namespace Setting
{
    // 系統設定
    public class SystemSetting
    {
        public static Localize nowLanguage = Localize.zh_tw;

        public static int AnnuityMax = 50;              // 年金每年最多增加多少
        public static int MedicineInsuranceMax = 50;    // 醫療保險每年最多增加多少
    }

    public class CharacterSetting
    {
        // 基本人設
        public static string name;
        public static int age = 55;
        public static int kidAmount;
        public static bool spouse;

        // 四大基本數值
        public static int Money
        {
            get
            {
                return deposit
                    + stock
                    + fund;
            }
            set
            {
                deposit = value;
            }
        }
        public static int Mental = 75;
        public static int Hearth = 75;
        public static int Social = 75;

        // Money 細項
        public static int deposit = 1200;
        public static int stock = 0;
        public static int fund = 0;
        public static int annuity = 0;
        public static int medicineInsurance = 0;

        /*
         * P心理
         * S社交
         * H健康
         * $(萬)
         * example: "P + 50", "P - 3 5"
         */
        public static string AttributeChanged(List<string> changed)
        {
            string mentalResult = "";
            string physiologicResult = "";
            string SocialResult = "";

            for(int i = 0; i < changed.Count; i++)
            {
                Debug.Log(changed[i]);

                // 解析字串
                // words[0]: Attribute
                // words[1]: Sign
                // words[2]: Value
                // words[3]: Value if random
                string[] words = changed[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // 得到 value
                int value = 0;
                if(words.Length == 3)
                {
                    // example: "P + 50"
                    value = int.Parse(words[2]); 
                }
                else if(words.Length == 4)
                {
                    // example: "P - 3 5"
                    value = RandomUtil.random.Next(int.Parse(words[2]), int.Parse(words[3]));
                }
                // 判斷+-
                if(words[1] == "+")
                {
                    // 判斷 Attribute
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

    public enum Localize
    {
        en,
        zh_tw
    }
}

