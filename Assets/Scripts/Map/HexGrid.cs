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
    public int CellCountX = 6;
    public int CellCountZ = 6;
    public HexCell cellPrefab;
    public TMP_Text cellLabelPrefab;


    public enum terrainType
    {
        forest,
        mountain,
        snow,
        desert,
        recycler,
        incinerator,
        water,
        boatCleaner,
        riverBarricade,
        riverZ,
        riverX,
        riverXZ,
        riverZX
    };

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    private InputAction leftClickAction;


    HexCell[] cells;

    Canvas gridCanvas;
    HexMesh hexMesh;

    HexSpawnPrefab hexSpawner;


    void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        hexSpawner = GetComponentInChildren<HexSpawnPrefab>();

        List<Map> mapList = Maps.instance.mapList;

        if (mapList.Count > 0)
        {
            Map map1 = mapList[1];

            int height = map1.layout.GetLength(0);
            int width = map1.layout.GetLength(1);
            cells = new HexCell[map1.layout.Length];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    terrainType terrainType = map1.layout[i, j];
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Debug.Log("Cell " + i + " " + j + " " + map1.layout[i, j]);
                    int index = i * width + j;
                    HexCell cell = cells[index] = Instantiate<HexCell>(cellPrefab);
                    cell.SetCellPrefab(map1.layout[i, j]);
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




        // InputSystem.onActionChange() += ctx => HandleInput();

        //  InputActionChange.ActionPerformed(ctx => HandleInput());

        // leftClickAction.performed += ctx => HandleInput();
        // leftClickAction.Enable();



    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f) * -1; // * -1 to flip the map top to bottom

        // HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        HexCell cell = cells[i];
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;
        cell.position = position;


        TMP_Text label = Instantiate<TMP_Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();

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
        Debug.Log("Mouse world position " + Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        // TouchCell(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Hit at " + hit.point);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            TouchCell(hit.point);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("No hit");
        }
        /*  // Does the ray intersect any objects excluding the player layer
         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
         {
             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
             Debug.Log("Did Hit");
         }
         else
         {
             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
             Debug.Log("Did not Hit");
         } */
    }

    void TouchCell(Vector3 position)
    {
        Debug.Log("touched at " + position + " -> " + transform.InverseTransformPoint(position));
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * CellCountX + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = touchedColor;
        // hexMesh.Triangulate(cells);
        Debug.Log("touched at " + coordinates.ToString() + " -> " + index);
    }

    public void OnDestroy()
    {
        // leftClickAction.Disable();
    }
}