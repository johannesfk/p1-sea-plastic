using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regions : MonoBehaviour
{

    [Header("Amounts")]
    public float regionPolution;
    public float regionWaste = 10;
    public float regionLandfilled = 1;
    public float regionRecycle = 1;
    public float regionTrashDestroyed = 1;
    [Header("Percentages")]
    public float regionPollutionPercentage;
    public float regionRecyclePercentage;
    public float regionTrashDestroyedPercentage;
    public float regionLandfillPercentage;

    private void Update()
    {
        regionPolution = regionWaste - regionRecycle - regionTrashDestroyed - regionLandfilled;

        //region Percentages
        regionPollutionPercentage = regionPolution / regionWaste * 100;

        regionRecyclePercentage = regionRecycle / regionWaste * 100;

        regionTrashDestroyedPercentage = regionTrashDestroyed / regionWaste * 100;
        
        regionLandfillPercentage = regionLandfilled / regionWaste * 100;


    }


}
