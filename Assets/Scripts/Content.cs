using System.Collections;
using System.Collections.Generic;
using System;
using Setting;

namespace ExtensionMethods
{
    public static class IntExtensions
    {
        public static string LocalMoneyString(this int man)
        {
            switch (Setting.SystemSetting.nowLanguage)
            {
                case Localize.en:
                    double million = man / 100.0;
                    return string.Format("{0:N}", million);
                case Localize.zh_tw:
                    return man.ToString();
            }
            return "Not Found.";
        }
    }
}
