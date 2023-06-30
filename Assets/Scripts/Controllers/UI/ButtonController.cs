
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public void ChangeScene(string nameScene)
    { SceneManager.LoadScene(nameScene); }

    public void ExitGame()
    { Application.Quit(); }

}
