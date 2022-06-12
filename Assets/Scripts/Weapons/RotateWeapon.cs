using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    public static void Rotate(Quaternion rotation, GameObject obj){
        obj.transform.localRotation = rotation;
    }

    public static void Rotate(GameObject obj){
        /*
        Quaternion weaponRot = FollowPlayer.weaponRotation;
        Quaternion rot = obj.transform.localRotation;
        float rotX = Mathf.Clamp((rot.x + weaponRot.x + 0.25f), 0, 2);
        float rotY = Mathf.Clamp(rot.y + weaponRot.y, -90f, 90f);
        float rotZ = Mathf.Clamp(rot.z + weaponRot.z, -90f, 90f);
        Quaternion rotation = new Quaternion(rotX, rotY, rotZ, weaponRot.w);
        obj.transform.localRotation = rotation;
        */
    }
}
