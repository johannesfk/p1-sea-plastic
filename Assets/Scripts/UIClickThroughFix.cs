using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickThroughFix : MonoBehaviour
{
    public static bool uiActive;

    private void Awake()
    {
        uiActive = false;
    }

    void OnEnable()
    {
        uiActive = true;
    }

    // Update is called once per frame
    void OnDisable()
    {
        uiActive = false;
    }
}
