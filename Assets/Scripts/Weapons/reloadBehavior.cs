using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadBehavior : MonoBehaviour
{
    Weapon f = new Weapon(1, 0, 0, 0);

    //Input
    PlayerActionControls playerActionControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Weapon.Reload.performed += _ => Reload();
    }

    private void OnEnable(){
        playerActionControls.Enable();
    }

    private void OnDisable(){
        playerActionControls.Disable();
    }

    //-------------------------------------------
    //Reload

    void Reload()
    {
        f.Reload();
    }
    
}
