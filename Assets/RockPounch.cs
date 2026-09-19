using UnityEngine;

public class RockPounch : MonoBehaviour
{
    [SerializeField] private string rockLayerName = "Rocks";

    private int rockLayer;

    private void Awake()
    {
        rockLayer = LayerMask.NameToLayer(rockLayerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == rockLayer)
        {
            Destroy(other.gameObject);
        }
    }
}
