using UnityEngine;
using UnityEngine.SceneManagement;

public class WallCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("¡Colisión con una pared!");
            SceneManager.LoadScene("GameOver");
        }
    }
}
