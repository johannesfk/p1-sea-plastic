using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static HexGrid;

public class HexInteraction : MonoBehaviour
{
    public static HexInteraction instance;

    public delegate void CellTypePlacedHandler();
    public event CellTypePlacedHandler OnCellTypePlaced;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }
    public bool uiActive = false;

    private Structures nextStructure;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !uiActive)
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
        int index = coordinates.X + (coordinateZ * HexGrid.instance.width) + coordinates.Z / 2; /*  + (coordinates.Z * -1 / 2) */
        // Debug.Log("touched index " + "(" + "X: " + (coordinateZ) + "*" + "W: " + width + " -> " + "X: " + coordinates.X + "+" + "Z*W:" + (coordinateZ * width) + "+ Z/2: " + (coordinateZ / 2) + " -> " + "i: " + index);
        Debug.Log("touched at " + coordinates.ToString() + " -> " + index);
        if (index >= 0 && index < HexGrid.instance.cells.Length)
        {
            HexCell cell = HexGrid.instance.cells[index];
            Debug.Log("Touched region " + cell.region);

            /// TODO: Don't allow to build next to cell with same type
            if (cell.terrainType == terrainType.water || cell.terrainType == terrainType.contaminatedWater)
            {
                if (cell.terrainType == terrainType.contaminatedWater)
                {
                    WaterContamination.Instance.contaminatedCells.Remove(cell);
                }
                cell.SetCellType(terrainType.boatCleaner);
            }
            else if (
                cell.terrainType == terrainType.plains ||
                cell.terrainType == terrainType.forest ||
                cell.terrainType == terrainType.desert)
            {
                cell.SetCellType(terrainType.incinerator); // TODO: Change to chosen type
                OnCellTypePlaced?.Invoke();

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
        }
    }

    public void PlaceCellType(Structures type)
    {
        // Not yet implemented

        Debug.Log("Placing " + type);
        // hexSpawner.SpawnPrefab(type);

        throw new NotImplementedException();
    }
}
