using System.Linq;
using TMPro;
using UnityEngine;

public class VisorController : MonoBehaviour
{
    public Transform modelSpawnPoint;

    public VisorInstance[] visorModels;

    public VisorInstance currentVisorInstance;

    public CellController lockPositionXcell, lockPositionYcell, lockPositionZcell, lockRotationXcell, lockRotationYcell, lockRotationZcell;

    public TextMeshProUGUI textSize;
    private float instanceSize = 1;
    private bool isSizeUpdated = false;
    private float sizeUpdatedValue;
    public float scaleSpeed = 5f; // tweak this

    public TextMeshProUGUI textHeigth;
    private float instanceHeight = 1;
    private void FixedUpdate()
    {
        if (isSizeUpdated)
        {
            float factor = 1f + (sizeUpdatedValue * scaleSpeed * Time.fixedDeltaTime);
            instanceSize *= factor;

            textSize.text = instanceSize.ToString();
            currentVisorInstance.model.transform.localScale = Vector3.one * instanceSize;
        }
    }

    public void SpawnVisorModel(string modelName)
    {
        GameManager.Instance.ResetPlayerPositionInEnviroment();

        ClearController();

        VisorInstance newVisorModel = visorModels.FirstOrDefault(i => i.modelName == modelName);
        currentVisorInstance = Instantiate(newVisorModel, modelSpawnPoint);
        currentVisorInstance.transform.parent = GameManager.Instance.enviromentController.currentEnviromentInstance.transform;

        instanceSize = 1;
        instanceHeight = 1;
        textSize.text = instanceSize.ToString();
        textHeigth.text = instanceHeight.ToString();

        UpdateToggleButtons();
    }

    public void ClearController()
    {
        if (currentVisorInstance != null)
        {
            Destroy(currentVisorInstance.gameObject);
        }
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

    public void StartUpdateSize(float value)
    {
        isSizeUpdated = true;
        sizeUpdatedValue = value;
    }

    public void StopUpdateSize()
    {
        isSizeUpdated = false;
    }

    public void UpdateHeight(float value)
    {
        instanceHeight += value;
        textHeigth.text = instanceHeight.ToString();
        currentVisorInstance.transform.localPosition += Vector3.up * value;
    }
}
