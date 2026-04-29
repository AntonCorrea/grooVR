using UnityEngine;

public class EnviromentInstance : MonoBehaviour
{
    public string enviromentName;
    public GameObject spawnPlayerPoint;
    public GameObject vehicleSpawnPoint;
    public Material skyBox;

    public NPCSequence[] NPCSequenceList;
    public Transform spawnNPCPoint;
    public GameObject moveActionElementsObject;
    public GameObject speakActionElementsObject; 
}
