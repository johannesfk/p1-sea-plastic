using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.UIElements;

public class HexGrid : MonoBehaviour
{
    public static HexGrid instance;

    public int width;
    public int height;
    public HexCell cellPrefab;
    public TMP_Text cellLabelPrefab;
    public enum terrainType
    {
        forest,
        plains,
        mountain,
        snow,
        desert,
        recycler,
        incinerator,
        landfill,
        water,
        contaminatedWater,
        boatCleaner,
        riverBarricade,
        riverWE,
        riverNS,
        riverNE,
        riverNW,
        artic
    };
    

    public HexCell[] cells { get; private set; }

    Canvas gridCanvas;

    // HexSpawnPrefab hexSpawner;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        // hexSpawner = GetComponentInChildren<HexSpawnPrefab>();

        // gridCanvas.gameObject.SetActive(false);

        List<Map> mapList = Maps.instance.mapList;

        if (mapList.Count > 0)
        {
            Map map1 = mapList[0];

            height = map1.layout.GetLength(0);
            width = map1.layout.GetLength(1);
            cells = new HexCell[map1.layout.Length];

            Debug.Log("Map size: " + height + " ✕ " + width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    terrainType terrainType = map1.layout[i, j].terrainType;
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Debug.Log("Cell " + i + " " + j + " " + map1.layout[i, j]);
                    int index = i * width + j;
                    HexCell cell = cells[index] = Instantiate<HexCell>(cellPrefab);
                    cell.terrainType = map1.layout[i, j].terrainType;
                    // cell.SetCellType(map1.layout[i, j].terrainType);
                    cell.region = map1.layout[i, j].region;
                }
            }

            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }

            }
        }
        else
        {
            Debug.LogError("No maps found");
        }
    }

    void CreateCell(int x, int z, int i)
    {
        z = z * -1;
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f); // * -1 to flip the map top to bottom

        // HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        HexCell cell = cells[i];
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.index = i;
        cell.position = position;

        // Set neighboring cells

        /// If the cell is not on the leftmost column,
        /// set the cell to the west as the previous cell in the array.
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        // If the cell is not on the first row
        if (-z > 0)
        {
            /// If the cell is on an even row set the cell to
            /// the north west as the cell at index - width of the map.
            /// Uses bitwise AND to check if the cell is on an even row.
            if ((-z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.NW, cells[i - width]);
                /// If the cell is not on the rightmost column set the cell to
                /// the north east as the cell at index - width of the map + 1.
                if (x < width - 1)
                {
                    cell.SetNeighbor(HexDirection.NE, cells[i - width + 1]);
                }
            }
            /// If the cell is on an odd row set the cell to 
            /// the north west as the cell at index - width of the map - 1.
            else
            {
                cell.SetNeighbor(HexDirection.NE, cells[i - width]);
                /// If the cell is not on the rightmost column set the cell to
                /// the north east as the cell at index - width of the map.
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.NW, cells[i - width - 1]);
                }
            }
        }


        // Cell Coordinates as label for debugging
        /* TMP_Text label = Instantiate<TMP_Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines(); */

        /* if (cell.region != 0)
        {
            TMP_Text regionlabel = Instantiate<TMP_Text>(cellLabelPrefab);
            regionlabel.rectTransform.SetParent(gridCanvas.transform, false);
            regionlabel.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            regionlabel.fontSize = 10;
            regionlabel.text = cell.region.ToString();
            regionlabel.color = Color.red;
        } */


    }

    public void OnDestroy()
    {
        // leftClickAction.Disable();
    }
    public IEnumerator WaitForCells()
    {
        while (cells == null || cells.Length == 0)
        {
            yield return null;
        }
    }
}
