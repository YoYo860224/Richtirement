﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;
using System;

namespace Setting
{
    // 系統設定
    public class SystemSetting
    {
        public static Localize nowLanguage = Localize.en;

        public static int AnnuityMax = 50;              // 年金每年最多增加多少
        public static int MedicineInsuranceMax = 50;    // 醫療保險每年最多增加多少
    }

    public class CharacterSetting
    {
        public static void InitCharacter()
        {
            CharacterSetting.name = "";
            CharacterSetting.age = 55;
            CharacterSetting.hasSpouse = false;
            CharacterSetting.kidAmount = 0;
            CharacterSetting.Mental = 75;
            CharacterSetting.Hearth = 75;
            CharacterSetting.Social = 75;
            CharacterSetting.moneyHasChanged = 0;
            CharacterSetting.mentalHasChanged = 0;
            CharacterSetting.hearthHasChanged = 0;
            CharacterSetting.socialHasChanged = 0;
            CharacterSetting.deposit = 0;
            CharacterSetting.stock = 0;
            CharacterSetting.fund = 0;
            CharacterSetting.annuity = 0;
            CharacterSetting.medicineInsurance = 0;
        }

        // 基本人設
        public static string name;
        public static int age = 55;
        public static bool hasSpouse;
        public static int kidAmount;

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
        public static int deposit = 1500;
        public static int stock = 0;
        public static int fund = 0;
        public static int annuity = 0;
        public static int medicineInsurance = 0;

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

