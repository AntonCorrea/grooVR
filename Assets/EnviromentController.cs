using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public EnviromentInstance currentEnviroment;

    public EnviromentInstance[] enviroments;

    public Collider floorCollider;

    public GameObject stand;
    
    public void LoadEnviroment(int i)
    {
        if(currentEnviroment != null)
        {
            Destroy(currentEnviroment.gameObject);
        }

        currentEnviroment = Instantiate(enviroments[i], transform);
    }
}
