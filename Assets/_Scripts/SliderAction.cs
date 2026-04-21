using Autohand;
using UnityEngine;
using UnityEngine.Events;

public class SliderAction : PhysicsGadgetConfigurableLimitReader
{
    public UnityEvent action;
    [Tooltip("Acts as speed")]
    public Vector3 axis = Vector3.up;
    [Header("Range")]
    public bool useRange = false;
    public Vector3 minRange = -Vector3.up;
    public Vector3 maxRange = Vector3.up;

    bool done = false;

    protected new void Start()
    {
        base.Start();
    }

    public void FixedUpdate()
    {
        if(done == false)
        {
            var value = GetValue();
            //print(value);
            if (value > 0.9)
            {
                done = true;
                action.Invoke();
            }
            else if (value < -0.9)
            {
                action.Invoke();
                done = true;
            }
                
        }
        

    }
}

