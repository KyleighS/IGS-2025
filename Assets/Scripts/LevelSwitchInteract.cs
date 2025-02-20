using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchInteract : MonoBehaviour, IInteractable
{
    public string sceneName;
    public string GetDescription()
    {
        return "E to Leave";
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}
