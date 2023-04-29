using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//base Vector3 is not serializable to byte format.
[Serializable]
public struct Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }
    
    public Vector3Data(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}
