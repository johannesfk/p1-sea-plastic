using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("World Stats")]
    public float polution = 25;
    public float polutionMultiplier = 0.1f;

    private float startPolution = 25;
    private float polutionThreshhold = 100;

    [Header("Regions")]
    private float region1Polution = 0.05f;
    private float region2Polution = 0.03f;
    private float region3Polution = 0.005f;
    private float region4Polution = 0.015f;

    [Header("Company Stats")]
    public string companyName;
    public float money = 0;
    public float income = 1;
    public float popularity = 0;

    private float startMoney = 100;

    [Header("Upgrades")]
    [SerializeField] private bool donationUpgrade = false;

    private float donationChance;
    private void Awake()
    {
        if (gameObject != null)
        {
            polution = startPolution;
            money = startMoney;
        }
    }

    private void Update()
    {
        polutionMultiplier = region1Polution + region2Polution + region3Polution + region4Polution;
    }

    private void FixedUpdate()
    {

        if (polution >= polutionThreshhold)
        {
            Debug.Log("Du fucking Tabte lol");
        }

        if (polution < polutionThreshhold)
        {
            polution += Time.fixedDeltaTime * polutionMultiplier;
        }

        money += Time.fixedDeltaTime * income;

        if (donationUpgrade)
        {
            donationChance = Random.Range(0, 100);

            if (donationChance < popularity)
            {
                money += Random.Range(0, donationChance) / 2;
                Debug.Log("You got a dontaion of: " + donationChance / 2 + " Smackeroos");
            }
        }

    }

    public void ChangeName(string addedText)
    {
        companyName = addedText;
        Debug.Log("Your Company is named: " + addedText);
    }

}
