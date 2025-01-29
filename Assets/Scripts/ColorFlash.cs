using UnityEngine;
using UnityEngine.UI;

public class ColorFlash : MonoBehaviour
{
    public Image dot;
    public Color newColor;
    public Color ogColor;
    public float fadeTime = 0.1f;
    public bool decrease = true;

    private void Update()
    {
        Color tempColor = dot.color;
        if (dot.color != newColor && tempColor.a>0 && decrease  )
        {
            //Debug.Log("Increasing" + dot.color.a);
            tempColor.a = tempColor.a - (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            //Debug.Log("SWAP" + dot.color);
            decrease = false;
        }
        if (dot.color != ogColor && tempColor.a < 1 && !decrease)
        {
            //Debug.Log("Decreasing" + dot.color.a);
            tempColor.a = tempColor.a + (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            //Debug.Log("Decreasing"+ dot.color);
            decrease = true;
        }

    }

    //this should be called somewhere in Update
    void FadeOut()
    {
        dot.color = Color.Lerp(dot.color, newColor, fadeTime * Time.deltaTime);

    }
    void FadeIn()
    {
        dot.color = Color.Lerp(dot.color, ogColor, fadeTime * Time.deltaTime);
    }
}
