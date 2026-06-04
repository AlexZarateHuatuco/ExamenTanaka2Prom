using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 150f;
    [SerializeField] private float maxArmor = 150f;
    [SerializeField] private float currentArm;
    [SerializeField] private float currentHP;
    [SerializeField] private string gameoverscene = "";
   



    void Start()
    {
        currentArm = maxArmor;
        currentHP = maxHealth;

    }

    // falta subrutina de daño :c
    void Update()
    {
       if (currentHP<=0)
        {
            currentHP = 0;
            SceneManager.LoadScene(gameoverscene);
        }
    }
}
