using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour
{
    abstract public void TurnOn();
    abstract public void TurnOff();
}
