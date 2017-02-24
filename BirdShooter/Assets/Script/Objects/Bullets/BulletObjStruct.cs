using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulletObjStruct : BasicObjStruct
{
    public float Damage;
}

[System.Serializable]
public class ChaseBulletObjStruct : BulletObjStruct
{
    public float EjectSpeed;
    public float RotateSpeed;
    public float Tracking;
    
}
