using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controls;
    //public string loadLevel;

    void Start()
    {
        controls.SetActive(false);
    }

    public void LoadLevls(string loadLevl)
    {
        SceneManager.LoadScene(loadLevl);
    }

    public void OpenControls()
    {
        controls.SetActive(true);
    }

    public void CloseControls()
    {
        controls.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
