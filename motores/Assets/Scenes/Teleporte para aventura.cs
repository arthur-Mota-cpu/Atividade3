using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Continua 6");
        }
    }
}