using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PollutionController : MonoBehaviour
{
    // Start is called before the first frame update

    public static PollutionController instance;

    [Header("World Stats")]

    public float polutionProcent;
    public TMP_Text polutionProcentText;
    public float polution = 0;
    public float polutionMultiplier = 10f;

    private float startPolution = 2;
    private float polutionThreshhold = 10000;

    [Header("Regions")]
    private float region1Polution = 12f;
    private float region2Polution = 31f;
    private float region3Polution = 55f;
    private float region4Polution = 2f;
    private float region5Polution = 0;
    private float region6Polution = 0;
    private float region7Polution = 0;

    void Awake()
    {
        polution = startPolution;
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        polutionMultiplier = region1Polution + region2Polution + region3Polution + region4Polution + region5Polution + region6Polution + region7Polution;

        polutionProcent = polution / polutionThreshhold * 100;

        polutionProcentText.text = ((int)polutionProcent)+"%".ToString();

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
