using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSwitchesOn : Switchable
{
    public List<Switchable> allSwitchables = new List<Switchable>();
    public List<Switchable> controlledSwitchables = new List<Switchable>();

    public override void TurnOn()
    {
        foreach (Switchable switchable in controlledSwitchables)
        {
            switchable.TurnOn();
        }
    }

    public override void TurnOff()
    {
        foreach (Switchable switchable in controlledSwitchables)
        {
            switchable.TurnOff();
        }
    }

    private void FixedUpdate()
    {
        bool s = true;
        foreach (Switchable switchable in allSwitchables)
        {
            
            if (!switchable.isOn)
            {
                s = false;
            }

        }

        if (s)
        {
            TurnOn();
        } else
        {
            TurnOff();
        }

    }

}
