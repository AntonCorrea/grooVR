using UnityEngine;

public enum OptionsBtn
{
    Vehiculos, Modelos_3D, Procedimientos, Opciones
}

public class MenuController : MonoBehaviour
{
    public MenuInstance[] menus;

    public CellController[] cells;

    private void Start()
    {
        Hide();

        cells = GetComponentsInChildren<CellController>();
        
        for(int i=0; i<6; i++)
        {
            if(menus[0].optionsFunctions.Length > i)
            {
                cells[i].gameObject.SetActive(true);

                cells[i].textMesh.text = menus[0].optionsFunctions[i].ToString();

                //agregar ref a funcion

                if(menus[0].models.Length > i)
                {
                    cells[i].SetModel(menus[0].models[i]);
                }
                else
                {
                    cells[i].SetModel(null);
                }
                

            }
            else
            {
                cells[i].gameObject.SetActive(false);
            }
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
