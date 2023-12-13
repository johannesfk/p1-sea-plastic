using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    [SerializeField] private GameObject shopScreen;

    [Header("Money")]
    [SerializeField] private int startMoney;
    public int money;
    [SerializeField] private int startIncome;
    public int income;


    [Header("Prices")]
    [SerializeField] private int recyclePrice;
    [SerializeField] private int landfillPrice;
    [SerializeField] private int boatPrice;
    [SerializeField] private int incineratorPrice;

    private void Awake()
    {
        instance = this;

        money = startMoney;
    }

    #region Money

    private void AddMoney()
    {
        money += income;
    }

    #endregion


    private void Start()
    {
        HexInteraction.instance.OnCellTypePlaced += HandleCellTypePlaced;
        GameManager.instance.OnNewDay += HandleNewDay;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shopScreen.SetActive(!shopScreen.activeSelf);
        }
    }

    void HandleCellTypePlaced()
    {
        // This code will be executed when a cell type is placed
        Debug.Log("A cell type has been placed");
    }

    private void HandleNewDay()
    {
        Debug.Log("New day");
        throw new NotImplementedException();
    }


    #region ShopMenu
    public void BuyRecycle()
    {
        if (money >= recyclePrice)
        {
            money -= recyclePrice;
            shopScreen.SetActive(false);
            HexInteraction.instance.PlaceCellType(HexGrid.Structures.recycler);

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
            shopScreen.SetActive(false);
            HexInteraction.instance.PlaceCellType(HexGrid.Structures.landfill);

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
            shopScreen.SetActive(false);
            HexInteraction.instance.PlaceCellType(HexGrid.Structures.boatCleaner);

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
            shopScreen.SetActive(false);
            HexInteraction.instance.PlaceCellType(HexGrid.Structures.incinerator);

            Debug.Log("Buy THAT BURNER THING");
        }
        else
        {
            Debug.Log(">YOU CANT BUY THAT SHIT YOU POOR FUCK");
        }

    }

    #endregion

    void OnDestroy()
    {
        HexInteraction.instance.OnCellTypePlaced -= HandleCellTypePlaced;
    }

}
