using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : Switchable
{

    public bool startLit = false;

    void Start()
    {
        if (startLit)
        {
            TurnOn();
        } else
        {
            TurnOff();
        }

    }

    public override void TurnOff()
    {
        isOn = false;
        SpriteRenderer material = gameObject.GetComponent<SpriteRenderer>();
        material.material.DisableKeyword("_EMISSION");
    }

    public override void TurnOn()
    {
        isOn = true;
        SpriteRenderer material = gameObject.GetComponent<SpriteRenderer>();
        material.material.EnableKeyword("_EMISSION");
    }


}
