using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public int gridSize = 10;           // number of cells across
    public float cellSize = 1f;         // size of each cell
    public float baseSpeed = 5f;        // base expansion speed for center lines
    public float speedFalloff = 0.2f;   // how much slower outer lines get
    public float lineWidth = 0.05f;

    private Coroutine expandRoutine;

    [ContextMenu("StartGrid")]
    public void StartGrid()
    {
        if (expandRoutine != null)
            StopCoroutine(expandRoutine);

        expandRoutine = StartCoroutine(ExpandGrid());
    }

    private IEnumerator ExpandGrid()
    {
        // clear old lines
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float halfGrid = gridSize * cellSize * 0.5f;

        // store all line data
        List<LineData> lines = new List<LineData>();

        for (int i = -gridSize / 2; i <= gridSize / 2; i++)
        {
            float pos = i * cellSize;

            // vertical lines (along local Z)
            lines.Add(CreateLineData(new Vector3(pos, 0, 0), Vector3.forward, Mathf.Abs(pos)));

            // horizontal lines (along local X)
            lines.Add(CreateLineData(new Vector3(0, 0, pos), Vector3.right, Mathf.Abs(pos)));
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

                // Now world-space positions are computed once using parent transform
                Vector3 start = line.worldCenter - line.worldDirection * halfLen;
                Vector3 end = line.worldCenter + line.worldDirection * halfLen;

                line.renderer.SetPosition(0, start);
                line.renderer.SetPosition(1, end);
            }
            yield return null;
        }
    }

    private LineData CreateLineData(Vector3 localCenter, Vector3 localDir, float distance)
    {
        // Convert local coords into world coords (bake parent position & rotation)
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Vector3 worldDir = transform.TransformDirection(localDir);

        GameObject lineObj = new GameObject("Line");
        lineObj.transform.parent = transform;
        //lineObj.transform.parent = null; // 🔹 detach so it no longer follows parent

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = false; // 🔹 keep in world space
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        return new LineData
        {
            worldCenter = worldCenter,
            worldDirection = worldDir.normalized,
            distanceFromCenter = distance,
            renderer = lr,
            currentLength = 0f,
            targetLength = (gridSize * cellSize) * 0.5f
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

        float halfGrid = (gridSize * cellSize) * 0.5f;

        // local corners of the square
        Vector3[] corners = new Vector3[4];
        corners[0] = new Vector3(-halfGrid, 0, -halfGrid);
        corners[1] = new Vector3(-halfGrid, 0, halfGrid);
        corners[2] = new Vector3(halfGrid, 0, halfGrid);
        corners[3] = new Vector3(halfGrid, 0, -halfGrid);

        // transform to world space
        for (int i = 0; i < 4; i++)
            corners[i] = transform.TransformPoint(corners[i]);

        // draw outline
        Gizmos.DrawLine(corners[0], corners[1]);
        Gizmos.DrawLine(corners[1], corners[2]);
        Gizmos.DrawLine(corners[2], corners[3]);
        Gizmos.DrawLine(corners[3], corners[0]);
    }
}
