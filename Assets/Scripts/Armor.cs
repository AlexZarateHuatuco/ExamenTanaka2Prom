
using UnityEngine;

public class Armor : MonoBehaviour
{
    public PlayerHealth player = new PlayerHealth();

    public Armor(PlayerHealth p)
    {
        this.player = p;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.CurrentArmor < player.MaxArmor)
            {
                player.RecoverArmor(10f);
                Destroy(gameObject);
            }
        }
    }
}
