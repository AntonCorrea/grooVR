using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public GameObject currentEnviroment;

    public GameObject[] enviroments;

    public Collider floorCollider;

    public GameObject stand;
    
    public void LoadEnviroment(int i)
    {
        if(currentEnviroment != null)
        {
            GameObject.Destroy(currentEnviroment);
        }

        currentEnviroment = Instantiate(enviroments[i], transform);
    }
}
