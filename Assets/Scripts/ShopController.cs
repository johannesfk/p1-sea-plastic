using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    [SerializeField] private GameObject shopScreen;

    #region ShopMenu

    public void BuyRecycle()
    {
        shopScreen.SetActive(false);
        Debug.Log("Buy THAT RECYCLE");
    }

    public void BuyLandfill()
    {
        shopScreen.SetActive(false);
        Debug.Log("Buy THAT LANDFILLSHIT");
    }

    public void BuyBoat()
    {
        shopScreen.SetActive(false);
        Debug.Log("Buy THAT MF BOAT");
    }

    public void BuyIncinirator()
    {
        shopScreen.SetActive(false);
        Debug.Log("Buy THAT BURNER THING");
    }

    #endregion

}
