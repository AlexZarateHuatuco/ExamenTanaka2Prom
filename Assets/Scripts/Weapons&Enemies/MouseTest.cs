using UnityEngine;

public class MouseTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ESPACIO");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK");
        }
    }
}