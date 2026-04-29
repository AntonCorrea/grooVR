using System.Linq;
using TMPro;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public GameObject menuRoot;
    public TextMeshProUGUI title;
    public bool hideAtStart = false;

    public CellController goBackCell;

    private string currentMenuOpen;
    public MenuInstance[] menus;
    public CellController[] cells;
    public int currentCellsLenght;

    [Header("Menu Objects")]
    public GameObject currentMenu;
    public GameObject bigButtonsMenu;
    public GameObject listMenu;
    public GameObject vehiclesMenu;
    public GameObject visorMenu;
    public GameObject visorMenuAdvanced;
    
    [Header("Animation settings")]
    public float stagger = 0.03f;
    public Ease exitEase = Ease.InCubic;
    public float exitDuration = 0.22f;
    public float enterDuration = 0.4f;
    public Ease enterEase = Ease.OutBack;

    private void Start()
    {   
        if (hideAtStart)
        {
            HideHandMenu();
        }
    }


    public void ShowHandMenu()
    {
        menuRoot.SetActive(true);
        
    }

    public void HideHandMenu()
    {
        menuRoot.SetActive(false);
        UpdatePhysicsButtons();
        goBackCell.SetPositionPhysicButton();
    }

    public void OpenMenu(string menu)
    {
        print("OPEN MENU "+ menu);
        currentMenuOpen = menu;
        MenuInstance menuInstance = menus.FirstOrDefault(i => i.menuId == menu);
        title.text = menuInstance.menuId;

        _ = StartCoroutine(TransitionCoroutine(menuInstance));
    }

    private IEnumerator TransitionCoroutine(MenuInstance menu)
    {
        //print("apagar botones");
        int auxCells = cells.Length;

        UpdatePhysicsButtons();

        //print("animar salida");
        yield return AnimateExit();

        //assign cells to current menu
        SetMenuType((int)menu.type);
        cells = currentMenu.GetComponentsInChildren<CellController>(true);
        currentCellsLenght = menu.options.Length;

        //print("build menu");
        BuildMenu(menu);

        // Esperar un frame para que el layout se reconstruya
        yield return null;

        //print("animar entrada");
        yield return AnimateEnter();

        //print("prender botones");
        UpdatePhysicsButtons();
    }

    void UpdatePhysicsButtons()
    {
        for (int i = 0; i < currentCellsLenght; i++)
        {
            cells[i].SetPositionPhysicButton();
        }
    }


    private IEnumerator AnimateExit()
    {
        System.Collections.Generic.List<Transform> visuals = cells.Select(x => x.visuals.transform).ToList();

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < visuals.Count; i++)
        {
            Transform visual = visuals[i];
            seq.Insert(i * stagger,
                visual.DOScale(Vector3.zero, exitDuration).SetEase(exitEase)
            );
        }

        yield return seq.WaitForCompletion();
    }

    private IEnumerator AnimateEnter()
    {
        System.Collections.Generic.List<Transform> visuals = cells.Select(x => x.visuals.transform).ToList();

        //Resetear escala en 0 antes de animar
        foreach (var visual in visuals)
        {
            visual.localScale = Vector3.zero;
        }

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < visuals.Count; i++)
        {
            Transform visual = visuals[i];
            seq.Insert(i * stagger,
                visual.DOScale(Vector3.one, enterDuration).SetEase(enterEase)
            );
        }
        yield return seq.WaitForCompletion();
    }

    void BuildMenu(MenuInstance menu)
    {
        if(menu.type == MenuInstance.MenuType.BigButton || menu.type == MenuInstance.MenuType.List)
        {
            //deactivate all cells
            int auxCells = cells.Length;
            for (int i = 0; i < auxCells; i++)
            {
                cells[i].SetActive(false);
                cells[i].SetDisabledButton(false);
            }

            //load cells content
            for (int i = 0; i < currentCellsLenght; i++)
            {

                cells[i].textMesh.text = menu.options[i].ToString();

                int index = i;
                if(menu.functions[index] != null)
                {
                    cells[i].unityEvent = menu.functions[index];
                }

                if (menu.optionsDeactive.Length > 0)
                {
                    {
                        cells[i].setDeactive = menu.optionsDeactive[i];
                    }
                }

            }

            //reactivate cells
            for (int i = 0; i < currentCellsLenght; i++)
            {
                cells[i].SetActive(true);
                cells[i].SetDisabledButton(true);
            }
        }
        
        //goback button
        if (menu.idReturnTo != string.Empty)
        {
            goBackCell.textMesh.text = "Volver";
            goBackCell.SetPositionPhysicButton();
            goBackCell.SetActive(true);
            string menuIndex = menu.idReturnTo;
            goBackCell.unityEvent.RemoveAllListeners();
            goBackCell.unityEvent.AddListener(() => OpenMenu(menuIndex));
        }
        else
        {
            goBackCell.textMesh.text = "";
            goBackCell.SetActive(false);
        }       
    }

    void SetMenuType(int i)
    {
        bigButtonsMenu.SetActive(false);
        listMenu.SetActive(false);
        vehiclesMenu.SetActive(false);
        visorMenu.SetActive(false);
        visorMenuAdvanced.SetActive(false);

        switch (i)
        {
            case 0:
                currentMenu = bigButtonsMenu;
                break;
            case 1:
                currentMenu = listMenu;
                break;
            case 2:
                currentMenu = vehiclesMenu;
                break;
            case 3:
                currentMenu = visorMenu;
                break;
            case 4:
                currentMenu = visorMenuAdvanced;
                break;
        }

        currentMenu.SetActive(true);
    }

    public void Load360Video(int index)
    {
        GameManager.Instance.Load360Video(index);
    }

    public void LoadFlatVideo(int index)
    {
        GameManager.Instance.LoadFlatVideo(index);
    }

    public void LoadEnviroment(string enviroment)
    {
        GameManager.Instance.SpawnEnviroment(enviroment);
    }

    public void LoadEnviromentAndTutorial(string enviroment)
    {
        GameManager.Instance.SpawnEnviromentAndTutorial(enviroment);
    }

    public void LoadVehicleSim()
    {
        GameManager.Instance.SpawnVehicleSimulator();
    }

    public void LoadVisorModel(string modelName)
    {
        GameManager.Instance.SpawnVisorModel(modelName);
    }

    public void VisorLockPositionX()
    {
        GameManager.Instance.visorController.LockCurrentVisorPositionX();
    }

    public void VisorLockPositionY()
    {
        GameManager.Instance.visorController.LockCurrentVisorPositionY();
    }

    public void VisorLockPositionZ()
    {
        GameManager.Instance.visorController.LockCurrentVisorPositionZ();
    }

    public void VisorLockRotationX()
    {
        GameManager.Instance.visorController.LockCurrentVisorRotationX();
    }

    public void VisorLockRotationY()
    {
        GameManager.Instance.visorController.LockCurrentVisorRotationY();

    }

    public void VisorLockRotationZ()
    {
        GameManager.Instance.visorController.LockCurrentVisorRotationZ();

    }

    public void VisorReset()
    {
        GameManager.Instance.visorController.ResetVisor();
    }

    public void VisorToggleGuizmo()
    {
        GameManager.Instance.visorController.ToggleCurrentGuizmo();
    }

    public void VisorStartUpdateSize(float value)
    {
        GameManager.Instance.visorController.StartUpdateSize(value);
    }
    public void VisorStopUpdateSize()
    {
        GameManager.Instance.visorController.StopUpdateSize();
    }

    public void VisorUpdateHeight(float value)
    {
        GameManager.Instance.visorController.UpdateHeight(value);
    }


}
