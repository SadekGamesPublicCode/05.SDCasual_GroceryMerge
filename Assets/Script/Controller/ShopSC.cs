using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] HomeSC homeCtr;
    [HideInInspector] DataSC data;
    [SerializeField] GameObject powerupPnl, moneypackPnl;
    [SerializeField] Text pGemTxt, pMOneyTxt;
    int pCurMoney, pCurGem;
    int itemPriceGuide, itemPriceClock, itemPriceMatch;
    int packPrice1, packPrice2, packPrice3, packPrice4, packPrice5, packPriceNoAds;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        homeCtr = GameObject.Find("MenuMN").GetComponent<HomeSC>();
        moneypackPnl.gameObject.SetActive(false);
        SetPrice();
        LoadPlayerData();
    }

    public void OnClosePanel() => homeCtr.UpdateHomeInfo();

    void LoadPlayerData()
    {
        pCurGem = data.pGems;
        pCurMoney = data.pTotalScore;

        pGemTxt.text = pCurGem.ToString();
        pMOneyTxt.text = pCurMoney.ToString();
    }
    void SetPrice()
    {
        itemPriceGuide = 100;
        itemPriceClock = 100;
        itemPriceMatch = 100;

        packPrice1 = 1;
        packPrice2 = 10;
        packPrice3 = 100;
        packPrice4 = 1000;
    }
    bool IsEnoughMoney(int price, int type)
    {
        //Check this everytime buy anything
        if(type == 1)
        {
            //Buy point
            if (pCurMoney >= price)
            {
                return true;
            }
            else return false;
        }else if (type == 2)
        {
            if (pCurGem >= price)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    void HandleBuyItem(int vaule, int type)
    {
        //Update UI here
        switch (type)
        {
            case 1:
                pCurMoney = vaule;
                pMOneyTxt.text = vaule.ToString();
                data.UpdateTotalScore(pCurMoney);
                break;
            case 2:
                pCurGem = vaule;
                pGemTxt.text = vaule.ToString();
                data.UpdateTotalGem(pCurGem);
                break;
            case 3:
                break;

        }
        LoadPlayerData();
    }

    #region Handle Power Up
    public void OnBuyLine()
    {
        if(IsEnoughMoney(itemPriceGuide, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceGuide;
            pCurMoney = tempNewCurrency;
            HandleBuyItem(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyClock()
    {
        if (IsEnoughMoney(itemPriceClock, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceClock;
            pCurMoney = tempNewCurrency;
            HandleBuyItem(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyMatch()
    {
        if (IsEnoughMoney(itemPriceMatch, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceMatch;
            pCurMoney = tempNewCurrency;
            HandleBuyItem(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    #endregion

    #region Handle Money Pack
    void HandleBuyPack(int vaule, int type)
    {
        //Update UI here
        switch (type)
        {
            case 1:
                pCurMoney = vaule;
                pMOneyTxt.text = vaule.ToString();
                data.UpdateTotalScore(pCurMoney);
                break;
            case 2:
                pCurGem = vaule;
                pGemTxt.text = vaule.ToString();
                data.UpdateTotalGem(pCurGem);
                break;
            case 3:
                break;

        }
        LoadPlayerData();
    }
    public void OnBuyPack1()
    {
        if (IsEnoughMoney(packPrice1, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice1;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack2()
    {
        if (IsEnoughMoney(packPrice2, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice2;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack3()
    {
        if (IsEnoughMoney(packPrice3, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice3;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack4()
    {
        if (IsEnoughMoney(packPrice4, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice4;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack5()
    {
        if (IsEnoughMoney(packPrice5, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice5;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }

    }
    public void OnBuyNoAds()
    {
        if (IsEnoughMoney(packPriceNoAds, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPriceNoAds;
            pCurGem = tempNewCurrency;
            HandleBuyPack(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    #endregion
}
