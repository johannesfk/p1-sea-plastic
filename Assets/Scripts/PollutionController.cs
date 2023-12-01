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

    public string regionName;   
    public int regionNumber;
    [Header("Amounts")]
    public float regionPolution;
    public float regionWaste;
    public float regionLandfilled;
    public float regionRecycle;
    public float regionTrashDestroyed;
    [Header("Percentages")]
    public float regionPolutionPercentage;
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
    public float worldPolution = 0;
    public float polutionPercentage;

    public float worldWaste;
    public float wolrdWastePolluted;

    public float worldLandfilled;
    public float worldLandfilledPercentage;

    public float worldRecycle;
    public float worldRecyclePercentage;

    public float worldTrashDestroyed;
    public float worldTrashDestroyedPercentage;

    [SerializeField] private TMP_Text polutionPercentageText;
    [SerializeField] private TMP_Text regionPolutionPercentageText;
    [SerializeField] private TMP_Text landfillPercentageText;
    [SerializeField] private TMP_Text incineratedPercentageText;
    [SerializeField] private TMP_Text recyclePercentageText;
    [SerializeField] private TMP_Text currentRegionName;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    void Awake()
    {
        worldPolution = startPolution;
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
        //World Percentages
        polutionPercentage = worldPolution / polutionThreshhold * 100;
        slider.value = polutionPercentage;

        worldLandfilledPercentage = worldLandfilled / worldWaste * 100;
        worldRecyclePercentage = worldRecycle / worldWaste * 100;
        worldTrashDestroyedPercentage = worldTrashDestroyed / worldWaste * 100;


        //region percentages
        regions[currentRegion].regionPolutionPercentage = regions[currentRegion].regionPolution / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionRecyclePercentage = regions[currentRegion].regionRecycle / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionTrashDestroyedPercentage = regions[currentRegion].regionTrashDestroyed / regions[currentRegion].regionWaste * 100;
        regions[currentRegion].regionLandfillPercentage = regions[currentRegion].regionLandfilled / regions[currentRegion].regionWaste * 100;

        //Text Percentages
        polutionPercentageText.text = ((int)polutionPercentage + "%").ToString();

        regionPolutionPercentageText.text = ((int)regions[currentRegion].regionPolutionPercentage + "%").ToString();

        landfillPercentageText.text = ((int)regions[currentRegion].regionLandfillPercentage + "%").ToString();

        recyclePercentageText.text = ((int)regions[currentRegion].regionRecyclePercentage + "%").ToString();

        incineratedPercentageText.text = ((int)regions[currentRegion].regionTrashDestroyedPercentage + "%").ToString();

        currentRegionName.text = regions[currentRegion].regionName;
    }
    
    public void EndDayAdd()
    {

        if (regions.Count > 0)
        {
            for (int i = 0; i < regions.Count; i++)
            {   

                worldPolution += regions[i].regionPolution;
                worldWaste += regions[i].regionWaste;
                worldLandfilled += regions[i].regionLandfilled;
                worldRecycle += regions[i].regionRecycle;
                worldTrashDestroyed += regions[i].regionTrashDestroyed;
                
            }
        }
    }


}
