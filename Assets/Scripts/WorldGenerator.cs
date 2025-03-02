using System;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Script encargado de generar aleatoriamente obstáculos en el mundo.
    // También se encarga de destruir los que no se pueden ver y generar el suelo que haga falta.

    // Primero importamos los Prefabs que vaya a crear el script.
    [SerializeField]
    private GameObject floor;

    [SerializeField]
    private GameObject[] obstaculos;

    [SerializeField]
    private GameObject coin;


    // -- POSICIONES DE LOS OBSTÁCULOS Y SUELOS -- //
    private Vector3 nextObstaclePosition = new Vector3(-1.1f, -4f, 30);
    private Vector3 nextFloorPosition = new Vector3(0, 0, 45);
    private Vector3 nextCoinPosition = new Vector3(-1.1f, 1.75f, 35f);

    void Start()
    {
        if (floor == null)
        {
            Debug.LogError("No se ha asignado el prefab de suelo.");
            return;
        }

        if (obstaculos == null)
        {
            Debug.LogError("No se han asignado los prefabs de obstáculos.");
            return;
        }

        if (coin == null)
        {
            Debug.LogError("No se han asignado los prefabs de monedas.");
            return;
        }

        // Ahora iniciamos los primeros obstaculos. Pondremos 5 obstáculos en la escena con sus respectivos suelos.
        for (int i = 0; i < 5; i++)
        {
            GenerateObstacle();
        }


    }

    private void GenerateObstacle()
    {

        // Aquí se genera un obstaculo aleatorio.
        int randomObstacle = UnityEngine.Random.Range(0, obstaculos.Length);


        int randomCoinPosition = UnityEngine.Random.Range(-1, 1);


        Instantiate(obstaculos[randomObstacle], nextObstaclePosition, Quaternion.identity);
        Instantiate(floor, nextFloorPosition, Quaternion.identity);

        // Por cada obstáculo generamos 10 monedas. Estas monedas son las que nos dan puntos.
        for (int i = 0; i < 5; i++)
        {
            Instantiate(coin, new Vector3(nextCoinPosition.x + randomCoinPosition, nextCoinPosition.y, nextCoinPosition.z), Quaternion.identity);
            nextCoinPosition.z += 1.5f;
        }

        // Actualizamos la posición del siguiente obstáculo.
        nextObstaclePosition.z += 20;
        nextFloorPosition.z += 20;
        nextCoinPosition.z = nextObstaclePosition.z + 5;
    }

    void OnTriggerEnter(Collider other)
    {
        // Se va a poner un collider en los huecos de los obstáculos.
        // Estos avisan en el caso de que se haya pasado. Para generar el siguiente en la posición que le toque. 
        if (other.CompareTag("ObstacleWindow"))
        {
            Debug.Log("¡Colisión con una ventanita!");
            GenerateObstacle();
        }
    }
}