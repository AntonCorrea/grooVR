using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    public int count = 50;
    public float spacing = 1.0f;
    public float lineLength = 5.0f;
    public Material lineMaterial;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject lineObj = new GameObject($"Line_{i}");
            lineObj.transform.parent = this.transform;

            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.forward * lineLength);

            lr.widthMultiplier = 0.05f;
            lr.material = lineMaterial != null ? lineMaterial : new Material(Shader.Find("Sprites/Default"));
            lr.startColor = lr.endColor = Color.white;
            lr.useWorldSpace = false;

            lineObj.transform.localPosition = new Vector3(i * spacing, 0, 0);
        }
    }
}

