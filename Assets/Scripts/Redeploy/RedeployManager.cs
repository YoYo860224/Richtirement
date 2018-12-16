using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Setting;
using Content;
using UnityEngine.SceneManagement;
using ExtensionMethods;

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
    public Text TitleText;
    public Button redeploy;
    public Text TotalAssets;
    public Transform TotalAssetsUpTransform;

    public GameObject ScrollView1;
    public Transform ScrollView1OutTransform;
    public GameObject ScrollView2;
    public Transform ScrollView2InTransform;

    public Text DepositValue;
    public Text MedicalValue;
    public Text AnnuityValue;
    public Text StocksValue;
    public Text FundsValue;

    public Image[] ScrollView1Images;
    public Text[] ScrollView1Texts;

    private int totalAssets;
    private int tempDeposity;
    private int tempStock;
    private int tempFund;
    private int tempAnnuity;
    private int tempMedicineInsurance;


    string TotalAssetsString(int assets)
    {
        return Content.Redeploy.TotalAssets1 + assets.LocalMoneyString() + Content.Redeploy.TotalAssets2;
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
        TitleText.text = Content.Redeploy.redeploy;


        deposity.slider.maxValue = totalAssets;
        deposity.slider.value = tempDeposity;
        deposity.moneyText.text = tempDeposity.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;
        deposity.percentsText.text = (deposity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        stock.slider.maxValue = totalAssets;
        stock.slider.value = tempStock;
        stock.moneyText.text = tempStock.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        stock.percentsText.text = (stock.slider.normalizedValue * 100.0f).ToString("0.") + " %";
        stock.slider.onValueChanged.AddListener(delegate { AssetsValueChangeCheck(1); });

        fund.slider.maxValue = totalAssets;
        fund.slider.value = tempFund;
        fund.moneyText.text = tempFund.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
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

        TotalAssets.text = TotalAssetsString(totalAssets);

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

        DepositValue.text = Setting.CharacterSetting.deposit.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        MedicalValue.text = Setting.CharacterSetting.medicineInsurance.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        AnnuityValue.text = Setting.CharacterSetting.annuity.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        StocksValue.text = Setting.CharacterSetting.stock.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        FundsValue.text = Setting.CharacterSetting.fund.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;

        Debug.Log("click redeploy button");
        StartCoroutine(FadeOutEditInterface());
 
    }

    public void BackToStory()
    {
        SceneManager.LoadScene("Story");
    }

    IEnumerator FadeOutEditInterface()
    {
        Vector3 ScrollView1PerMove = (ScrollView1OutTransform.position-ScrollView1.transform.position) * Time.deltaTime;

        Vector3 ScrollView2PerMove = (ScrollView2InTransform.position - ScrollView2.transform.position) * Time.deltaTime;

        Vector3 TotalAssetsPerMove = (TotalAssetsUpTransform.position - TotalAssets.transform.position) * Time.deltaTime;

        Debug.Log(ScrollView1PerMove);
        for(float i = 1; i >= 0; i -= Time.deltaTime)
        {
            for (int j = 0; j < ScrollView1Images.Length; j++)
            {
                SetImageAlpha(ScrollView1Images[j], i );
            }
            for (int j = 0; j < ScrollView1Texts.Length; j++)
            {
                SetTextAlpha(ScrollView1Texts[j], i);
                SetTextAlpha(TitleText, i);
            }
            ScrollView1.transform.position += ScrollView1PerMove;
            ScrollView2.transform.position += ScrollView2PerMove;
            TotalAssets.transform.position += TotalAssetsPerMove;

            if(ScrollView1.transform.position.x+ ScrollView1PerMove.x < ScrollView1OutTransform.position.x)
            {
                ScrollView1.transform.position = ScrollView1OutTransform.position;
            }
            if (ScrollView2.transform.position.x + ScrollView2PerMove.x < ScrollView2InTransform.position.x)
            {
                ScrollView2.transform.position = ScrollView2InTransform.position;
            }
            if(TotalAssets.transform.position.y + TotalAssetsPerMove.y > TotalAssetsUpTransform.position.y)
            {
                TotalAssets.transform.position = TotalAssetsUpTransform.position;
            }
            yield return null;
        }
        ScrollView1.transform.position = ScrollView1OutTransform.position;
        ScrollView2.transform.position = ScrollView2InTransform.position;
        TotalAssets.transform.position = TotalAssetsUpTransform.position;
        TitleText.text = "Allocation";

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            SetTextAlpha(TitleText, i);
            yield return null;
        }
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
        deposity.moneyText.text = tempDeposity.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        deposity.percentsText.text = (deposity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        stock.slider.maxValue = totalAssets;
        stock.slider.value = tempStock;
        stock.moneyText.text = tempStock.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        stock.percentsText.text = (stock.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        fund.slider.maxValue = totalAssets;
        fund.slider.value = tempFund;
        fund.moneyText.text = tempFund.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        fund.percentsText.text = (fund.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        annuity.slider.value = tempAnnuity;
        annuity.moneyText.text = tempAnnuity.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        annuity.percentsText.text = (annuity.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        medicineInsurance.slider.value = tempMedicineInsurance;
        medicineInsurance.moneyText.text = tempMedicineInsurance.LocalMoneyString() + Content.Redeploy.BigMoneyUnit;;
        medicineInsurance.percentsText.text = (medicineInsurance.slider.normalizedValue * 100.0f).ToString("0.") + " %";

        TotalAssets.text = TotalAssetsString(totalAssets);

        PropertyBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)tempDeposity / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);

        InvestmentBar.GetComponent<RectTransform>().anchorMax = new Vector2((float)((tempDeposity) + tempStock + tempFund) / (float)(Setting.CharacterSetting.Money + Setting.CharacterSetting.annuity + Setting.CharacterSetting.medicineInsurance + tempAnnuity + tempMedicineInsurance), 0.7f);
    }

    void SetImageAlpha(Image image, float value)
    {
        var tempColor = image.color;
        tempColor.a = value;
        image.color = tempColor;
    }

    void SetTextAlpha(Text text, float value)
    {
        var tempColor = text.color;
        tempColor.a = value;
        text.color = tempColor;
    }
}