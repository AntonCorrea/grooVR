using System.Linq;
using TMPro;
using UnityEngine;

//public enum OptionsBtn
//{
//    Vehiculos, Modelos_3D, Procedimientos, Ajustes, Camioneta_4x4
//}

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



    private void Start()
    {   
        //cells = GetComponentsInChildren<CellController>();

        //OpenMenu("grooVR Simulaciones (TUTOMENU)");

        if (hideAtStart)
        {
            HideHandMenu();
        }
    }

    private void SetActiveCellsButtons(int index, bool v)
    {
        cells[index].physicButton.enabled = v;
    }

    private void SetActiveCells(int index, bool v)
    {
        cells[index].gameObject.SetActive(v);
    }

    private void SetDeactiveBtnCells(int index, bool v)
    {
        if (cells[index].setDeactive)
        {
            cells[index].SetDeactive(v);
        }

    }

    public void ShowHandMenu()
    {
        menuRoot.SetActive(true);
    }

    public void HideHandMenu()
    {
        menuRoot.SetActive(false);

        //for (int i = 0; i < optionsCount; i++)
        //{
        //    cells[i].physicButton.transform.localPosition = Vector3.zero;
        //}
    }

    public void OpenMenu(string menu)
    {
        print("OPEN MENU "+ menu);

        MenuInstance menuInstance = menus.FirstOrDefault(i => i.menuTitle == menu);
           
        title.text = menuInstance.menuTitle;
        
        GameManager.Instance.actionDelay = () => OpenMenuDelay(menuInstance);
        _ = StartCoroutine(GameManager.Instance.InvokeWithDelay(menuDelay));
    }

    void OpenMenuType(int i)
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

    void OpenMenuDelay(MenuInstance menu)
    {
        //assign cells to current menu
        OpenMenuType((int)menu.type);
        cells = currentMenu.GetComponentsInChildren<CellController>();

        if(menu.type != MenuInstance.MenuType.Vehicle)
        {
            //deactivate all cells
            int auxCells = cells.Length;
            for (int i = 0; i < auxCells; i++)
            {
                SetActiveCellsButtons(i, false);
                SetActiveCells(i, false);
                SetDeactiveBtnCells(i, false);
            }

            //load cells content
            int currentCellsLenght = menu.options.Length;
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

                //cargar modelos 3d dentro de botones
            }

            //reactivate cells
            for (int i = 0; i < currentCellsLenght; i++)
            {
                cells[i].physicButton.transform.localPosition = Vector3.zero;

                SetActiveCells(i, true);
                SetActiveCellsButtons(i, true);
                SetDeactiveBtnCells(i, true);
            }
        }
        

        //goback button
        if (menu.idReturnTo != string.Empty)
        {
            goBackCell.textMesh.text = "Volver";
            goBackCell.SetDeactive(false);

            string menuIndex = menu.idReturnTo;
            goBackCell.buttonAction = () => OpenMenu(menuIndex);
        }
        else
        {
            goBackCell.textMesh.text = "";
            goBackCell.SetDeactive(true);
        }
          
    }

    public void LoadEnviroment(int index)
    {
        GameManager.Instance.LoadEnviroment(index);
    }

    public void LoadVideo(int index)
    {
        GameManager.Instance.LoadVideo(index);
    }

}
