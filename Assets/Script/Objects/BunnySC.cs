using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySC : ObjectSC
{
    void Start()
    {
        base.Start();
        objectName = gameObject.name;
    }
}
