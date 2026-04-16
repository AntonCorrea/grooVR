using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    public int segments = 50;     // Number of points
    public float radius = 5f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1; // +1 to close the circle
        line.useWorldSpace = false;

        CreateCircle();
    }

    void CreateCircle()
    {
        float angle = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, 0, y));

            angle += 360f / segments;
        }
    }
}