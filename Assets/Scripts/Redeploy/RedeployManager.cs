using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using Content;
using UnityEngine.SceneManagement;

public class RedeployManager : MonoBehaviour {
    public GameObject PropertyBar;
    public GameObject InvestmentBar;

    public GameObject deposityGameObject;
    public GameObject stockGameObject;
    public GameObject fundGameObject;
    public GameObject annuityGameObject;
    public GameObject medicineInsuranceGameObject;

    private RedeployField deposity;
    private RedeployField stock;
    private RedeployField fund;
    private RedeployField annuity;
    private RedeployField medicineInsurance;

    //public Text title;
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

    private void Awake()
    {
        deposity = deposityGameObject.GetComponent<RedeployField>();
        stock = stockGameObject.GetComponent<RedeployField>();
        fund = fundGameObject.GetComponent<RedeployField>();
        annuity = annuityGameObject.GetComponent<RedeployField>();
        medicineInsurance = medicineInsuranceGameObject.GetComponent<RedeployField>();

        if (Setting.CharacterSetting.age > 65)
        {
            annuityGameObject.SetActive(false);
            medicineInsuranceGameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        totalAssets = Setting.CharacterSetting.Money;
        tempDeposity = Setting.CharacterSetting.deposit;
        tempStock = Setting.CharacterSetting.stock;
        tempFund = Setting.CharacterSetting.fund;
        tempAnnuity = 0;
        tempMedicineInsurance = 0;

        //title.text = Content.Redeploy.Title;
        redeployText.text = Content.Redeploy.redeploy;


        deposity.slider.maxValue = totalAssets;
        deposity.slider.value = tempDeposity;
        deposity.moneyText.text = tempDeposity.ToString() + " M";
        deposity.percentsText.text = (deposity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        stock.slider.maxValue = totalAssets;
        stock.slider.value = tempStock;
        stock.moneyText.text = tempStock.ToString() + " M";
        stock.percentsText.text = (stock.slider.normalizedValue * 100.0f).ToString("0.") + " %";
        stock.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(1); });

        fund.slider.maxValue = totalAssets;
        fund.slider.value = tempFund;
        fund.moneyText.text = tempFund.ToString() + " M";
        fund.percentsText.text = (fund.slider.normalizedValue * 100.0f).ToString("0.") + " %";
        fund.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(2); });

        annuity.slider.maxValue = Setting.SystemSetting.AnnuityMax;
        annuity.slider.value = 0;
        annuity.moneyText.text = "0 M";
        annuity.percentsText.text = "0 %";
        annuity.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(3); });

        medicineInsurance.slider.maxValue = Setting.SystemSetting.MedicineInsuranceMax;
        medicineInsurance.slider.value = 0;
        medicineInsurance.moneyText.text = "0 M";
        medicineInsurance.percentsText.text = "0 %";
        medicineInsurance.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(4); });

        TotalAssets.text = TotalAssetsString(totalAssets.ToString());

        PropertyBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)tempDeposity / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);

        InvestmentBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)((tempDeposity) + tempStock + tempFund) / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void RedeployButtom()
    {
        Setting.CharacterSetting.deposit = (int)deposity.slider.value;
        Setting.CharacterSetting.stock = (int)stock.slider.value;
        Setting.CharacterSetting.fund = (int)fund.slider.value;
        Setting.CharacterSetting.annuity += (int)annuity.slider.value;
        Setting.CharacterSetting.medicineInsurance += (int)medicineInsurance.slider.value;

        Debug.Log("click redeploy button");
        SceneManager.LoadScene("Story");
    }

    public void AssetsValueChangeCheck(int id)
    {
        switch (id)
        {
            case 1:
                tempStock = (int)stock.slider.value;
                tempDeposity = totalAssets - tempStock - tempFund;
                if (tempDeposity <= 0)
                {
                    tempDeposity = 0;
                    tempStock = totalAssets - tempFund;
                }
                break;

            case 2:
                tempFund = (int)fund.slider.value;
                tempDeposity = totalAssets - tempStock - tempFund;
                if (tempDeposity <= 0)
                {
                    tempDeposity = 0;
                    tempFund = totalAssets - tempStock;
                }
                break;

            case 3:
                tempAnnuity = (int)annuity.slider.value;
                totalAssets = Setting.CharacterSetting.Money - tempAnnuity - tempMedicineInsurance;
                tempDeposity = totalAssets - tempStock - tempFund;
                if (tempDeposity <= 0)
                {
                    tempDeposity = 0;
                    tempAnnuity = Setting.CharacterSetting.Money - tempStock - tempFund - tempMedicineInsurance;
                    totalAssets = Setting.CharacterSetting.Money - tempAnnuity - tempMedicineInsurance;
                }
                break;

            case 4:
                tempMedicineInsurance = (int)medicineInsurance.slider.value;
                totalAssets = Setting.CharacterSetting.Money - tempAnnuity - tempMedicineInsurance;
                tempDeposity = totalAssets - tempStock - tempFund;
                if (tempDeposity <= 0)
                {
                    tempDeposity = 0;
                    tempMedicineInsurance = Setting.CharacterSetting.Money - tempStock - tempFund - tempAnnuity;
                    totalAssets = Setting.CharacterSetting.Money - tempAnnuity - tempMedicineInsurance;
                }
                break;
        }

        deposity.slider.maxValue = totalAssets;
        deposity.slider.value = tempDeposity;
        deposity.moneyText.text = tempDeposity.ToString() + " M";
        deposity.percentsText.text = (deposity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        stock.slider.maxValue = totalAssets;
        stock.slider.value = tempStock;
        stock.moneyText.text = tempStock.ToString() + " M";
        stock.percentsText.text = (stock.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        fund.slider.maxValue = totalAssets;
        fund.slider.value = tempFund;
        fund.moneyText.text = tempFund.ToString() + " M";
        fund.percentsText.text = (fund.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        annuity.slider.value = tempAnnuity;
        annuity.moneyText.text = tempAnnuity.ToString() + " M";
        annuity.percentsText.text = (annuity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        medicineInsurance.slider.value = tempMedicineInsurance;
        medicineInsurance.moneyText.text = tempMedicineInsurance.ToString() + " M";
        medicineInsurance.percentsText.text = (medicineInsurance.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        TotalAssets.text = TotalAssetsString(totalAssets.ToString());

        PropertyBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)tempDeposity / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);

        InvestmentBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)((tempDeposity) + tempStock + tempFund) / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);
    }
}
