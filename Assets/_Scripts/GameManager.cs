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

    public GameObject[] objectsForSpawnOnTable;
    public GameObject currentObjectOnTable;

    public Sphere360Video currentSphere360Video;

    public string StartOpenMenu;
    public string StartEnvirment;
    public bool tutorial = false;

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
            enviromentController.SpawnEnviroment("EnviromentTutorial");

        }
        else
        {
            handMenu.menuController.OpenMenu(StartOpenMenu);
            enviromentController.SpawnEnviroment(StartEnvirment);

            handGesturesController.rightHandActive.AddListener(() => handMenu.Show());
            handGesturesController.rightHandDeactive.AddListener(() => handMenu.Hide());

            handGesturesController.leftHandActive.AddListener(() => teleporter.StartTeleport());
            handGesturesController.leftHandDeactive.AddListener(() => teleporter.CancelTeleport());
        }

        
    }

    //public void ShowHandMenu()
    //{
    //    handMenu.Show();
    //}

    //public void HideHandMenu()
    //{
    //    handMenu.Hide();
    //}

    //public void StartTeleport()
    //{
    //    teleporter.StartTeleport();
    //}

    //public void CancelTeleport()
    //{
    //    teleporter.CancelTeleport();
    //}

    public IEnumerator InvokeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionDelay.Invoke();
    }

    public void SpawnOnTable(string newObject)
    {
        if (currentObjectOnTable != null)
            Destroy(currentObjectOnTable);

        GameObject newGameObject = objectsForSpawnOnTable.FirstOrDefault(i => i.name == newObject);
        currentObjectOnTable = Instantiate(newGameObject, enviromentController.currentEnviromentInstance.tableSpawnPoint.transform);

    }

    //public void SkipTutorial()
    //{
    //    xBotEnv.SetActive(false);
    //    xBotController.gameObject.SetActive(false);
    //    enviroment.SpawnEnviroment("Grid");
    //    isHandMenuActive = true;
    //    handMenu.menuController.OpenMenu("Main");
    //    teleporter.onlyUseTeleportPoints = false;
    //    Instance.isTeleporterActive = true;
    //}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DestroyObjectOnTable()
    {
        if (currentObjectOnTable != null)
            Destroy(currentObjectOnTable);
    }

    public void ResetPlayerPositionInEnviroment()
    {
        playerBody.SetPosition(enviromentController.currentEnviromentInstance.spawnPlayerPoint.transform.position, enviromentController.currentEnviromentInstance.spawnPlayerPoint.transform.rotation);
        handMenu.flexibleFollower.SetInstantPosition();
    }

    public void LoadVideo(int index)
    {
        currentSphere360Video.PlayVideo(index);
    }

    public void SpawnEnviroment(string name)
    {
        enviromentController.SpawnEnviroment(name);
        ResetPlayerPositionInEnviroment();
    }




}