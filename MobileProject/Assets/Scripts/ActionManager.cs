using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    iAction curAction;
    
    public void StartAction(iAction newAction)
    {
        if (curAction == newAction) return;

        if (curAction != null)
            curAction.End();

        curAction = newAction;
    }
}
