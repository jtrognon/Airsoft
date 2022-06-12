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

    public Weapon (float _fireRate, float _range, float _recoil, float _accuracy){
        fireRate = _fireRate;
        range = _range;
        recoil = _recoil;
        accuracy = _accuracy;
    }

    //------------------------------------------------------------------------------------------------
    //Fire events

    FireBehaviour fireBehaviour;
    private float nextTimeToFire = 0f;
    int ammoB = 25; 
    int ammo = 25;

    private void Start() {
        fireBehaviour = GetComponent<FireBehaviour>();
        PlayerPrefs.SetInt("Ammo", ammoB);
    }

    private void Update(){
        //check if its possible to fire
        ammo = PlayerPrefs.GetInt("Ammo", ammoB);
        if (playerActionControls.Player.Fire.ReadValue<float>() > 0 && Time.time >= nextTimeToFire && ammo > 0)
        {   
            --ammo;
            PlayerPrefs.SetInt("Ammo", ammo);
            nextTimeToFire = Time.time + 1f/fireRate; //be able to fire again in (1f/fireRate) seconds.
            fireBehaviour.Fire(accuracy);
        }

        RotateWeapon.Rotate(gameObject);
    }

    public void Reload(){
        ammo = ammoB;
        PlayerPrefs.SetInt("Ammo", ammo);
    }
}
