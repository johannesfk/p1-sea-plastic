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
    public float polution = 0;
    public float polutionMultiplier = 10f;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    [Header("Region1")]
    [SerializeField] private float region1Polution;
    public float r1Waste = 10;
    public float r1Recycle = 1;
    public float r1TrashDestroyed = 1;

    [Header("Region2")]
    [SerializeField] private float region2Polution;
    public float r2Waste = 10;
    public float r2Recycle = 1;
    public float r2TrashDestroyed = 1;

    [Header("Region3")]
    [SerializeField] private float region3Polution;
    public float r3Waste = 10;
    public float r3Recycle = 1;
    public float r3TrashDestroyed = 1;

    [Header("Region4")]
    [SerializeField] private float region4Polution;
    public float r4Waste = 10;
    public float r4Recycle = 1;
    public float r4TrashDestroyed = 1;

    [Header("Region5")]
    [SerializeField] private float region5Polution;
    public float r5Waste = 10;
    public float r5Recycle = 1;
    public float r5TrashDestroyed = 1;

    [Header("Region6")]
    [SerializeField] private float region6Polution;
    public float r6Waste = 5;
    public float r6Recycle = 1;
    public float r6TrashDestroyed = 1;

    void Awake()
    {
        polution = startPolution;
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {

        region1Polution = r1Waste - r1Recycle - r1TrashDestroyed;
        region2Polution = r2Waste - r2Recycle - r2TrashDestroyed;
        region3Polution = r3Waste - r3Recycle - r3TrashDestroyed;
        region4Polution = r4Waste - r4Recycle - r4TrashDestroyed;
        region5Polution = r5Waste - r5Recycle - r5TrashDestroyed;
        region6Polution = r6Waste - r6Recycle - r6TrashDestroyed;

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
