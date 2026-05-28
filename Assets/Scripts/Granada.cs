using UnityEngine;

public class Granada : MonoBehaviour
{
    public PlayerHealth player;

    public void OnTriggerEnter(Collider damage)
    {
        if (damage.gameObject.CompareTag("Player"))
        {

        }
    }
}
