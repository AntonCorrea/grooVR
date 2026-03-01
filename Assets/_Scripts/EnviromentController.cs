using System.Linq;
using TMPro;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public EnviromentInstance currentEnviromentInstance;

    public EnviromentInstance[] enviroments;

    public string currentEnviromentName;
    public TextMeshProUGUI currentEnviromentText;

    private void Start()
    {
        AssignEnviromentName(currentEnviromentName);
    }

    public void AssignEnviromentName(string name)
    {
        currentEnviromentName = name;
        currentEnviromentText.text = name;
    }
    
    public void SpawnEnviroment(string name="")
    {
        if(currentEnviromentInstance != null)
        {
            Destroy(currentEnviromentInstance.gameObject);
        }

        EnviromentInstance newEnviroment;
        if (name == string.Empty)
        {
            newEnviroment = enviroments.FirstOrDefault(i => i.enviromentName == currentEnviromentName);
        }
        else
        {
            newEnviroment = enviroments.FirstOrDefault(i => i.enviromentName == name);
        }
        

        currentEnviromentInstance = Instantiate(newEnviroment, transform);

        if(currentEnviromentInstance.skyBox != null)
        {
            RenderSettings.skybox = currentEnviromentInstance.skyBox;
        }
        else
        {
            RenderSettings.skybox = null;
        }
    }
}
