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

    public float menuDelay;

    //public int optionsCount = 6;

    public GameObject bigButtonsMenu;
    public GameObject listMenu;
    public GameObject vehiclesMenu;
    public GameObject currentMenu;

    public MenuInstance[] menus;
    public CellController[] cells;
    public int currentCellsLenght;

    private void Start()
    {   
        //cells = GetComponentsInChildren<CellController>();

        //OpenMenu("grooVR Simulaciones (TUTOMENU)");

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
    }

    public void OpenMenu(string menu)
    {
        print("OPEN MENU "+ menu);

        MenuInstance menuInstance = menus.FirstOrDefault(i => i.menuId == menu);
        title.text = menuInstance.menuId;

        _ = StartCoroutine(TransitionCoroutine(menuInstance));
    }

    private IEnumerator TransitionCoroutine(MenuInstance menu)
    {
        //print("apagar botones");
        int auxCells = cells.Length;
        for (int i = 0; i < auxCells; i++)
        {
            cells[i].SetEnabledPhysicButton(false);
        }

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
        for (int i = 0; i < currentCellsLenght; i++)
        {
            cells[i].SetEnabledPhysicButton(true);
        }
    }

    public float stagger = 0.03f;
    public Ease exitEase = Ease.InCubic;
    public float exitDuration = 0.22f;
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

    public float enterDuration = 0.4f;
    public Ease enterEase = Ease.OutBack;
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
        if(menu.type != MenuInstance.MenuType.Vehicle)
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
                cells[i].buttonAction = () => menu.functions[index].Invoke();

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
            //goBackCell.SetDisabledButton(false);
            goBackCell.SetActive(true);
            string menuIndex = menu.idReturnTo;
            goBackCell.buttonAction = () => OpenMenu(menuIndex);
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
        }

        currentMenu.SetActive(true);
    }

    public void LoadVideo(int index)
    {
        GameManager.Instance.LoadVideo(index);
    }

    public void LoadEnviromentAndVehicle()
    {
        GameManager.Instance.enviroment.SpawnEnviroment();
        GameManager.Instance.vehicleController.SpawnVehicle();
    }

}
