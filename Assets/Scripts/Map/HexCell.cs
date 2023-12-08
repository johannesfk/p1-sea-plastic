using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexGrid;

public class HexCell : MonoBehaviour
{
    public int index;
    public HexCoordinates coordinates;
    [SerializeField]
    [HideInInspector]
    public HexCell[] neighbors;
    public Vector3 position;
    public terrainType terrainType;
    public int region;
    public Color color;

    public GameObject forest;
    public GameObject plains;
    public GameObject mountain;
    public GameObject snow;
    public GameObject desert;
    public GameObject recycler;
    public GameObject incinerator;
    public GameObject landfill;
    public GameObject water;
    public GameObject contaminatedWater;
    public GameObject boatCleaner;
    public GameObject riverBarricade;
    public GameObject riverWE;
    public GameObject riverNS;
    public GameObject riverNE;
    public GameObject riverNW;

    private GameObject cellPrefab;

    public Dictionary<terrainType, GameObject> terrainPrefabs;


    private void Awake()
    {
        terrainPrefabs = new Dictionary<terrainType, GameObject>
        {
            { terrainType.forest, forest },
            { terrainType.plains, plains },
            { terrainType.mountain, mountain },
            { terrainType.snow, snow },
            { terrainType.desert, desert },
            { terrainType.recycler, recycler },
            { terrainType.incinerator, incinerator },
            { terrainType.landfill, landfill },
            { terrainType.water, water },
            { terrainType.contaminatedWater, contaminatedWater },
            { terrainType.boatCleaner, boatCleaner },
            { terrainType.riverBarricade, riverBarricade },
            { terrainType.riverWE, riverWE },
            { terrainType.riverNS, riverNS },
            { terrainType.riverNE, riverNE },
            { terrainType.riverNW, riverNW },
            { terrainType.artic, snow }
        };
    }

    public void SetCellType(terrainType type)
    {
        terrainType = type;
        if (terrainPrefabs.TryGetValue(type, out GameObject prefab))
        {
            cellPrefab = prefab;
            // Debug.Log("Added Cell prefab " + cellPrefab.name);
            AddCellPrefab();
        }
        else
        {
            Debug.LogError("Invalid terrain type");
        }
    }

    void Start()
    {

        SetCellType(terrainType);
    }

    private void AddCellPrefab()
    {
        // Destroy any existing cell prefabs
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var newCellPrefab = Instantiate(cellPrefab, this.transform.position, Quaternion.identity);
        newCellPrefab.transform.parent = this.transform;
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }
    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        // Debug.Log("Setting neighbor " + (int)direction + " to " + cell.coordinates.ToString());
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}
