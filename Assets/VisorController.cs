using System.Linq;
using UnityEngine;

public class VisorController : MonoBehaviour
{
    public Transform modelSpawnPoint;

    public VisorInstance[] visorModels;

    public VisorInstance currentVisorInstance;

    public CellController lockPositionXcell, lockPositionYcell, lockPositionZcell, lockRotationXcell, lockRotationYcell, lockRotationZcell;
    public void SpawnVisorModel(string modelName)
    {
        if (currentVisorInstance != null)
        {
            Destroy(currentVisorInstance.gameObject);
        }

        VisorInstance newVisorModel = visorModels.FirstOrDefault(i => i.modelName == modelName);
        currentVisorInstance = Instantiate(newVisorModel, modelSpawnPoint);
        UpdateToggleButtons();
    }

    public void UpdateToggleButtons()
    {
        lockPositionXcell.ToggleButton(currentVisorInstance.lockpositionX);
        lockPositionYcell.ToggleButton(currentVisorInstance.lockpositionY);
        lockPositionZcell.ToggleButton(currentVisorInstance.lockpositionZ);
        lockRotationXcell.ToggleButton(currentVisorInstance.lockRotationX);
        lockRotationYcell.ToggleButton(currentVisorInstance.lockRotationY);
        lockRotationZcell.ToggleButton(currentVisorInstance.lockRotationZ);

        if (currentVisorInstance.lockpositionX)
        {
            //lockPositionXcell.ToggleButton();
            currentVisorInstance.LockPositionX();
            currentVisorInstance.LockPositionX();
        }
        if (currentVisorInstance.lockpositionY)
        {
            //lockPositionYcell.ToggleButton();
            currentVisorInstance.LockPositionY();
            currentVisorInstance.LockPositionY();
        }
        if (currentVisorInstance.lockpositionZ)
        {
            //lockPositionZcell.ToggleButton();
            currentVisorInstance.LockPositionZ();
            currentVisorInstance.LockPositionZ();
        }
        if (currentVisorInstance.lockRotationX)
        {
            //lockRotationXcell.ToggleButton();
            currentVisorInstance.LockRotationX();
            currentVisorInstance.LockRotationX();
        }
        if (currentVisorInstance.lockRotationY)
        {
            //lockRotationYcell.ToggleButton();
            currentVisorInstance.LockRotationY();
            currentVisorInstance.LockRotationY();
        }
        if (currentVisorInstance.lockRotationZ)
        {
            //lockRotationZcell.ToggleButton();
            currentVisorInstance.LockRotationZ();
            currentVisorInstance.LockRotationZ();
        }



        //currentVisorInstance.LockPositionX();
        //currentVisorInstance.LockPositionY();
        //currentVisorInstance.LockPositionZ();
        //currentVisorInstance.LockRotationX();
        //currentVisorInstance.LockRotationY();
        //currentVisorInstance.LockRotationZ();
    }

    public void LockCurrentVisorPositionX()
    {
        currentVisorInstance.LockPositionX();
    }

    public void LockCurrentVisorPositionY()
    {
        currentVisorInstance.LockPositionY();
    }

    public void LockCurrentVisorPositionZ()
    {
        currentVisorInstance.LockPositionZ();
    }

    public void LockCurrentVisorRotationX()
    {
        currentVisorInstance.LockRotationX();
    }

    public void LockCurrentVisorRotationY()
    {
        currentVisorInstance.LockRotationY();
    }

    public void LockCurrentVisorRotationZ()
    {
        currentVisorInstance.LockRotationZ();
    }

    public void ResetVisor()
    {
        GameManager.Instance.ResetPlayerPositionInEnviroment();
        SpawnVisorModel(currentVisorInstance.modelName);
    }

    public void ToggleCurrentGuizmo()
    {
        currentVisorInstance.ToggleGuizmo();
    }
}
