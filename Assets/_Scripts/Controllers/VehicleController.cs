using System.Linq;
using TMPro;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public VehicleInstance[] vehicles;
    public VehicleInstance currentVehicleInstance;

    bool isDriving = false;
    public Camera vehicleCam;
    public Camera mainPlayerCam;

    public string currentVehicle;
    public string currentVehicleMode;
    public string currentVehicleEnviroment;

    public TextMeshProUGUI currentVehicleText;
    public TextMeshProUGUI currentVehicleModeText;
    public TextMeshProUGUI currentVehicleEnviromentText;

    private void Start()
    {
        AssignCurrentVehicle(currentVehicle);
        
        AssignCurrentVehicleMode(currentVehicleMode);

        AssignCurrentVehicleEnviroment(currentVehicleEnviroment);
    }

    private void Update()
    {
        if (Input.GetButtonDown("JoySubmit") || Input.GetButtonDown("Submit"))
        {
            ToogleVehicle();
        }
    }

    public void AssignCurrentVehicle(string vehicle)
    {
        currentVehicle = vehicle;
        currentVehicleText.text = vehicle;
    }

    public void AssignCurrentVehicleMode(string mode)
    {
        currentVehicleMode = mode;
        currentVehicleModeText.text = mode;
    }

    public void AssignCurrentVehicleEnviroment(string env)
    {
        currentVehicleEnviroment = env;
        currentVehicleEnviromentText.text = env;
    }


    public void SpawnVehicle()
    {
        ClearController();
            
        VehicleInstance newVehicle = vehicles.FirstOrDefault(i => i.vehicleName == currentVehicle);
        currentVehicleInstance = Instantiate(newVehicle, GameManager.Instance.enviromentController.currentEnviromentInstance.vehicleSpawnPoint.transform);
        vehicleCam = currentVehicleInstance.carCam;
    }

    public void ClearController()
    {
        if (currentVehicleInstance != null)
        {
            Destroy(currentVehicleInstance.gameObject);
        }
    }


    void ToogleVehicle()
    {
        if (isDriving)
        {
            vehicleCam.enabled = false;
            mainPlayerCam.enabled = true;
            isDriving = false;
        }
        else
        {
            vehicleCam.enabled = true;
            mainPlayerCam.enabled = false;
            isDriving = true;
        }
    }
}
