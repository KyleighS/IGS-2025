using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchInteract : MonoBehaviour, IInteractable
{
    public string sceneName;
    public string GetDescription()
    {
        return "To Leave";
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}
