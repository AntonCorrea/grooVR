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

        OpenMenu("grooVR Simulaciones (TUTOMENU)");

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

        for (int i = 0; i < 6; i++)
        {
            cells[i].physicButton.transform.localPosition = Vector3.zero;
        }
    }

    public void OpenMenu(string menu)
    {
        print("OPEN MENU "+ menu);

        MenuInstance menuInstance = menus.FirstOrDefault(i => i.menuTitle == menu);

        title.text = menuInstance.menuTitle;
        
        GameManager.Instance.actionDelay = () => OpenMenuDelay(menuInstance);
        _ = StartCoroutine(GameManager.Instance.InvokeWithDelay(menuDelay));
    }

    void OpenMenuDelay(MenuInstance menu)
    {
        int auxCells = 6;
        for(int i=0; i< auxCells; i++)
        {
            SetActiveCellsButtons(i, false);
            SetActiveCells(i, false);
            SetDeactiveBtnCells(i, false);
        }
        
        int currentCellsLenght = menu.options.Length;

        for (int i = 0; i < currentCellsLenght; i++)
        {

            cells[i].textMesh.text = menu.options[i].ToString();

            int index = i;
            cells[i].buttonAction = () => menu.functions[index].Invoke();

            if(menu.optionsDeactive.Length > 0)
            {
                {
                    cells[i].setDeactive = menu.optionsDeactive[i];
                }
            }

            //cargar modelos 3d dentro de botones
        }

        if (menu.idReturnTo != string.Empty)
        {
            int i = currentCellsLenght;

            cells[i].textMesh.text = "Volver";

            string menuIndex = menu.idReturnTo;
            cells[i].buttonAction = () => OpenMenu(menuIndex);

            currentCellsLenght += 1;
        }

        for (int i = 0; i < currentCellsLenght; i++)
        {
            cells[i].physicButton.transform.localPosition = Vector3.zero;

            SetActiveCells(i, true);
            SetActiveCellsButtons(i, true);
            SetDeactiveBtnCells(i, true);
        }
            
    }

    public void LoadEnviroment(int index)
    {
        GameManager.Instance.LoadEnviroment(index);
    }

    

}
