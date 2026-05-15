using Autohand;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AutoHandPlayer playerBody;

    public Action actionDelay;

    public HandMenu handMenu;

    public Teleporter teleporter;

    public EnviromentController enviromentController;

    public VehicleController vehicleController;

    public VisorController visorController;

    public HandGesturesController handGesturesController;

    public NPCController NPCControllerPrefab;
    public NPCController NPC;

    public Sphere360Video currentSphere360Video;

    public string StartOpenMenu;
    public string StartEnvirment;
    public bool tutorial = false;
    public string StartTutotial;
    public bool rightHandActive = true;
    public bool leftHandActive = true;
    void Awake()
    {
        playerBody = AutoHandExtensions.CanFindObjectOfType<AutoHandPlayer>();

        // Check if another instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;

    }

    private void Start()
    {
        if (tutorial)
        {
            handMenu.menuController.OpenMenu("MainTuto");
            SpawnEnviromentAndTutorial(StartTutotial);
        }
        else
        {
            SkipTutorial();
        }        
    }

    public void SkipTutorial()
    {
        handMenu.menuController.OpenMenu(StartOpenMenu);
        enviromentController.SpawnEnviroment(StartEnvirment);

        if (rightHandActive)
        {
            handGesturesController.leftHandActive.AddListener(() => handMenu.Show());
            handGesturesController.leftHandDeactive.AddListener(() => handMenu.Hide());
        }
        if (leftHandActive)
        {
            handGesturesController.rightHandActive.AddListener(() => teleporter.StartTeleport());
            handGesturesController.rightHandDeactive.AddListener(() => teleporter.CancelTeleport());
        }

    }

    //public IEnumerator InvokeWithDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    actionDelay.Invoke();
    //}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPlayerPositionInEnviroment()
    {
        playerBody.SetPosition(enviromentController.currentEnviromentInstance.spawnPlayerPoint.transform.position, enviromentController.currentEnviromentInstance.spawnPlayerPoint.transform.rotation);
        handMenu.flexibleFollower.SetInstantPosition();
    }

    public void Load360Video(int index)
    {
        SpawnEnviroment("Sphere360");
        currentSphere360Video.PlayVideo(index);
    }

    public void LoadFlatVideo(int index)
    {
        SpawnEnviroment("QuadVideo");
        currentSphere360Video.PlayVideo(index);
    }

    void ClearControllers()
    {
        enviromentController.ClearController();
        vehicleController.ClearController();
        visorController.ClearController();

        if (NPC)
        {
            Destroy(NPC.gameObject);
        }
    }

    public void SpawnEnviroment(string name)
    {
        ClearControllers();
        enviromentController.SpawnEnviroment(name);
        ResetPlayerPositionInEnviroment();
    }



    public void SpawnVehicleSimulator()
    {
        SpawnEnviroment(vehicleController.currentVehicleEnviroment);
        vehicleController.SpawnVehicle();
    }

    public void SpawnVisorModel(string modelName)
    {
        enviromentController.SpawnEnviroment("Empty");
        visorController.SpawnVisorModel(modelName);
        handMenu.menuController.OpenMenu("VisorMenu");
    }

    public void SpawnTraining(string tutorial)
    {
        NPC = Instantiate(NPCControllerPrefab,enviromentController.currentEnviromentInstance.spawnNPCPoint);
        NPC.LoadSpeakList(enviromentController.currentEnviromentInstance.speakActionElementsObject);
        NPC.LoadMoveList(enviromentController.currentEnviromentInstance.moveActionElementsObject);
        enviromentController.StartTutorial(tutorial);
    }

    public void SpawnEnviromentAndTutorial(string enviroment, string tutorial = "")
    {
        SpawnEnviroment(enviroment);
        SpawnTraining(tutorial);
    }

}