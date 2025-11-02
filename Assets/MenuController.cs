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
    public MenuInstance[] menus;
    public CellController[] cells;

    public float menuDelay;


    private void Start()
    {   
        //cells = GetComponentsInChildren<CellController>();

        OpenMenu("grooVR Simulaciones (DEMO)");

        if (hideAtStart)
        {
            Hide();
        }
    }

    private void SetActiveCellsButtons(int cellsActive, bool v)
    {
        for (int i = 0; i < cellsActive; i++)
        {
            cells[i].physicButton.enabled = v;
        }
    }

    private void SetActiveCells(int cellsActive, bool v)
    {
        for (int i = 0; i < cellsActive; i++)
        {
            cells[i].gameObject.SetActive(v);
        }
    }

    private void SetDeactiveBtnCells(int cellsDeactive, bool v)
    {
        for (int i = 0; i < cellsDeactive; i++)
        {
            if (cells[i].setDeactive)
            {
                cells[i].SetDeactive(v);
            }
                
        }
    }

    public void Show()
    {
        menuRoot.SetActive(true);
    }

    public void Hide()
    {
        menuRoot.SetActive(false);
    }

    public void OpenMenu(string menu)
    {
        print("OPEN MENU "+ menu);

        MenuInstance menuInstance = menus.FirstOrDefault(i => i.menuTitle == menu);

        title.text = menuInstance.menuTitle;

        SetActiveCellsButtons(6, false);


        GameManager.Instance.actionDelay = () => OpenMenuDelay(menuInstance);
        _ = StartCoroutine(GameManager.Instance.InvokeWithDelay(menuDelay));
    }

    void OpenMenuDelay(MenuInstance menu)
    {
        SetActiveCells(6, false);
        SetDeactiveBtnCells(6, false);

        int currentCellsLenght = menu.options.Length;

        for (int i = 0; i < currentCellsLenght; i++)
        {

            cells[i].textMesh.text = menu.options[i].ToString();

            int index = i;
            cells[i].buttonAction = () => menu.functions[index].Invoke();

            cells[i].physicButton.transform.localPosition = Vector3.zero;

            if(menu.optionsDeactive.Length > 0)
            {
                //if (menu.optionsDeactive[i])
                {
                    cells[i].setDeactive = menu.optionsDeactive[i];
                }
            }
            //cargar modelos 3d dentro de botones
            //if (menus[option].models.Length > i)
            //{
            //    cells[i].SetModel(menus[option].models[i]);
            //}
            //else
            //{
            //    cells[i].SetModel(null);
            //}
        }

        if (menu.idReturnTo != string.Empty)
        {
            int i = currentCellsLenght;

            cells[i].textMesh.text = "Volver";

            string menuIndex = menu.idReturnTo;
            cells[i].buttonAction = () => OpenMenu(menuIndex);

            currentCellsLenght += 1;
        }

        SetActiveCells(currentCellsLenght, true);
        SetActiveCellsButtons(currentCellsLenght, true);
        SetDeactiveBtnCells(currentCellsLenght, true);
    }

    public void LoadEnviroment(int index)
    {
        GameManager.Instance.LoadEnviroment(index);
    }

    

}
