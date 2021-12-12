using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : MonoBehaviour
{
    public static void Hit(Collider hitCollider, GameObject firingObj){
        if (hitCollider.tag == "Enemy"){
            Debug.Log("Enemy !");
        }
    }
}
