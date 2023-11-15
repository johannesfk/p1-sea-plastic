    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    public Image[] imagePieCharts;
    public float[] Values;

    void Start()
    {
        SetValues(Values);
    }

    void Update()
    {
        
    }
    public void SetValues(float[] valuesToSet)
    {
        float totalvalues = 0;
        for(int i = 0; i < imagePieCharts.Length; i++)
        {
            totalvalues += FindPercentage(valuesToSet, i) ;
            imagePieCharts[i].fillAmount = totalvalues;
        }
    }
    private float FindPercentage(float[] valueToSet, int index)
    {
        float totalAmount = 0;
        for (int i = 0; i < valueToSet.Length; i++)
        {
            totalAmount += valueToSet[i];
        }
        return valueToSet[index] / totalAmount;
    }
}
