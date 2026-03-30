using System.Linq;
using UnityEngine;

public class VisorController : MonoBehaviour
{
    public Transform modelSpawnPoint;

    public VisorInstance[] visorModels;

    public VisorInstance currentVisorInstance;

    public void SpawnVisorModel(string modelName)
    {
        if (currentVisorInstance != null)
        {
            Destroy(currentVisorInstance.gameObject);
        }

        VisorInstance newVisorModel = visorModels.FirstOrDefault(i => i.modelName == modelName);
        currentVisorInstance = Instantiate(newVisorModel, modelSpawnPoint);
    }

    public void LockCurrentVisorPositionX(bool val)
    {
        currentVisorInstance.LockPositionX(val);
    }

    public void LockCurrentVisorPositionY(bool val)
    {
        currentVisorInstance.LockPositionY(val);
    }

    public void LockCurrentVisorPositionZ(bool val)
    {
        currentVisorInstance.LockPositionZ(val);
    }

    public void LockCurrentVisorRotationX(bool val)
    {
        currentVisorInstance.LockRotationX(val);
    }

    public void LockCurrentVisorRotationY(bool val)
    {
        currentVisorInstance.LockRotationY(val);
    }

    public void LockCurrentVisorRotationZ(bool val)
    {
        currentVisorInstance.LockRotationZ(val);
    }

    public void ResetVisor()
    {
        currentVisorInstance.transform.position = modelSpawnPoint.transform.position;
        currentVisorInstance.transform.rotation = modelSpawnPoint.transform.rotation;
    }

    public void SetCurrentGuizmo(bool val)
    {
        currentVisorInstance.SetGuizmo(val);
    }
}
