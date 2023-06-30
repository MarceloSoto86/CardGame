using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // [SerializeField] private GameObject confirmationPrompt = null;


    public void ChangeScene(string sceneName)
    {
        // Cargar la escena con el nombre proporcionado
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
