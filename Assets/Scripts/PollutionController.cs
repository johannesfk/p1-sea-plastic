using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class WorldStats
{
    public static List<float> regionPolution;

    public static float polutionTotal;

  //  public static object polutionRegion { get; internal set; }
    
    public void Awake()
    {
        polutionTotal = 0;
        Debug.Log("finkledink");
    }

    private void Update()
    {

        if (regionPolution != null)
        {
            foreach (var item in regionPolution)
            {
                polutionTotal += item;
            }
        }
        
    }

}

[System.Serializable]
public class Region
{

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

    public void Update()
    {
        regionPolution = regionWaste - regionRecycle - regionTrashDestroyed - regionLandfilled;

        Debug.Log("dillermand");

        //region Percentages
        regionPollutionPercentage = regionPolution / regionWaste * 100;

        regionRecyclePercentage = regionRecycle / regionWaste * 100;

        regionTrashDestroyedPercentage = regionTrashDestroyed / regionWaste * 100;

        regionLandfillPercentage = regionLandfilled / regionWaste * 100;


    }
}


public class PollutionController : MonoBehaviour
{
    public static PollutionController instance;

    public List<Region> regions;

    [Header("Stuff")]
    public UnityEngine.UI.Slider slider;

    [Header("World Stats")]
    public float polutionPercentage;

    [SerializeField] private TMP_Text polutionProcentText;

    public float polution = 0;
    public float polutionMultiplier;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    void Awake()
    {
        polution = startPolution;
        instance = this;

        if (WorldStats.regionPolution != null)
        {
            if (WorldStats.regionPolution.Count > 0)
            {
                for (int i = 0; i < regions.Count; i++)
                {
                    WorldStats.regionPolution[i] = regions[i].regionPolution;
                }
            }
        }
       
    }

    // Update is called once per frame
    private void Update()
    {

        polutionMultiplier = WorldStats.polutionTotal;

        polutionPercentage = polution / polutionThreshhold * 100;

        slider.value = polutionPercentage;
        
        polutionProcentText.text = ((int)polutionPercentage + "%").ToString();

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
