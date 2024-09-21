using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
    void Update()
    {
        //detectar si se presiona "espacio"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // cambiar a la escena "Game"
            SceneManager.LoadScene("Game");
        }
    }
}
