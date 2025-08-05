using Photon.Pun;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;


    [Header("UI")]
    public TextMeshProUGUI healthText;

    private bool isLocalPlayer => photonView.IsMine;

    [PunRPC]
    public void TakeDamage(int damageAmount) // Renamed to match Weapon's call
    {
        health -= damageAmount;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            if (isLocalPlayer)
                RoomManager.instance.SpawnPlayer();

            PhotonNetwork.Destroy(gameObject); // Network-safe destruction
        }
    }
}