using Autohand;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


    [ContextMenu("OnBtnStart")]
    public void OnBtnStart()
    {
        enviroment.stand.gameObject.SetActive(false);
        //enviroment.LoadEnviroment(3);
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

    public void LoadEnviroment(int index)
    {
        if (currentObjectOnTable != null)
            Destroy(currentObjectOnTable);

        enviroment.LoadEnviroment(index);
        playerBody.SetPosition(enviroment.currentEnviroment.spawnPlayerPoint.transform.position);
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
}