using System.Linq;
using TMPro;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public EnviromentInstance currentEnviromentInstance;

    public EnviromentInstance[] enviroments;
   
    public void SpawnEnviroment(string enviromentName)
    {
        if(currentEnviromentInstance != null)
        {
            Destroy(currentEnviromentInstance.gameObject);
        }

        EnviromentInstance newEnviroment;  
        newEnviroment = enviroments.FirstOrDefault(i => i.enviromentName == enviromentName);
        currentEnviromentInstance = Instantiate(newEnviroment, transform);
     
        if(currentEnviromentInstance.skyBox != null)
        {
            RenderSettings.skybox = currentEnviromentInstance.skyBox;
        }
        else
        {
            RenderSettings.skybox = null;
        }

        GameManager.Instance.ResetPlayerPositionInEnviroment();
    }
}
