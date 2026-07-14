using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanItSC : ObjectSC
{
    void Start()
    {
        base.Start();
        objectName = gameObject.name;
    }
}
