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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
