using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;
    public readonly int X => x;
    public readonly int Z => z;
    public readonly int Y => -X - Z;

    public readonly float HexX => X + Z / 2 + ((Z & 1) == 0 ? 0f : 0.5f);
    public readonly float HexZ => Z * HexMetrics.outerToInner;

    public HexCoordinates(int x, int z)
    {
        if (HexMetrics.Wrapping)
        {
            Debug.Log("Wrapping hexGrid");
            /*  int oX = x + z / 2;
             if (oX < 0)
             {
                 Debug.Log("Wrapping hexGrid +x");
                 x += HexMetrics.wrapSize;
             }
             else if (oX >= HexMetrics.wrapSize)
             {
                 Debug.Log("Wrapping hexGrid -x");
                 x -= HexMetrics.wrapSize;
             } */
        }
        this.x = x;
        this.z = z;
    }

    public readonly int DistanceTo(HexCoordinates other)
    {
        int xy =
            (x < other.x ? other.x - x : x - other.x) +
            (Y < other.Y ? other.Y - Y : Y - other.Y);

        if (HexMetrics.Wrapping)
        {
            other.x += HexMetrics.wrapSize;
            int xyWrapped =
                (x < other.x ? other.x - x : x - other.x) +
                (Y < other.Y ? other.Y - Y : Y - other.Y);
            if (xyWrapped < xy)
            {
                xy = xyWrapped;
            }
            else
            {
                other.x -= 2 * HexMetrics.wrapSize;
                xyWrapped =
                    (x < other.x ? other.x - x : x - other.x) +
                    (Y < other.Y ? other.Y - Y : Y - other.Y);
                if (xyWrapped < xy)
                {
                    xy = xyWrapped;
                }
            }
        }

        return (xy + (z < other.z ? other.z - z : z - other.z)) / 2;
    }

    public readonly HexCoordinates Step(HexDirection direction) => direction switch
    {
        HexDirection.NE => new HexCoordinates(x, z + 1),
        HexDirection.E => new HexCoordinates(x + 1, z),
        HexDirection.SE => new HexCoordinates(x + 1, z - 1),
        HexDirection.SW => new HexCoordinates(x, z - 1),
        HexDirection.W => new HexCoordinates(x - 1, z),
        HexDirection.NW => new HexCoordinates(x - 1, z + 1)
    };

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / HexMetrics.innerDiameter;
        float y = -x;

        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        /// Rounding errors can cause the sum of the coordinates to drift from zero.
        /// If that happens, adjust the bottom coordinate.
        /// This will move the other two coordinates in turn,
        /// So that the sum again equals zero.
        /// This code is not perfect but works well enough for this game.
        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }
    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}