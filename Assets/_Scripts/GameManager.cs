using Autohand;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AutoHandPlayer playerBody;

    public Action actionDelay;

    public bool skipTuto = false;

    public XBotController xBotController;
    public GameObject xBotEnv;
    public GameObject stand;

    public HandMenu handMenu;

    public Teleporter teleporter;
    public EnviromentController enviroment;
    public bool isHandMenuActive = false;
    public bool isTeleporterActive = false;

    public bool isFirtTimeTeleporter = false;

    public GameObject[] objectsForSpawnOnTable;
    public GameObject currentObjectOnTable;

    public VehicleController[] vehicles;
    public VehicleController currentVehicle;

    bool isDriving = false;
    public Camera mainPlayerCam;
    public Camera vehicleCam;

    public Sphere360Video currentSphere360Video;
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
        //DontDestroyOnLoad(gameObject); // Persist between scenes


        if (skipTuto)
        {
            SkipTutorial();
        }
        else
        {
            handMenu.menuController.OpenMenu("grooVR Simulaciones (TUTOMENU)");
        }

    }

    private void Update()
    {

        if (Input.GetButtonDown("JoySubmit") || Input.GetButtonDown("Submit"))
        {
            ToogleVehicle();
        }
    }


    [ContextMenu("OnBtnStart")]
    public void OnBtnStart()
    {
        stand.SetActive(false);
        enviroment.LoadEnviroment(3);
        enviroment.currentEnviroment.table.SetActive(false);
        xBotController.SetActions(xBotActions.waitToMoveToGreet);
    }

    public void ShowHandMenu()
    {
        if(isHandMenuActive)
            handMenu.Show();
    }

    public void HideHandMenu()
    {
        handMenu.Hide();
    }

    public void StartTeleport()
    {
        if(isTeleporterActive)
            teleporter.StartTeleport();

        if (isFirtTimeTeleporter)
        {
            TeleportTutoFirstTime();
            isFirtTimeTeleporter = false;
        }
    }

    public void CancelTeleport()
    {
        teleporter.CancelTeleport();
    }


    public IEnumerator InvokeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionDelay.Invoke();
    }

    public void HandMenuShowFirstTime()
    {
        xBotController.SetActions(xBotActions.talkGotoAjustes);
    }

    public void HandMenuFinishedTuto()
    {
        handMenu.isTutoFinished = true;
    }

    [ContextMenu("StartTeleportTuto")]
    public void CheerFinishMenuTuto()
    {
        xBotController.SetActions(xBotActions.cheersFinishMenuTuto);
    }

    public void TeleportTutoFirstTime()
    {
        xBotController.SetActions(xBotActions.explainTeleport_1);
    }

    public void SpawnOnTable(string newObject)
    {
        if (currentObjectOnTable != null)
            Destroy(currentObjectOnTable);

        GameObject newGameObject = objectsForSpawnOnTable.FirstOrDefault(i => i.name == newObject);
        currentObjectOnTable = Instantiate(newGameObject, enviroment.currentEnviroment.tableSpawnPoint.transform);

    }

    public void SkipTutorial()
    {
        xBotEnv.SetActive(false);
        xBotController.gameObject.SetActive(false);
        enviroment.LoadEnviroment(3);
        isHandMenuActive = true;
        handMenu.menuController.OpenMenu("grooVR Simulaciones");
        teleporter.onlyUseTeleportPoints = false;
        Instance.isTeleporterActive = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadEnviroment(int index)
    {
        if (currentObjectOnTable != null)
            Destroy(currentObjectOnTable);

        enviroment.LoadEnviroment(index);
        playerBody.SetPosition(enviroment.currentEnviroment.spawnPlayerPoint.transform.position);
    }

    public void LoadVideo(int index)
    {
        currentSphere360Video.PlayVideo(index);
    }

    public void SpawnVehicle(string vehicle)
    {
        if (enviroment.currentEnviroment != enviroment.enviroments[3])// && enviroment.currentEnviroment.vehicleSpawnPoint == null)
        {
            LoadEnviroment(4);
        }

        if (currentVehicle != null)
            Destroy(currentVehicle.gameObject);

        VehicleController newVehicle = vehicles.FirstOrDefault(i => i.name == vehicle);
        currentVehicle = Instantiate(newVehicle, enviroment.currentEnviroment.vehicleSpawnPoint.transform);
        vehicleCam = currentVehicle.carCam;
    }

    //public void EnterHummer()
    //{
    //    vehicleCam.enabled = true;
    //    mainPlayerCam.enabled = false;
    //}

    //void ExitHummer()
    //{
    //    vehicleCam.enabled = false;
    //    mainPlayerCam.enabled = true;
    //}

    void ToogleVehicle()
    {
        if (isDriving)
        {
            vehicleCam.enabled = false;
            mainPlayerCam.enabled = true;
            isDriving = false;
        }
        else
        {

            vehicleCam.enabled = true;
            mainPlayerCam.enabled = false;
            isDriving = true;
        }
    }
}