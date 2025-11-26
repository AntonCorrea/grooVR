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

    public XBotController xbot;
    public HandMenu handMenu;
    public Teleporter teleporter;
    public EnviromentController enviroment;

    public bool isHandMenuActive = false;
    public bool isTeleporterActive = false;

    public bool isFirtTimeTeleporter = true;

    public GameObject[] objectsForSpawnOnTable;
    public GameObject currentObjectOnTable;

    public VehicleController[] vehicles;
    public VehicleController currentVehicle;

    bool isDriving = false;
    public Camera mainPlayerCam;
    public Camera vehicleCam;
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
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Submit"))
        //{
        //    EnterHummer();
        //}
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    ExitHummer();
        //}

        if (Input.GetButtonDown("JoySubmit") || Input.GetButtonDown("Submit"))
        {
            ToogleVehicle();
        }
    }


    [ContextMenu("OnBtnStart")]
    public void OnBtnStart()
    {
        enviroment.stand.gameObject.SetActive(false);
        enviroment.LoadEnviroment(3);
        enviroment.currentEnviroment.table.SetActive(false);
        xbot.SetActions(xBotActions.waitToMoveToGreet);
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
        xbot.SetActions(xBotActions.talkGotoAjustes);
    }

    public void HandMenuFinishedTuto()
    {
        handMenu.isTutoFinished = true;
    }

    [ContextMenu("StartTeleportTuto")]
    public void CheerFinishMenuTuto()
    {
        xbot.SetActions(xBotActions.cheersFinishMenuTuto);
    }

    public void TeleportTutoFirstTime()
    {
        xbot.SetActions(xBotActions.explainTeleport_1);
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
        enviroment.stand.gameObject.SetActive(false);
        xbot.gameObject.SetActive(false);
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