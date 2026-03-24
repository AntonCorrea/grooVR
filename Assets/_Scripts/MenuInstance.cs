using UnityEngine;
using UnityEngine.Events;

public class MenuInstance : MonoBehaviour
{
    public MenuType type;
    public string menuId;
    public string idReturnTo;
    public string[] options;
    public UnityEvent[] functions;
    public bool[] optionsDeactive;
    public CellController[] cells;
    public enum MenuType
    {
        BigButton, List, Vehicle
    }
}
