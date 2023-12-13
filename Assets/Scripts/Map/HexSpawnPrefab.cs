using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawnPrefab : MonoBehaviour
{

    public GameObject hexPrefab;
    public Vector3 position;


    void Awake()
    {
        Instantiate(hexPrefab, position, Quaternion.identity);
    }
}
