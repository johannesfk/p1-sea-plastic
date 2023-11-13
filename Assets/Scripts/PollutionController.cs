using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("World Stats")]

    public float polutionProcentage;
    public float polution = 0;
    public float polutionMultiplier = 0.1f;

    private float startPolution = 2;
    private float polutionThreshhold = 10000;

    [Header("Regions")]
    private float region1Polution = 0.05f;
    private float region2Polution = 0.03f;
    private float region3Polution = 0.005f;
    private float region4Polution = 0.015f;

    void Awake()
    {
        polution = startPolution;

    }

    // Update is called once per frame
    private void Update()
    {
        polutionMultiplier = region1Polution + region2Polution + region3Polution + region4Polution;

        polutionProcentage = polution / polutionThreshhold * 100;

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
