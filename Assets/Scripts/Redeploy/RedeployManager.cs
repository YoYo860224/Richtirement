using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using Content;
using UnityEngine.SceneManagement;

public class RedeployManager : MonoBehaviour {
    public RedeployField deposity;
    public RedeployField stock;
    public RedeployField fund;
    public RedeployField annuity;
    public RedeployField medicineInsurance;

    public Text title;
    public Text redeployText;
    public Button redeploy;
    public Text TotalAssets;

    private int totalAssets;
    private int tempDeposity;
    private int tempStock;
    private int tempFund;
    private int tempAnnuity;
    private int tempMedicineInsurance;


    string TotalAssetsString(string assets)
    {
        return Content.Redeploy.TotalAssets1 + assets + Content.Redeploy.TotalAssets2;
    }

    // Use this for initialization
    void Start () {
        totalAssets = Setting.CharacterSetting.Money;
        tempDeposity = Setting.CharacterSetting.deposit;
        tempStock = Setting.CharacterSetting.stock;
        tempFund = Setting.CharacterSetting.fund;
        tempAnnuity = 0;
        tempMedicineInsurance = 0;

        title.text = Content.Redeploy.Title;
        redeployText.text = Content.Redeploy.redeploy;
        

        
        deposity.moneyText.text = Setting.CharacterSetting.deposit.ToString() + "萬";
        deposity.slider.value = ((float)Setting.CharacterSetting.deposit / (float)Setting.CharacterSetting.Money * 100.0f);
        deposity.percentsText.text = deposity.slider.value.ToString("0.00") + "%";

        stock.moneyText.text = Setting.CharacterSetting.stock.ToString() + "萬";
        stock.slider.value = ((float)Setting.CharacterSetting.stock / (float)Setting.CharacterSetting.Money * 100.0f);
        stock.percentsText.text = stock.slider.value.ToString("0.00") + "%";
        stock.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(1); });

        fund.moneyText.text = Setting.CharacterSetting.fund.ToString() + "萬";
        fund.slider.value = ((float)Setting.CharacterSetting.fund / (float)Setting.CharacterSetting.Money * 100.0f);
        fund.percentsText.text = fund.slider.value.ToString("0.00") + "%";
        fund.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(2); });

        annuity.moneyText.text = "0萬";
        annuity.slider.value = 0;
        annuity.percentsText.text = "0%";
        annuity.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(3); });

        medicineInsurance.moneyText.text = "0萬";
        medicineInsurance.slider.value = 0;
        medicineInsurance.percentsText.text = "0%";
        medicineInsurance.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(4); });


        TotalAssets.text = TotalAssetsString(Setting.CharacterSetting.Money.ToString());


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
        switch (id)
        {
            case 1:
                if ((float)(stock.slider.value / 100.0f * (float)totalAssets) - tempStock <= tempDeposity)
                {
                    tempStock = (int)(stock.slider.value * (float)totalAssets / 100.0f);
                    stock.moneyText.text = tempStock.ToString() + "萬";
                }
                break;
            case 2:
                if ((float)(fund.slider.value / 100.0f * (float)totalAssets) - tempFund <= tempDeposity)
                {
                    tempFund = (int)(fund.slider.value * (float)totalAssets / 100.0f);
                    fund.moneyText.text = tempFund.ToString() + "萬";
                }
                break;
            case 3:
                if ((float)(annuity.slider.value / 100.0f * (float)Setting.SystemSetting.AnnuityMax) - tempAnnuity <= tempDeposity)
                {
                    tempAnnuity = (int)(annuity.slider.value / 100.0f * (float)Setting.SystemSetting.AnnuityMax);
                    annuity.moneyText.text = tempAnnuity.ToString() + "萬";
                }
                break;
            case 4:
                if ((float)(medicineInsurance.slider.value / 100.0f * (float)Setting.SystemSetting.MedicineInsuranceMax) - tempMedicineInsurance <= tempDeposity)
                {
                    tempMedicineInsurance = (int)(medicineInsurance.slider.value / 100.0f * (float)Setting.SystemSetting.MedicineInsuranceMax);
                    medicineInsurance.moneyText.text = tempMedicineInsurance.ToString() + "萬";
                }
                break;
        }

        stock.slider.value = (int)((float)tempStock / (float)totalAssets * 100.0f);
        stock.percentsText.text = stock.slider.value.ToString("0.00") + "%";

        fund.slider.value = (int)((float)tempFund / (float)totalAssets * 100.0f);
        fund.percentsText.text = fund.slider.value.ToString("0.00") + "%";

        annuity.slider.value = (int)((float)tempAnnuity / (float)Setting.SystemSetting.AnnuityMax * 100.0f);
        annuity.percentsText.text = annuity.slider.value.ToString("0.00") + "%";

        medicineInsurance.slider.value = (int)((float)tempMedicineInsurance / (float)Setting.SystemSetting.AnnuityMax * 100.0f);
        medicineInsurance.percentsText.text = medicineInsurance.slider.value.ToString("0.00") + "%";

        tempDeposity = totalAssets - tempFund - tempStock - tempMedicineInsurance - tempAnnuity;
        deposity.moneyText.text = tempDeposity.ToString() + "萬";
        deposity.slider.value = (int)((float)tempDeposity / (float)totalAssets * 100.0f);
        deposity.percentsText.text = deposity.slider.value.ToString("0.00") + "%";
        TotalAssets.text = TotalAssetsString(Setting.CharacterSetting.Money.ToString());
    }

    //public void AssetsValueChangeCheck(int id)
    //{
    //    bool conversionSuccessful = false;

    //    switch (id)
    //    {
    //        case 0:
    //            conversionSuccessful = int.TryParse(deposity.text, out Setting.CharacterSetting.deposit);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.deposit = 0;
    //            }
    //            break;
    //        case 1:
    //            conversionSuccessful = int.TryParse(stock.text, out Setting.CharacterSetting.stock);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.stock = 0;
    //            }
    //            break;
    //        case 2:
    //            conversionSuccessful = int.TryParse(foreign.text, out Setting.CharacterSetting.foreignCurrency);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.foreignCurrency = 0;
    //            }
    //            break;
    //        case 3:
    //            conversionSuccessful = int.TryParse(rent.text, out Setting.CharacterSetting.estateAndRent);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.estateAndRent = 0;
    //            }
    //            break;
    //        case 4:
    //            conversionSuccessful = int.TryParse(dividend.text, out Setting.CharacterSetting.dividend);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.dividend = 0;
    //            }
    //            break;
    //        case 5:
    //            conversionSuccessful = int.TryParse(annuity.text, out Setting.CharacterSetting.annuity);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.annuity = 0;
    //            }
    //            break;
    //        case 6:
    //            conversionSuccessful = int.TryParse(illinessInsurance.text, out Setting.CharacterSetting.criticalIllnessInsurance);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.criticalIllnessInsurance = 0;
    //            }
    //            break;
    //        case 7:
    //            conversionSuccessful = int.TryParse(longTermCareInsurance.text, out Setting.CharacterSetting.longTermCareInsurance);
    //            if (!conversionSuccessful)
    //            {
    //                Setting.CharacterSetting.longTermCareInsurance = 0;
    //            }
    //            break;
    //    }

    //    TotalAssets.text = TotalAssetsString(Setting.CharacterSetting.Money.ToString());
    //}
}
