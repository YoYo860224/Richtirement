using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using Content;
using UnityEngine.SceneManagement;

public class RedeployManager : MonoBehaviour {
    public InputField deposity;
    public InputField stock;
    public InputField foreign;
    public InputField rent;
    public InputField dividend;
    public InputField annuity;
    public InputField illinessInsurance;
    public InputField longTermCareInsurance;

    public Text[] placeHolds;

    public Text title;
    public Text redeployText;
    public Button redeploy;
    public Text TotalAssets;


    string TotalAssetsString(string assets)
    {
        return Content.Redeploy.TotalAssets1 + assets + Content.Redeploy.TotalAssets2;
    }

    // Use this for initialization
    void Start () {
        foreach (Text temp in placeHolds)
        {
            temp.text = Content.Redeploy.EnterHint;
        }

        title.text = Content.Redeploy.Title;
        redeployText.text = Content.Redeploy.redeploy;
        
        deposity.text = Setting.CharacterSetting.deposit.ToString();
        deposity.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(0); });

        stock.text = Setting.CharacterSetting.stock.ToString();
        stock.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(1); });

        foreign.text = Setting.CharacterSetting.foreignCurrency.ToString();
        foreign.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(2); });

        rent.text = Setting.CharacterSetting.estateAndRent.ToString();
        rent.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(3); });

        dividend.text = Setting.CharacterSetting.dividend.ToString();
        dividend.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(4); });

        annuity.text = Setting.CharacterSetting.annuity.ToString();
        annuity.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(5); });

        illinessInsurance.text = Setting.CharacterSetting.criticalIllnessInsurance.ToString();
        illinessInsurance.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(6); });

        longTermCareInsurance.text = Setting.CharacterSetting.longTermCareInsurance.ToString();
        longTermCareInsurance.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(7); });


        TotalAssets.text = TotalAssetsString(Setting.CharacterSetting.currentAssets.ToString());


    }

    // Update is called once per frame
    void Update () {
        
    }

    public void RedeployButtom()
    {
        Debug.Log("click redeploy button");
        SceneManager.LoadScene("Story");

    }

    public void AssetsValueChangeCheck(int id)
    {
        bool conversionSuccessful = false;

        switch (id)
        {
            case 0:
                conversionSuccessful = int.TryParse(deposity.text, out Setting.CharacterSetting.deposit);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.deposit = 0;
                }
                break;
            case 1:
                conversionSuccessful = int.TryParse(stock.text, out Setting.CharacterSetting.stock);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.stock = 0;
                }
                break;
            case 2:
                conversionSuccessful = int.TryParse(foreign.text, out Setting.CharacterSetting.foreignCurrency);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.foreignCurrency = 0;
                }
                break;
            case 3:
                conversionSuccessful = int.TryParse(rent.text, out Setting.CharacterSetting.estateAndRent);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.estateAndRent = 0;
                }
                break;
            case 4:
                conversionSuccessful = int.TryParse(dividend.text, out Setting.CharacterSetting.dividend);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.dividend = 0;
                }
                break;
            case 5:
                conversionSuccessful = int.TryParse(annuity.text, out Setting.CharacterSetting.annuity);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.annuity = 0;
                }
                break;
            case 6:
                conversionSuccessful = int.TryParse(illinessInsurance.text, out Setting.CharacterSetting.criticalIllnessInsurance);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.criticalIllnessInsurance = 0;
                }
                break;
            case 7:
                conversionSuccessful = int.TryParse(longTermCareInsurance.text, out Setting.CharacterSetting.longTermCareInsurance);
                if (!conversionSuccessful)
                {
                    Setting.CharacterSetting.longTermCareInsurance = 0;
                }
                break;
        }

        Setting.CharacterSetting.CalcTotalAssets();
        TotalAssets.text = TotalAssetsString(Setting.CharacterSetting.currentAssets.ToString());
    }
}
