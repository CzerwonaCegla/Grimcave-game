using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool WasTriggered = false;

    public void TriggerChange()
    {
        WasTriggered = true;
    }
}
