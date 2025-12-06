using UnityEngine;

public class GridRoom : MonoBehaviour
{
    public Grid[] grids;

    private void Start()
    {
        StartRoomGrids();
    }

    public void StartRoomGrids()
    {
        foreach (Grid grid in grids)
        {
            grid.StartGrid();
        }
    }
}
