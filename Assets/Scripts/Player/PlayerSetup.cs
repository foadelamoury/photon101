using UnityEngine;

public class PlayerSetup : MonoBehaviour
{

    public Movement movement;

    public new GameObject camera;


    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
        Sway weaponSway = GetComponentInChildren<Sway>(true);
        if(weaponSway != null)
        weaponSway.enabled = true;

    }



}
