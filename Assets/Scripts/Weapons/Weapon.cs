using Photon.Pun;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Camera camera;

    public int damage;

    public float fireRate;

    private float nextFire;

    [Header("VFX")]
    public GameObject hitVFX;

    void Update()
    {
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if(Input.GetButton("Fire1") && nextFire  <= 0)
        {
            nextFire = 1 / fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {

            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            if (hit.transform.TryGetComponent<Health>(out var health))
            {
                PhotonView pv = health.GetComponent<PhotonView>();
                if (pv != null)
                {
                    pv.RPC("TakeDamage", RpcTarget.All, damage); // Matches renamed method
                }
            }
        } 
    }
}
