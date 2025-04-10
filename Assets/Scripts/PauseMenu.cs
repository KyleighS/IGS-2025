using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenu = "MainMenu";
    public GameObject pauseMenu;
    public bool isPaused;
    public TimeController timeController;
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            //timeController.UnpauseTimer();
        }
        else
        {
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            //timeController.PauseTimer();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseGame();
    }
}
