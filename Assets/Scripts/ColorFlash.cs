using UnityEngine;
using UnityEngine.UI;

public class ColorFlash : MonoBehaviour
{
    public Image dot;
    public float speed = 1.0f;
    void Start()
    {
        dot = GetComponent<Image>();
        var tempColorAlpha = dot.color;
        tempColorAlpha.a = 1f;
        dot.color = tempColorAlpha;
    }
    private void Update()
    {
        
    }
}
