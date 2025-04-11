using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Change la scène vers "GameScene" lorsque le bouton "Jouer" est cliqué
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditMenu()
    {
        SceneManager.LoadScene("CreditMenu");
    }
}
