using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBearSC : ObjectSC
{
    void Start()
    {
        base.Start();
        objectName = gameObject.name;
    }
}
