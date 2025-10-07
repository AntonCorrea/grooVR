using UnityEngine;

public class CubeMatrixController : MonoBehaviour
{
    [Header("Matrix Settings")]
    public int matrixSize = 5;
    public float spacing = 2f;
    public GameObject cubePrefab;

    [Header("Movement Settings")]
    public Vector3 moveDirection = new Vector3(1, 0, 0);
    public float moveSpeed = 1f;
    public float appearDistance = -5f;
    public float disappearDistance = 10f;
    public float zoomDuration = 0.2f;

    [Header("Materials")]
    public Material[] materials;

    private GameObject[,] cubes;
    private Vector3[,] initialPositions;
    private float[,] timers;
    private float[,] offsets;

    private bool isPlaying = false;
    private bool isInitialized = false;

    // --- PUBLIC API ---

    /// <summary>
    /// Initializes and starts the cube animation effect.
    /// </summary>
    [ContextMenu("StartEffect")]
    public void StartEffect()
    {
        if (!isInitialized)
            InitEffect();

        ResetTimers();
        isPlaying = true;
    }

    /// <summary>
    /// Stops the cube animation and optionally destroys the cubes.
    /// </summary>
    [ContextMenu("StopEffect")]
    public void EndEffect()
    {
        isPlaying = false;

        //if (destroyCubes)
        //{
        ClearCubes();
        isInitialized = false;
        //}
    }

    // --- CORE LOGIC ---

    private void InitEffect()
    {
        cubes = new GameObject[matrixSize, matrixSize];
        initialPositions = new Vector3[matrixSize, matrixSize];
        timers = new float[matrixSize, matrixSize];
        offsets = new float[matrixSize, matrixSize];

        for (int x = 0; x < matrixSize; x++)
        {
            for (int z = 0; z < matrixSize; z++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, z * spacing);
                GameObject cube = Instantiate(cubePrefab, transform);
                cube.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                cube.transform.localPosition = pos;

                cubes[x, z] = cube;
                initialPositions[x, z] = pos;

                timers[x, z] = Random.value;
                offsets[x, z] = Random.Range(0.8f, 1.2f);
            }
        }

        isInitialized = true;
    }

    private void ResetTimers()
    {
        for (int x = 0; x < matrixSize; x++)
        {
            for (int z = 0; z < matrixSize; z++)
            {
                timers[x, z] = Random.value;
            }
        }
    }

    private void ClearCubes()
    {
        if (cubes == null) return;

        foreach (var cube in cubes)
        {
            if (cube != null)
                Destroy(cube);
        }
    }

    private void Update()
    {
        if (isPlaying)
            UpdateEffect();
    }

    private void UpdateEffect()
    {
        float moveRange = disappearDistance - appearDistance;

        for (int x = 0; x < matrixSize; x++)
        {
            for (int z = 0; z < matrixSize; z++)
            {
                GameObject cube = cubes[x, z];
                float t = timers[x, z];

                // Advance timer (loop)
                t += (moveSpeed * offsets[x, z] * Time.deltaTime) / moveRange;
                if (t > 1f) t -= 1f;
                timers[x, z] = t;

                // Move cube
                Vector3 basePos = initialPositions[x, z];
                Vector3 pos = basePos + moveDirection.normalized * Mathf.Lerp(appearDistance, disappearDistance, t);
                cube.transform.localPosition = pos;

                // Scale logic
                float scale = 1f;
                float edgeFraction = zoomDuration / moveRange;

                if (t < edgeFraction)
                {
                    float progress = Mathf.InverseLerp(0f, edgeFraction, t);
                    scale = Mathf.Lerp(0f, 1f, progress);
                }
                else if (t > 1f - edgeFraction)
                {
                    float progress = Mathf.InverseLerp(1f - edgeFraction, 1f, t);
                    scale = Mathf.Lerp(1f, 0f, progress);

                    cube.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                }

                cube.transform.localScale = Vector3.one * scale;
            }
        }
    }
}
