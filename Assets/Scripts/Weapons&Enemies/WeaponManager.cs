using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    private int currentWeaponIndex;
    private void Start()
    {
        SelectWeapon(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        int nextWeapon = currentWeaponIndex + 1;

        if (nextWeapon >= weapons.Length)
        {
            nextWeapon = 0;
        }

        SelectWeapon(nextWeapon);
    }

    private void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        currentWeaponIndex = index;
    }
}