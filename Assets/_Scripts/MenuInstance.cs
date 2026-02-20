using UnityEngine;
using UnityEngine.Events;

public class MenuInstance : MonoBehaviour
{
    //public string[] options;
    public string menuTitle;
    public string idReturnTo;
    public string[] options;
    public GameObject[] models;
    public UnityEvent[] functions;
    public bool[] optionsDeactive;
    public MenuType type;
    public CellController[] cells;
    public enum MenuType
    {
        BigButton, List, Vehicle
    }
}
