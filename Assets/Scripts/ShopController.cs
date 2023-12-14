using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static HexGrid;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    [SerializeField] private GameObject shopScreen;

    [Header("Money")]
    [SerializeField] private int startMoney;
    public float money;
    [SerializeField] private int startIncome;
    public int income;
    public float incomeMultiplier;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int recycleExtraIncome;

    [Header("Prices")]
    [SerializeField] private int recyclePrice;
    [SerializeField] private int landfillPrice;
    [SerializeField] private int boatPrice;
    [SerializeField] private int incineratorPrice;

    //UI Price Text
    [SerializeField] private TMP_Text recyclePriceText;
    [SerializeField] private TMP_Text landfillPriceText;
    [SerializeField] private TMP_Text boatPriceText;
    [SerializeField] private TMP_Text incineratorPriceText;
    private void Awake()
    {
        instance = this;

        money = startMoney;

        income = startIncome;

    }


    private void Start()
    {
        HexInteraction.instance.OnCellTypePlaced += HandleCellTypePlaced;
        GameManager.instance.OnNewDay += HandleNewDay;

        recyclePriceText.text = "cost: " + recyclePrice.ToString();
        landfillPriceText.text = "cost: " + landfillPrice.ToString();
        boatPriceText.text = "cost: " + boatPrice.ToString();
        incineratorPriceText.text = "cost: " + incineratorPrice.ToString();

    }

    private void Update()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    void HandleCellTypePlaced()
    {
        // This code will be executed when a cell type is placed
        Debug.Log("A cell type has been placed");

    }

    private void HandleNewDay()
    {
        Debug.Log("New day");

        money += income * incomeMultiplier;
    }


    #region ShopMenu
    public void BuyRecycle()
    {
        if (money >= recyclePrice)
        {
            money -= recyclePrice;
            BoughtItem();
            HexInteraction.instance.PlaceCellType(terrainType.recycler);

            Debug.Log("Buy THAT RECYCLE BASTARD");
        }
        else
        {
            Debug.Log(">YOU CANT BUY THAT SHIT YOU POOR FUCK");
        }
    }

    public void BuyLandfill()
    {
        if (money >= landfillPrice)
        {
            money -= landfillPrice;
            BoughtItem();
            HexInteraction.instance.PlaceCellType(terrainType.landfill);

            Debug.Log("Buy THAT LANDFILLSHIT");
        }
        else
        {
            Debug.Log(">YOU CANT BUY THAT SHIT YOU POOR FUCK");
        }

    }

    public void BuyBoat()
    {
        if (money >= boatPrice)
        {
            money -= boatPrice;
            BoughtItem();
            HexInteraction.instance.PlaceCellType(terrainType.boatCleaner);

            Debug.Log("Buy THAT MF BOAT");
        }
        else
        {
            Debug.Log(">YOU CANT BUY THAT SHIT YOU POOR FUCK");
        }

    }

    public void BuyIncinirator()
    {
        if (money >= incineratorPrice)
        {
            money -= incineratorPrice;
            BoughtItem();
            HexInteraction.instance.PlaceCellType(terrainType.incinerator);

            Debug.Log("Buy THAT BURNER THING");
        }
        else
        {
            Debug.Log(">YOU CANT BUY THAT SHIT YOU POOR FUCK");
        }

    }

    private void BoughtItem()
    {
        shopScreen.SetActive(false);
        GameManager.instance.Resume();
    }

    #endregion

    public void RecycleBuilded()
    {
        income += recycleExtraIncome;
    }

    void OnDestroy()
    {
        HexInteraction.instance.OnCellTypePlaced -= HandleCellTypePlaced;
        GameManager.instance.OnNewDay -= HandleNewDay;
    }

}
