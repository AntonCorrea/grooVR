using TMPro;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Transform modelPlace;
    public GameObject modelPrefab;
    public GameObject model;
    public GameObject defaultModel;

    public void SetModel(GameObject m)
    {
        if (m)
        {
            model = Instantiate(modelPrefab, modelPlace);
        }
        else
        {
            model = Instantiate(defaultModel, modelPlace);
        }
        
    }
}
