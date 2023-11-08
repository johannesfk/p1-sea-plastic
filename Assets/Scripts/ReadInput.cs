using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    public Text obj_text;
    public InputField display;

    void Start()
    {
        obj_text.text = PlayerPrefs.GetString("Company_Name");  
    }
    public void DisplayText()
    {
        obj_text.text = display.text;
        PlayerPrefs.SetString("Company_Name", obj_text.text);
        PlayerPrefs.Save();
    }
}
