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
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Door")
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
