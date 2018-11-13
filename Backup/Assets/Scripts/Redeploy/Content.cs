using System.Collections;
using System.Collections.Generic;
using System;
using Setting;

namespace Content
{

    public class Redeploy
    {
        public static string Title
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Asset allocation";
                    case Localize.zh_tw:
                        return "資產分配";
                }
                return "Not Found.";
            }
        }

        public static string redeploy
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "redeploy";
                    case Localize.zh_tw:
                        return "配置";
                }
                return "Not Found.";
            }
        }

        public static string EnterHint
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Enter asset...";
                    case Localize.zh_tw:
                        return "輸入金額";
                }
                return "Not Found.";
            }
        }


        public static string deposit
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "deposit";
                    case Localize.zh_tw:
                        return "存款";
                }
                return "Not Found.";
            }
        }

        public static string stock
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "stock";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string foreignCurrency
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "foreign currency";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string estateAndRent
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "estate/rent";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string dividend
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "dividend";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string annuity
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "annuity";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string criticalIllnessInsurance
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "critical illness insurance";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string longTermCareInsurance
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "long-term care insurance";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string TotalAssets1
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Total of asset : ";
                    case Localize.zh_tw:
                        return "";
                }
                return "Not Found.";
            }
        }

        public static string TotalAssets2
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return " million";
                    case Localize.zh_tw:
                        return " 萬";
                }
                return "Not Found.";
            }
        }
    }

}
