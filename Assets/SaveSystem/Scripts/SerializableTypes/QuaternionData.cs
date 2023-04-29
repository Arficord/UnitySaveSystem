using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base Quaternion is not serializable to byte format.
[Serializable]
public class QuaternionData
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionData(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }
    
    public QuaternionData(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Quaternion GetQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}
