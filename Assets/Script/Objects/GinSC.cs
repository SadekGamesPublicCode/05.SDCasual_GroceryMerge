using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinSC : ObjectSC
{
    void Start()
    {
        base.Start();
        objectName = gameObject.name;
    }
}
