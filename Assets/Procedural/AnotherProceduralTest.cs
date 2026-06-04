using System.Collections.Generic;
using UnityEngine;

public class AnotherProceduralTest : MonoBehaviour
{
    [Header("Configuración de Prefabs")]
    [SerializeField] private List<GameObject> habitacionesPrefabs; 
    [SerializeField] private Transform jugador;

    [Header("Ajustes del Generador")]
    [SerializeField] private int maxHabitacionesEnPantalla = 5;
    [SerializeField] private float distanciaparaSpawn = 50f;

    private List<GameObject> habitacionesActivas = new List<GameObject>();
    private Vector3 puntoSiguienteSpawn;

    private void Start()
    {
        puntoSiguienteSpawn = transform.position;

        for (int i = 0; i < maxHabitacionesEnPantalla; i++)
        {
            GenerarHabitacion();
        }
    }

    private void Update()
    {
        float distancia = Vector3.Distance(jugador.position, puntoSiguienteSpawn);

        if (distancia < distanciaparaSpawn)
        {
            GenerarHabitacion();
            BorrarHabitacionVieja();
        }
    }

    private void GenerarHabitacion()
    {
        int indiceAleatorio = Random.Range(0, habitacionesPrefabs.Count);
        GameObject prefabSeleccionado = habitacionesPrefabs[indiceAleatorio];
        GameObject nuevaHabitacion = Instantiate(prefabSeleccionado, puntoSiguienteSpawn, Quaternion.identity);
        habitacionesActivas.Add(nuevaHabitacion);

        Transform entrada = nuevaHabitacion.transform.Find("Entrada");
        Transform salida = nuevaHabitacion.transform.Find("Salida");

        if (entrada != null && salida != null)
        {
            nuevaHabitacion.transform.position = puntoSiguienteSpawn;
            nuevaHabitacion.transform.position -= (entrada.position - nuevaHabitacion.transform.position);
            puntoSiguienteSpawn = salida.position;
        }
    }

    private void BorrarHabitacionVieja()
    {
        if (habitacionesActivas.Count > maxHabitacionesEnPantalla)
        {
            GameObject habitacionVieja = habitacionesActivas[0];
            habitacionesActivas.RemoveAt(0);
            Destroy(habitacionVieja);
        }
    }
}