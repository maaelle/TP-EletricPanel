using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class And : LogicGateBase
{
    public override bool UseGate()
    {
        if (connectionPins[0].IsOn == true && connectionPins[1].IsOn == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
