using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Not : LogicGateBase
{

    public override bool UseGate()
    {
        if (connectionPins[0].IsOn  == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
