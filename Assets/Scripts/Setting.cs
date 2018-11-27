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
        public static bool hasSpouse;
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

        // add 1
        // no changed 0
        // minus -1
        public static int moneyHasChanged = 0;
        public static int mentalHasChanged = 0;
        public static int hearthHasChanged = 0;
        public static int socialHasChanged = 0;


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
            moneyHasChanged = 0;
            mentalHasChanged = 0;
            hearthHasChanged = 0;
            socialHasChanged = 0;

            string mentalResult = "";
            string hearthResult = "";
            string socialResult = "";

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
                            mentalHasChanged = 1;
                            mentalResult = "Mental Index + " + value.ToString() + "\n";
                            Mental += value;
                            break;
                        case "S":
                            socialHasChanged = 1;
                            socialResult = "Social Index + " + value.ToString() + "\n";
                            Social += value;
                            break;
                        case "H":
                            hearthHasChanged = 1;
                            hearthResult = "Physiologic Index + " + value.ToString() + "\n";
                            Hearth += value;
                            break;
                    }
                }
                else if (words[1] == "-")
                {
                    switch (words[0])
                    {
                        case "P":
                            mentalHasChanged = -1;
                            mentalResult = "Mental Index - " + value.ToString() + "\n";
                            Mental -= value;
                            break;
                        case "S":
                            socialHasChanged = -1;
                            socialResult = "Social Index - " + value.ToString() + "\n";
                            Social -= value;
                            break;
                        case "H":
                            hearthHasChanged = -1;
                            hearthResult = "Physiologic Index - " + value.ToString() + "\n";
                            Hearth -= value;
                            break;
                    }
                }
            }

            return mentalResult + hearthResult + socialResult;
        }

        public static void nYearsLater(int n) {
            age += n;
            for (int i = 0; i < n; i++)
            {
                oneYearLater();
            }
        }

        public static void oneYearLater() {
            // 股票漲跌
            var stockRatio = RandomUtil.normal() * 0.3 + 1.0;
            if (stockRatio > 2)
                stockRatio = 2;
            if (stockRatio < 0)
                stockRatio = 0;
            stock = (int)(stock * stockRatio);

            // 基金漲跌
            var fundRatio = RandomUtil.normal() * 0.15 + 1.0;
            if (fundRatio > 2)
                fundRatio = 2;
            if (fundRatio < 0)
                fundRatio = 0;
            fund = (int)(fund * fundRatio);

            // 消費
            var depositRatio = RandomUtil.normal() * 0.003 + 0.96;
            if (depositRatio > 0.97)
                depositRatio = 0.97;
            if (depositRatio < 0.95)
                depositRatio = 0.95;
            deposit = (int)(deposit * depositRatio);
            // 基金配息
            deposit += (int)(fund * 0.05);
            // 年金
            annuity = (int)(annuity * 0.05);
        }
    }

    public enum Localize
    {
        en,
        zh_tw
    }
}

