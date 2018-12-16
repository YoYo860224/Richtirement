using System.Collections;
using System.Collections.Generic;
using System;
using Setting;

namespace Content
{
    public class InitSetting
    {
        //public static string Title
        //{
        //    get
        //    {
        //        switch (Setting.SystemSetting.nowLanguage)
        //        {
        //            case Localize.en:
        //                return "Enter your name and other setting";
        //            case Localize.zh_tw:
        //                return "請輸入名稱和其他設定";
        //        }
        //        return "Not Found.";
        //    }
        //}

        public static string Name
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Name";
                    case Localize.zh_tw:
                        return "姓名";
                }
                return "Not Found.";
            }
        }

        public static string NamePlaceHold
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Enter Name...";
                    case Localize.zh_tw:
                        return "請輸入姓名...";
                }
                return "Not Found.";
            }
        }

        public static string Kids
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Your kids";
                    case Localize.zh_tw:
                        return "你有幾個孩子";
                }
                return "Not Found.";
            }
        }

        public static string CurrentAssets
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "Current assets";
                    case Localize.zh_tw:
                        return "現有總資產";
                }
                return "Not Found.";
            }
        }

        public static string CurrentAssets_subTitle
        {
            get
            {
                switch (Setting.SystemSetting.nowLanguage)
                {
                    case Localize.en:
                        return "(unit/million)";
                    case Localize.zh_tw:
                        return "(萬)";
                }
                return "Not Found.";
            }
        }
    }

}
