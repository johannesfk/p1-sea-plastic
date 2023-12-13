using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    [Header("Money")]
    [SerializeField] private int startMoney;
    public int money;

    [Header("Prices")]
    [SerializeField] private int recyclePrice;
    [SerializeField] private int landfillPrice;
    [SerializeField] private int boatPrice;
    [SerializeField] private int incineratorPrice;

    [SerializeField] private GameObject shopScreen;
    private void Awake()
    {
        instance = this;

        money = startMoney;

    }

    #region ShopMenu

    public void BuyRecycle()
    {   
        if (money >= recyclePrice)
        {
            money -= recyclePrice;
            shopScreen.SetActive(false);
            HexGrid.instance.PlaceCellType(HexGrid.Structures.recycler);

            Debug.Log("Buy THAT RECYCLE BASTARD");
        }
    }

    public void BuyLandfill()
    {
        if (money >= landfillPrice)
        {
            money -= landfillPrice;
            shopScreen.SetActive(false);
            HexGrid.instance.PlaceCellType(HexGrid.Structures.landfill);

            Debug.Log("Buy THAT LANDFILLSHIT");
        }
  
    }

    public void BuyBoat()
    {
        if (money >= boatPrice)
        {
            money -= boatPrice;
            shopScreen.SetActive(false);
            HexGrid.instance.PlaceCellType(HexGrid.Structures.boatCleaner);

            Debug.Log("Buy THAT MF BOAT");
        }
       
    }

    public void BuyIncinirator()
    {
        if (money >= incineratorPrice)
        {
            money -= incineratorPrice;
            shopScreen.SetActive(false);
            HexGrid.instance.PlaceCellType(HexGrid.Structures.incinerator);

            Debug.Log("Buy THAT BURNER THING");
        }
        
    }

    #endregion

}
