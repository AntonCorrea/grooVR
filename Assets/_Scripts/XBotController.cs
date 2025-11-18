using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum xBotActions
{
    idle, greet, waitToMoveToGreet, moveToGreet, showLeftHand, talkGotoAjustes,talkGotoEntornos, talkGotoTaller, finishMenuTuto,
    cheersFinishMenuTuto, talkStartTeleportTuto, showRightHand, moveToShowRightHand, explainTeleport_1, explainTeleport_2,
    cheersFinishTeleportTuto, moveToSideTable, moveToFrontTable, talkOpenJenga, finishOpenJenga, cheersFinishAll,
    sayGoodBye
}

public class XBotController : MonoBehaviour
{

    Animator animator;
    NavMeshAgent agent;
    DialogueSystem dialogueSystem;
    public Transform[] waypoints;
    private Action onDestinationComplete;
    private Action onTimePassed;
    [Header("Settings")]
    public float waypointThreshold = 0.1f;

    private int currentWaypoint = 0;
    private bool moveAgent = false;
    private bool talking = false;

    public CubeMatrixController matrix;
    public GameObject teleportLine;
    public Autohand.Demo.TeleportPoint teleportPoint;
    private bool hasTeleportedFirstTime = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        dialogueSystem = GetComponent<DialogueSystem>();
        SetActions(xBotActions.idle);
        Invoke("StartDialogue", 1f);//hay q inicializar el dialogo aqui, no recuerdo xq
    }

    void StartDialogue()
    {
        Talk("Presiona el boton para iniciar la experiencia", 0);
    }

    void Greet()
    {
        SetActions(xBotActions.greet);
    }

    public void TalkEntornos()
    {
        SetActions(xBotActions.talkGotoEntornos);
    }

    public void TalkTaller()
    {
        SetActions(xBotActions.talkGotoTaller);
    }

    public void FinishMenuTuto()
    {
        SetActions(xBotActions.finishMenuTuto);
    }

    public void FinishTutoJenga()
    {
        SetActions(xBotActions.finishOpenJenga);
    }


    void Update()
    {
        if (moveAgent)
        {
            HandleMovement();
        }

        UpdateAnimations();
    }

    // === Movement ===
    private void HandleMovement()
    {
        if (waypoints.Length == 0) return;

        agent.isStopped = false;
        agent.SetDestination(waypoints[currentWaypoint].position);

        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);

        if (distance < waypointThreshold)
        {
            OnDestination();
            transform.rotation = Quaternion.LookRotation(waypoints[currentWaypoint].forward);
        }
    }

    private void UpdateAnimations()
    {
        bool isWalking = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isTalking", talking);
    }

    // === Dialogue ===
    public void Talk(string dialogueId, int voiceIndex)
    {
        talking = true;
        dialogueSystem.StartDialogue(dialogueId, voiceIndex);
    }

    public void StopTalking()
    {
        talking = false;
        dialogueSystem.StopDialogue();
    }


    // === Guiding / Stages ===

    public void SetActions(xBotActions action)
    {
        print("perfom action: "+action);
        switch (action)
        {
            case xBotActions.idle:                
                animator.Play("Idle");
                break;
            case xBotActions.greet:
                animator.Play("Greet");
                Talk("Hola,me llamo X.\nVoy a ser tu guia en esta experiencia VR.", 1);
                onTimePassed = () => SetActions(xBotActions.showLeftHand);
                _ = StartCoroutine(InvokeWithDelay(4f));
                break;
            case xBotActions.waitToMoveToGreet:
                animator.Play("Idle");
                onTimePassed = () => SetActions(xBotActions.moveToGreet);
                _ = StartCoroutine(InvokeWithDelay(4f));
                break;
            case xBotActions.moveToGreet:
                animator.Play("Idle");
                moveAgent = true;
                currentWaypoint = 0;
                onDestinationComplete = Greet;
                break;
            case xBotActions.showLeftHand:
                animator.SetTrigger("HandUp_Left");               
                Talk("Levanta la mano izquierda, palma arriba, como lo hago, para activar el menú.", 2);
                matrix.StartEffect();
                GameManager.Instance.isHandMenuActive = true;
                break;
            case xBotActions.talkGotoAjustes:
                Talk("Ahora presiona el boton de Ajustes, es el unico naranja",0);
                break;
            case xBotActions.talkGotoEntornos:
                Talk("Presiona el boton Entornos",0);
                break;
            case xBotActions.talkGotoTaller:
                Talk("Presiona el boton Taller", 0);
                break;
            case xBotActions.finishMenuTuto:
                Talk("Muy bien!\nYa puedes bajar la mano y desactivar el menu", 0);
                animator.SetTrigger("HandDown_Left");
                matrix.EndEffect();
                GameManager.Instance.HandMenuFinishedTuto();
                break;
            case xBotActions.cheersFinishMenuTuto:
                Talk("Completaste el Tutorial del menu!",0);
                animator.Play("Clapping");
                GameManager.Instance.isHandMenuActive = false;
                onTimePassed = () => SetActions(xBotActions.talkStartTeleportTuto);
                _ = StartCoroutine(InvokeWithDelay(5f));
                break;
            case xBotActions.talkStartTeleportTuto:
                Talk("Ahora, te mostrare como moverte hacia el fondo de la habitacion",0);
                animator.Play("Idle");
                teleportPoint = GameObject.FindFirstObjectByType<Autohand.Demo.TeleportPoint>();
                teleportPoint.StartHighlight.AddListener(OnHightLightTeleportPoint);
                teleportPoint.StopHighlight.AddListener(OnStopHightLightTeleportPoint);
                teleportPoint.OnTeleport.AddListener(OnTeleportToPoint);
                onTimePassed = () => SetActions(xBotActions.moveToShowRightHand);
                _ = StartCoroutine(InvokeWithDelay(4f));
                break;
            case xBotActions.moveToShowRightHand:
                moveAgent = true;
                currentWaypoint = 1;
                onDestinationComplete = () => SetActions(xBotActions.showRightHand);
                break;
            case xBotActions.showRightHand:
                Talk("Levanta la mano derecha, palma arriba",0);
                animator.SetTrigger("HandUp_Right");
                teleportLine.SetActive(true);
                GameManager.Instance.isTeleporterActive = true;
                break;
            case xBotActions.explainTeleport_1:
                Talk("Apunta la linea hacia el circulo en el suelo.",0);
                break;
            case xBotActions.explainTeleport_2:
                Talk("Bien, mientras la linea sea azul, cierra la palma de tu mano, haciendo un puño.",0);
                break;
            case xBotActions.cheersFinishTeleportTuto:
                Talk("Excelente! Ya sabes como transportarte!", 0);                
                animator.Play("Clapping");
                animator.SetTrigger("HandDown_Right");
                teleportLine.SetActive(false);
                GameManager.Instance.isTeleporterActive = false;
                onTimePassed = () => SetActions(xBotActions.moveToSideTable);
                _ = StartCoroutine(InvokeWithDelay(3f));
                break;
            case xBotActions.moveToSideTable:
                animator.Play("Idle");
                moveAgent = true;
                currentWaypoint = 2;
                onDestinationComplete = () => SetActions(xBotActions.moveToFrontTable);
                break;
            case xBotActions.moveToFrontTable:
                moveAgent = true;
                currentWaypoint = 3;
                onDestinationComplete = () => SetActions(xBotActions.talkOpenJenga);
                break;
            case xBotActions.talkOpenJenga:
                Talk("Para terminar, abramos la experiencia Jenga.\nAbre el menu, ve a Procedimientos > Jenga",0);
                GameManager.Instance.isHandMenuActive = true;
                GameManager.Instance.handMenu.menuController.OpenMenu("grooVR Simulaciones (TUTOJENGA)");
                break;
            case xBotActions.finishOpenJenga:
                Talk("Muy bien! Ya puedes jugar Jenga!",0);
                GameManager.Instance.HideHandMenu();
                GameManager.Instance.handMenu.menuController.OpenMenu("grooVR Simulaciones");
                GameManager.Instance.teleporter.onlyUseTeleportPoints = false;
                GameManager.Instance.isTeleporterActive = true;
                onTimePassed = () => SetActions(xBotActions.cheersFinishAll);
                _ = StartCoroutine(InvokeWithDelay(3f));
                break;
            case xBotActions.cheersFinishAll:
                Talk("Haz completado el Tutorial! Felicidades!", 0);
                animator.Play("Clapping");
                onTimePassed = () => SetActions(xBotActions.sayGoodBye);
                _ = StartCoroutine(InvokeWithDelay(3f));
                break;
            case xBotActions.sayGoodBye:
                Talk("Ahora me desactivare para que puedas experimentar.\nHasta luego!!",0);
                animator.Play("Idle");
                onTimePassed = () => gameObject.SetActive(false);
                _ = StartCoroutine(InvokeWithDelay(3f));
                break;
        }
    }

    private void OnDestination()
    {
        Debug.Log("NPC reached waypoint");
        agent.isStopped = true;
        moveAgent = false;
        agent.ResetPath();
        onDestinationComplete.Invoke();
        // Trigger next stage event here
    }

    //replace with game manager
    IEnumerator InvokeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        onTimePassed.Invoke();
    }

    public void OnHightLightTeleportPoint(Autohand.Demo.TeleportPoint point, Autohand.Teleporter teleporter)
    {
        if (hasTeleportedFirstTime == false)
        {
            SetActions(xBotActions.explainTeleport_2);
        }
    }

    public void OnStopHightLightTeleportPoint(Autohand.Demo.TeleportPoint point, Autohand.Teleporter teleporter)
    {
        if(hasTeleportedFirstTime == false)
        {
            SetActions(xBotActions.explainTeleport_1);
        }
        
    }

    public void OnTeleportToPoint(Autohand.Demo.TeleportPoint point, Autohand.Teleporter teleporter)
    {
        if (hasTeleportedFirstTime == false)
        {
            SetActions(xBotActions.cheersFinishTeleportTuto);
            hasTeleportedFirstTime = true;
        }
    }





}
