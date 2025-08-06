using Photon.Pun;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PhotonView))] // Ensures PhotonView component exists
public class Health : MonoBehaviourPun // Changed from MonoBehaviour to MonoBehaviourPun
{
    public int health;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    [PunRPC]
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthText.text = health.ToString();

        if (health <= 0 && photonView.IsMine)
        {
            RoomManager.instance.SpawnPlayer();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}