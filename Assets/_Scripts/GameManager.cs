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

    //public bool skipTuto = false;

    public XBotController xBotController;
    public GameObject xBotEnv;
    public GameObject stand;

    public HandMenu handMenu;

    public Teleporter teleporter;

    public EnviromentController enviromentController;

    public VehicleController vehicleController;



    public bool isHandMenuActive = false;
    public bool isTeleporterActive = false;

    public bool isFirtTimeTeleporter = false;

    public GameObject[] objectsForSpawnOnTable;
    public GameObject currentObjectOnTable;

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


        handMenu.menuController.OpenMenu("Main");
        enviromentController.SpawnEnviroment("Grid");

        //DontDestroyOnLoad(gameObject); // Persist between scenes
        //if (skipTuto)
        //{
        //    SkipTutorial();
        //}
        //else
        //{
        //    handMenu.menuController.OpenMenu("grooVR Simulaciones (TUTOMENU)");
        //}

    }


    [ContextMenu("OnBtnStart")]
    public void OnBtnStart()
    {
        stand.SetActive(false);
        SpawnEnviroment("Grid");
        enviromentController.currentEnviromentInstance.table.SetActive(false);
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
        playerBody.SetPosition(enviromentController.currentEnviromentInstance.spawnPlayerPoint.transform.position);
    }

    public void LoadVideo(int index)
    {
        currentSphere360Video.PlayVideo(index);
    }

    public void SpawnEnviroment(string name)
    {
        enviromentController.SpawnEnviroment(name);
    }




}