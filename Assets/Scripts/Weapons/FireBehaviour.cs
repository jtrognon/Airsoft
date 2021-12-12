using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    public GameObject impact;
    public LayerMask layerMask;
    public ParticleSystem fireEffect;

    public void Fire(float accuracy)
    {
        fireEffect.Play();
        Vector3 direction = transform.up * Random.Range(-accuracy, accuracy);
        RaycastHit hit;
        layerMask = ~layerMask; //everything except the layer mask given by the inspector
        bool hitRaycast = Physics.Raycast(transform.position, direction, out hit, 200.0f, layerMask);

        if (hitRaycast){
            Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
            HitBehaviour.Hit(hit.collider, gameObject);
            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGO, 1.5f);
        }
        else{
            Debug.DrawRay(transform.position, direction * 50f, Color.blue);
        }
    }
}
