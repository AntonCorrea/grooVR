using System.Collections;
using UnityEngine;

public abstract class NPCAction : MonoBehaviour
{
    public string id;
    public bool stopNow = false;
    public bool skipAfter = false;
    public float loopTime = 0f;
    public abstract IEnumerator Execute(NPCController npc);
}