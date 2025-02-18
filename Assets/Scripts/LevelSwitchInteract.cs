using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchInteract : MonoBehaviour, IInteractable
{
    public string sceneName;
    public string GetDescription()
    {
        return "Left Click to Leave";
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}
