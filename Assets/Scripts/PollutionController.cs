using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PollutionController : MonoBehaviour
{
    public static PollutionController instance;

    [Header("Stuff")]
    public Slider slider;

    [Header("World Stats")]
    public float polutionProcent;
    public TMP_Text polutionProcentText;

    public TMP_Text regionPollutionText;

    public float polution = 0;
    public float polutionMultiplier = 10f;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    [Header("Region1")]
    [SerializeField] private float region1Polution;
    public float r1Waste = 10;
    public float r1Recycle = 1;
    public float r1TrashDestroyed = 1;
    public float r1pollutionProcent;
    public float r1recycleProcent;
    public float r1trashDestroyedProcent;

    [Header("Region2")]
    [SerializeField] private float region2Polution;
    public float r2Waste = 10;
    public float r2Recycle = 1;
    public float r2TrashDestroyed = 1;
    public float r2pollutionProcent;
    public float r2recycleProcent;
    public float r2trashDestroyedProcent;

    [Header("Region3")]
    [SerializeField] private float region3Polution;
    public float r3Waste = 10;
    public float r3Recycle = 1;
    public float r3TrashDestroyed = 1;
    public float r3pollutionProcent;
    public float r3recycleProcent;
    public float r3trashDestroyedProcent;

    [Header("Region4")]
    [SerializeField] private float region4Polution;
    public float r4Waste = 10;
    public float r4Recycle = 1;
    public float r4TrashDestroyed = 1;
    public float r4pollutionProcent;
    public float r4recycleProcent;
    public float r4trashDestroyedProcent;

    [Header("Region5")]
    [SerializeField] private float region5Polution;
    public float r5Waste = 10;
    public float r5Recycle = 1;
    public float r5TrashDestroyed = 1;
    public float r5pollutionProcent;
    public float r5recycleProcent;
    public float r5trashDestroyedProcent;

    [Header("Region6")]
    [SerializeField] private float region6Polution;
    public float r6Waste = 5;
    public float r6Recycle = 1;
    public float r6TrashDestroyed = 1;
    public float r6pollutionProcent;
    public float r6recycleProcent;
    public float r6trashDestroyedProcent;

    void Awake()
    {
        polution = startPolution;
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        //the regions polution calculations
        region1Polution = r1Waste - r1Recycle - r1TrashDestroyed;
        region2Polution = r2Waste - r2Recycle - r2TrashDestroyed;
        region3Polution = r3Waste - r3Recycle - r3TrashDestroyed;
        region4Polution = r4Waste - r4Recycle - r4TrashDestroyed;
        region5Polution = r5Waste - r5Recycle - r5TrashDestroyed;
        region6Polution = r6Waste - r6Recycle - r6TrashDestroyed;

        //the regions Procentages
        r1pollutionProcent = region1Polution / r1Waste * 100;
        r2pollutionProcent = region2Polution / r2Waste * 100;
        r3pollutionProcent = region3Polution / r3Waste * 100;
        r4pollutionProcent = region4Polution / r4Waste * 100;
        r5pollutionProcent = region5Polution / r5Waste * 100;
        r6pollutionProcent = region6Polution / r6Waste * 100;

        //Region Recycle Procent
        r1recycleProcent = r1Recycle / r1Waste * 100;
        r2recycleProcent = r2Recycle / r2Waste * 100;
        r3recycleProcent = r3Recycle / r3Waste * 100;
        r4recycleProcent = r4Recycle / r4Waste * 100;
        r5recycleProcent = r5Recycle / r5Waste * 100;
        r6recycleProcent = r6Recycle / r6Waste * 100;

        //Region Destroyed Procent
        r1trashDestroyedProcent = r1TrashDestroyed / r2Waste * 100;
        r2trashDestroyedProcent = r2TrashDestroyed / r2Waste * 100;
        r3trashDestroyedProcent = r3TrashDestroyed / r3Waste * 100;
        r4trashDestroyedProcent = r4TrashDestroyed / r4Waste * 100;
        r5trashDestroyedProcent = r5TrashDestroyed / r5Waste * 100;
        r6trashDestroyedProcent = r6TrashDestroyed / r6Waste * 100;


    //region percentage to the UI
    regionPollutionText.text = ("Pollution " + "Region: " + (int)r1pollutionProcent + "% " + "Region: " + (int)r2pollutionProcent + "% " + "Region: " + (int)r3pollutionProcent + "% " + "Region: " + (int)r4pollutionProcent + "% " + "Region: " + (int)r5pollutionProcent + "% " + "Region: " + (int)r6pollutionProcent + "% ");


        polutionMultiplier = region1Polution + region2Polution + region3Polution + region4Polution + region5Polution + region6Polution;

        polutionProcent = polution / polutionThreshhold * 100;

        polutionProcentText.text = ((int)polutionProcent)+"%".ToString();

        slider.value = polutionProcent;

       


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

    }

}
