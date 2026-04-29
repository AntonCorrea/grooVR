using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSequence : MonoBehaviour
{
    public string sequenceName;
    public List<NPCAction> sequence = new List<NPCAction>();
    public NPCSequenceRunner runner;   

    public void StartTutorial()
    {
        CreateSequence();
        runner.PlaySequence(sequence);
    }

    public abstract void CreateSequence();
}