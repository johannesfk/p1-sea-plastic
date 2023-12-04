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
        riverNW
    };

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    private InputAction leftClickAction;


    public HexCell[] cells { get; private set; }

    Canvas gridCanvas;
    HexMesh hexMesh;

    HexSpawnPrefab hexSpawner;


    void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        hexSpawner = GetComponentInChildren<HexSpawnPrefab>();

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
        cell.color = defaultColor;
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

        if (cell.region != 0)
        {
            TMP_Text regionlabel = Instantiate<TMP_Text>(cellLabelPrefab);
            regionlabel.rectTransform.SetParent(gridCanvas.transform, false);
            regionlabel.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            regionlabel.fontSize = 10;
            regionlabel.text = cell.region.ToString();
            regionlabel.color = Color.red;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left click");
            HandleInput();
        }
    }

    void HandleInput()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Ray inputRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        Debug.Log("Mouse position " + Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Hit at " + hit.point);
            Debug.DrawRay(inputRay.origin, inputRay.direction * hit.distance, Color.yellow);
            TouchCell(hit.point);
        }
        else
        {
            Debug.DrawRay(inputRay.origin, inputRay.direction * 1000, Color.white);
            Debug.Log("No hit");
        }
    }

    void TouchCell(Vector3 position)
    {
        Debug.Log("touched position " + position + " -> " + transform.InverseTransformPoint(position));
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int coordinateZ = coordinates.Z * -1;
        int index = coordinates.X + (coordinateZ * width) + coordinates.Z / 2; /*  + (coordinates.Z * -1 / 2) */
        // Debug.Log("touched index " + "(" + "X: " + (coordinateZ) + "*" + "W: " + width + " -> " + "X: " + coordinates.X + "+" + "Z*W:" + (coordinateZ * width) + "+ Z/2: " + (coordinateZ / 2) + " -> " + "i: " + index);
        Debug.Log("touched at " + coordinates.ToString() + " -> " + index);
        if (index >= 0 && index < cells.Length)
        {
            HexCell cell = cells[index];
            cell.color = touchedColor;
            Debug.Log("Touched region " + cell.region);
            if (cell.terrainType == terrainType.water)
            {
                cell.SetCellType(terrainType.boatCleaner);
            }
            else
            {
                cell.SetCellType(terrainType.recycler);
            }

            Debug.Log("Touched cell position " + cell.transform.position);

            for (int i = 0; i < cell.neighbors.Length; i++)
            {
                HexCell neighbor = cell.neighbors[i];
                if (neighbor != null)
                {
                    // Debug.Log("Neighbor " + ((HexDirection)i).ToString() + neighbor.coordinates.ToString());

                    // Debug directions
                    /* TMP_Text neighborlabel = Instantiate<TMP_Text>(cellLabelPrefab);
                    neighborlabel.rectTransform.SetParent(gridCanvas.transform, false);
                    neighborlabel.rectTransform.anchoredPosition =
                        new Vector2(neighbor.position.x, neighbor.position.z);
                    neighborlabel.fontSize = 5;
                    neighborlabel.color = Color.yellow;
                    neighborlabel.text = ((HexDirection)i).ToString() + "\n" + index.ToString(); */
                }
            }


            /// TODO: Disabled until implemented
            /// Should probably not be "new" but rather
            /// a reference to the existing controller.
            /* PollutionController pollutionController = new()
            {
                currentRegion = cell.region
            }; */
        }
        // hexMesh.Triangulate(cells);
        Debug.Log("Cells count " + cells.Length);
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
