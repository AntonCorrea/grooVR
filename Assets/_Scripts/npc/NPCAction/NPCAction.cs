using System.Collections;
using UnityEngine;

public abstract class NPCAction : MonoBehaviour
{
    public string id;
    public bool stopNow = false;
    public bool skipAfterAction = false;
    public float actionClipTime = 0f;
    public abstract IEnumerator Execute(NPCController npc);
}