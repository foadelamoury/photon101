using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Movement movement;
    public GameObject camera;
    public Weapon weapon; // Add weapon reference

    private void Start()
    {
        if (photonView.IsMine)
        {
            // Enable local components
            movement.enabled = true;
            camera.SetActive(true);

            // Enable sway
            Sway weaponSway = GetComponentInChildren<Sway>(true);
            if (weaponSway != null) weaponSway.enabled = true;

            // Enable weapon for local player
            if (weapon != null) weapon.enabled = true;
        }
        else
        {
            // Disable components for remote players
            movement.enabled = false;
            if (camera != null) camera.SetActive(false);
            if (weapon != null) weapon.enabled = false;
        }
    }
}