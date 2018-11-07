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
        public static string name;
        public static int kidAmount;
        public static float currentAssets = 5730;

        public static float deposit = 1200;
        public static float stock = 1450;
        public static float foreignCurrency = 400;
        public static float estateAndRent = 50;
        public static float dividend = 600;
        public static float annuity = 30;
        public static float criticalIllnessInsurance = 500;
        public static float longTermCareInsurance = 1500;

        public static void CalcTotalAssets()
        {
            currentAssets = deposit + stock + foreignCurrency + estateAndRent + dividend + annuity + criticalIllnessInsurance + longTermCareInsurance;
        }

        public static float Money
        {
            get
            {
                return currentAssets;
            }
        }
        public static float Mental = 70.0f;
        public static float PhysicalHearth = 70.0f;
        public static float SocialHearth = 70.0f;
        /*
         * P心理
         * S社交
         * H健康
         * $(萬)
        */
    }
}

