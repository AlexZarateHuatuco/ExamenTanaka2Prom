
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public PlayerHealth player = new PlayerHealth();

    public Botiquin(PlayerHealth p)
    {
        this.player = p;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(player.CurrentHealth < player.MaxHealth)
            {
                player.RecoverHealth(10f);
                Destroy(gameObject);
            }
        }
    }
}
