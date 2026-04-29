using System.Linq;
using TMPro;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public EnviromentInstance currentEnviromentInstance;

    public EnviromentInstance[] enviroments;
   
    public void SpawnEnviroment(string enviromentName)
    {
        ClearController();

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
    }

    public void StartTutorial(string name)
    {
        NPCSequence sequence;
        if (string.IsNullOrEmpty(name))
        {
            sequence = currentEnviromentInstance.NPCSequenceList[0];
        }
        else
        {
            sequence = currentEnviromentInstance.NPCSequenceList.FirstOrDefault(i => i.sequenceName == name);
        }
        
        sequence.runner = GameManager.Instance.NPC.sequenceRunner;
        sequence.StartTutorial();
    }

    public void ClearController()
    {
        if (currentEnviromentInstance != null)
        {
            Destroy(currentEnviromentInstance.gameObject);
        }
    }
}
