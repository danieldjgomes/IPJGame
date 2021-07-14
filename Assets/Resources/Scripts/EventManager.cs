using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void EndRoundAction();
    public static event EndRoundAction OnRoundEnds;


    public static void CallNextTurn()
    {
        if (OnRoundEnds != null)
        {
            OnRoundEnds();
        }
    }
}
