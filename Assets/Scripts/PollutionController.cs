using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class PollutionController : MonoBehaviour
{
    public static PollutionController instance;

    [Header("Stuff")]
    public UnityEngine.UI.Slider slider;

    [Header("World Stats")]
    public float polutionPercentage;

    [SerializeField] private TMP_Text polutionProcentText;

    public float polution = 0;
    public float polutionMultiplier = 10f;

    private float startPolution = 200;
    private float polutionThreshhold = 10000;

    void Awake()
    {
        polution = startPolution;
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
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
