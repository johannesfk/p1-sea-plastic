using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexGrid;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Vector3 position;
    public terrainType terrainType;
    public Color color;

    public GameObject forest;
    public GameObject mountain;
    public GameObject water;
    public GameObject boatCleaner;

    public GameObject riverZ;
    public GameObject riverX;
    public GameObject riverXZ;
    public GameObject riverZX;

    private GameObject cellPrefab;

    public Dictionary<terrainType, GameObject> terrainPrefabs;


    private void Awake()
    {
        terrainPrefabs = new Dictionary<terrainType, GameObject>
        {
            { terrainType.water, water },
            { terrainType.forest, forest },
            { terrainType.mountain, mountain },
            { terrainType.boatCleaner, boatCleaner },
            { terrainType.riverZ, riverZ },
            { terrainType.riverX, riverX },
            { terrainType.riverXZ, riverXZ },
            { terrainType.riverZX, riverZX }
        };
    }

    public void SetCellPrefab(terrainType type)
    {
        if (terrainPrefabs.TryGetValue(type, out GameObject prefab))
        {
            cellPrefab = prefab;
        }
        else
        {
            Debug.LogError("Invalid terrain type");
        }
    }

    void Start()
    {
        // Debug.Log("Cell created" + " " + position + " " + cellPrefab.name);
        var newCellPrefab = Instantiate(cellPrefab, this.transform.position, Quaternion.identity);
        newCellPrefab.transform.parent = this.transform;
    }
}
