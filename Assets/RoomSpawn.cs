using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public GameObject[] areas;
    public int currentRoom = 1;
    // Iglesia 1
    // Cementerio 2
    // Pueblo 3

    public void OnTriggerEnter(Collider RoomSpawn)
    {
        if (RoomSpawn.gameObject.CompareTag("Player"))
        {
            GenerateRoom();
        }
    }

    public void GenerateRoom()
    {
       
    }
}