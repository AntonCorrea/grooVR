using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularGrid : MonoBehaviour
{
    public int gridSize = 20;
    public float cellSize = 1f;
    public float radius = 8f;

    public float lineLength = 1f; // full visible length of each line

    public float baseSpeed = 5f;
    public float speedFalloff = 0.2f;
    public float lineWidth = 0.05f;

    private Coroutine expandRoutine;

    private void Start()
    {
        StartGrid();
    }

    [ContextMenu("StartGrid")]
    public void StartGrid()
    {
        if (expandRoutine != null)
            StopCoroutine(expandRoutine);

        expandRoutine = StartCoroutine(ExpandGrid());
    }

    private IEnumerator ExpandGrid()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        List<LineData> lines = new List<LineData>();
        HashSet<string> createdLines = new HashSet<string>();

        for (int x = -gridSize / 2; x <= gridSize / 2; x++)
        {
            for (int z = -gridSize / 2; z <= gridSize / 2; z++)
            {
                Vector3 center = new Vector3(x * cellSize, 0, z * cellSize);

                // circular mask
                if (center.magnitude > radius)
                    continue;

                float dist = center.magnitude;

                // 4 edges per cell (deduplicated)
                TryAddLine(lines, createdLines,
                    center + new Vector3(-cellSize * 0.5f, 0, 0),
                    Vector3.forward,
                    dist);

                TryAddLine(lines, createdLines,
                    center + new Vector3(cellSize * 0.5f, 0, 0),
                    Vector3.forward,
                    dist);

                TryAddLine(lines, createdLines,
                    center + new Vector3(0, 0, -cellSize * 0.5f),
                    Vector3.right,
                    dist);

                TryAddLine(lines, createdLines,
                    center + new Vector3(0, 0, cellSize * 0.5f),
                    Vector3.right,
                    dist);
            }
        }

        bool done = false;

        while (!done)
        {
            done = true;

            foreach (var line in lines)
            {
                float speed = baseSpeed / (1 + line.distanceFromCenter * speedFalloff);
                line.currentLength += speed * Time.deltaTime;

                if (line.currentLength < line.targetLength)
                    done = false;

                float halfLen = Mathf.Min(line.currentLength, line.targetLength);

                Vector3 start = line.worldCenter - line.worldDirection * halfLen;
                Vector3 end = line.worldCenter + line.worldDirection * halfLen;

                line.renderer.SetPosition(0, start);
                line.renderer.SetPosition(1, end);
            }

            yield return null;
        }
    }

    private void TryAddLine(List<LineData> lines, HashSet<string> set, Vector3 center, Vector3 dir, float dist)
    {
        string key = $"{Mathf.Round(center.x * 100)}_{Mathf.Round(center.z * 100)}_{dir.x}_{dir.z}";

        if (set.Contains(key))
            return;

        set.Add(key);
        lines.Add(CreateLineData(center, dir, dist));
    }

    private LineData CreateLineData(Vector3 localCenter, Vector3 localDir, float distance)
    {
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Vector3 worldDir = transform.TransformDirection(localDir);

        GameObject lineObj = new GameObject("Line");
        lineObj.transform.parent = transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        // clamp so it doesn't exceed the cell size
        float halfTarget = Mathf.Min(lineLength * 0.5f, cellSize * 0.5f);

        return new LineData
        {
            worldCenter = worldCenter,
            worldDirection = worldDir.normalized,
            distanceFromCenter = distance,
            renderer = lr,
            currentLength = 0f,
            targetLength = halfTarget
        };
    }

    private class LineData
    {
        public Vector3 worldCenter;
        public Vector3 worldDirection;
        public float distanceFromCenter;
        public LineRenderer renderer;
        public float currentLength;
        public float targetLength;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}