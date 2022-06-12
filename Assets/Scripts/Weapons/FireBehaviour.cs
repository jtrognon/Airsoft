using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    public GameObject impact;
    public LayerMask layerMask;
    public ParticleSystem fireEffect;
    public Camera fpsCamera;

    public void Fire(float accuracy)
    {
        fireEffect.Play();
        Vector3 direction = fpsCamera.transform.forward;
        RaycastHit hit;
        //layerMask = ~layerMask; everything except the layer mask given by the inspector
        bool hitRaycast = Physics.Raycast(fpsCamera.transform.position, direction, out hit, 200.0f, layerMask);
        Debug.Log(hitRaycast);

        if (hitRaycast){
            HitBehaviour.Hit(hit.collider, gameObject);
            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGO, 1.5f);
        }
    }
}
