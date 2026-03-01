using UnityEngine;
using UnityEngine.Events;

public class MenuInstance : MonoBehaviour
{
    public string menuId;
    public string idReturnTo;
    public string[] options;
    public UnityEvent[] functions;
    public bool[] optionsDeactive;
    public MenuType type;
    public CellController[] cells;
    public enum MenuType
    {
        BigButton, List, Vehicle
    }
}
