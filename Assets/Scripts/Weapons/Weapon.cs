using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Inputs
    PlayerActionControls playerActionControls;

    private void Awake(){
        playerActionControls = new PlayerActionControls();

    }

    private void OnEnable(){
        playerActionControls.Enable();
    }

    private void OnDisable(){
        playerActionControls.Disable();
    }

    //---------------------------------------------------------------------
    //Weapons propreties

    private float fireRate;
    private float range;
    private float recoil;
    private float accuracy;

    public Weapon(float _fireRate, float _range, float _recoil, float _accuracy){
        fireRate = _fireRate;
        range = _range;
        recoil = _recoil;
        accuracy = _accuracy;
    }

    //------------------------------------------------------------------------------------------------
    //Fire events

    FireBehaviour fireBehaviour;
    private float nextTimeToFire = 0f;

    private void Start() {
        fireBehaviour = GetComponent<FireBehaviour>();
    }

    void Update(){
        //check if its possible to fire
        if (playerActionControls.Player.Fire.ReadValue<float>() > 0f && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate; //be able to fire again in (1f/fireRate) seconds.
            fireBehaviour.Fire(accuracy);
        }

        RotateWeapon.Rotate(gameObject);
    }
}
