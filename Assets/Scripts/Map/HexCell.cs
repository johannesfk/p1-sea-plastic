using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HexGrid;
using static HexCoordinates;

public class HexCell : MonoBehaviour
{
    public int index;
    public int coastDistance = 0;
    public HexCoordinates coordinates;
    [SerializeField]
    [HideInInspector]
    public HexCell[] neighbors;
    public Vector3 position;
    public terrainType terrainType;
    public int region;

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
            { terrainType.recycler, recycler },
            { terrainType.incinerator, incinerator },
            { terrainType.landfill, landfill },
            { terrainType.boatCleaner, boatCleaner },
            { terrainType.riverBarricade, riverBarricade },
            { terrainType.forest, forest },
            { terrainType.plains, plains },
            { terrainType.mountain, mountain },
            { terrainType.snow, snow },
            { terrainType.desert, desert },
            { terrainType.water, water },
            { terrainType.contaminatedWater, contaminatedWater },
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

    private void Start()
    {
        SetCellType(terrainType);
    }

    /* public void Refresh()
    {
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            TryGetNeighbor(d, out HexCell neighbor);
            if (neighbor != null)
            {
                Debug.Log("Neighbor " + d.ToString() + neighbor.coordinates.ToString());
                neighbors[(int)d] = GetNeighbor(d);
            }
            Debug.Log("Neighbor " + d.ToString() + " is now " + GetNeighbor(d).coordinates.ToString());
        }
    } */

    private void AddCellPrefab()
    {
        terrainType[] rotatableTerrainTypes = {
            terrainType.water,
            terrainType.boatCleaner,
            terrainType.forest,
            terrainType.plains,
            terrainType.mountain,
            terrainType.recycler,
            terrainType.incinerator,
            terrainType.landfill
        };
        // Destroy any existing cell prefabs
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var newCellPrefab = Instantiate(cellPrefab, this.transform.position, Quaternion.identity);
        newCellPrefab.transform.parent = this.transform;
        if (rotatableTerrainTypes.Contains(terrainType))
        {
            newCellPrefab.transform.Rotate(0, UnityEngine.Random.Range(0, 6) * 60, 0);
        }
    }

    public HexCell GetNeighbor(HexDirection direction) =>
        HexGrid.instance.GetCell(coordinates.Step(direction));

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        Debug.Log("Setting neighbor " + (int)direction + " to " + cell.coordinates.ToString());
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
        Debug.Log("Neighbor " + (int)direction.Opposite() + " is now ");
        if (cell.neighbors[(int)direction.Opposite()] != null)
        {
            Debug.Log(cell.neighbors[(int)direction.Opposite()].coordinates.ToString());
        }
    }

    public bool TryGetNeighbor(HexDirection direction, out HexCell cell) =>
        HexGrid.instance.TryGetCell(coordinates.Step(direction), out cell);
}
