using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Box size of each block area")]
    public Vector3 blockSize = new Vector3(1, 1, 1);

    [Header("Gizmo Color")]
    public Color gizmoColor = Color.red;

    private List<Vector3> blockedPositions = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddBlock(Vector3 pos)
    {
        if (!blockedPositions.Contains(pos))
            blockedPositions.Add(pos);
    }

    public void RemoveBlock(Vector3 pos)
    {
        blockedPositions.Remove(pos);
    }

    // -----------------------------
    // BOX COLLISION CHECK
    // -----------------------------
    public bool IsBlocked(Vector3 targetPos)
    {
        foreach (var block in blockedPositions)
        {
            Bounds b = new Bounds(block, blockSize);

            if (b.Contains(targetPos))
                return true;
        }

        return false;
    }

    // -----------------------------
    // GIZMOS (CUBE BLOCKS)
    // -----------------------------
    private void OnDrawGizmos()
    {
        if (blockedPositions == null) return;

        Gizmos.color = gizmoColor;

        foreach (var block in blockedPositions)
        {
            Gizmos.DrawWireCube(block, blockSize);
        }
    }
}