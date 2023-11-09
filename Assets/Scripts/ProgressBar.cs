using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Slider slider;
    public float pollution = 0;

    // Start is called before the first frame update
    void Start()
    {
        pollution = 0;
        slider.value = pollution;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateProgress();
    }


    public void UpdateProgress()
    {
        pollution = pollution + 1;
        slider.value = pollution;



    }




}
