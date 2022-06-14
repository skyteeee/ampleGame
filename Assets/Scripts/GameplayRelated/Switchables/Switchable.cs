using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour
{

    public bool isOn;
    abstract public void TurnOn();
    abstract public void TurnOff();
}
