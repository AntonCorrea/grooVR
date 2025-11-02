using Autohand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action actionDelay;

    public XBotController xbot;
    public HandMenu handMenu;
    public Teleporter teleporter;
    public EnviromentController enviroment;

    public bool isHandMenuActive = false;
    public bool isTeleporterActive = false;

   
    void Awake()
    {
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
        enviroment.LoadEnviroment(3);
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
        enviroment.LoadEnviroment(index);
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
    public void StartTeleportTuto()
    {
        xbot.SetActions(xBotActions.cheersFinishMenuTuto);
    }
}