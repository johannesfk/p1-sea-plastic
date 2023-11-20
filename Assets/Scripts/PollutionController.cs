using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Unity.Mathematics;


[System.Serializable]
public class Region
{


    public int regionNumber;
    [Header("Amounts")]
    public float regionPolution;
    public float regionWaste;
    public float regionLandfilled;
    public float regionRecycle;
    public float regionTrashDestroyed;
    [Header("Percentages")]
    public float regionPollutionPercentage;
    public float regionRecyclePercentage;
    public float regionTrashDestroyedPercentage;
    public float regionLandfillPercentage;
}


public class PollutionController : MonoBehaviour
{
    public static PollutionController instance;

    public List<Region> regions;

    public int currentRegion;

    [Header("Stuff")]
    public UnityEngine.UI.Slider slider;

    [Header("World Stats")]
    public float totalRegionPolution;
    public float polutionPercentage;

    [SerializeField] private TMP_Text polutionPercentageText;
    [SerializeField] private TMP_Text landfillPercentageText;

    public float polution = 0;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    void Awake()
    {
        polution = startPolution;
        instance = this;

        currentRegion = 0;

        if (regions.Count > 0)
        {
            for (int i = 0;i < regions.Count; i++)
            {
                regions[i].regionNumber = i;
            }
        }

    }

    // Update is called once per frame
    private void Update()
    {

        polutionPercentage = polution / polutionThreshhold * 100;

        //region percentages
        regions[currentRegion].regionPollutionPercentage = regions[currentRegion].regionPolution / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionRecyclePercentage = regions[currentRegion].regionRecycle / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionTrashDestroyedPercentage = regions[currentRegion].regionTrashDestroyed / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionLandfillPercentage = regions[currentRegion].regionLandfilled / regions[currentRegion].regionWaste * 100;

        slider.value = polutionPercentage;
        


        landfillPercentageText.text = regions[currentRegion].regionLandfillPercentage + "%";

        polutionPercentageText.text = ((int)polutionPercentage + "%").ToString();



    }
    
    public void EndDayAdd()
    {

        if (regions.Count > 0)
        {
            for (int i = 0; i < regions.Count; i++)
            {
                polution =+ regions[i].regionPolution;
            }
        }
    }


}
