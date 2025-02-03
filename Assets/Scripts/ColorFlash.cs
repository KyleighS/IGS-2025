using Unity.VisualScripting;
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
        //checks if the dot color is not the new color and that the aplha is decreasing
        if (dot.color != newColor && tempColor.a>0 && decrease  )
        {
            //subtracts form the alpha in tempColor and sets it to the dot color
            tempColor.a = tempColor.a - (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            decrease = false;
        }

        //checks if the dot color is not the original color and that the aplha is not decreasing
        if (dot.color != ogColor && tempColor.a < 1 && !decrease)
        {
            //adds to the alpha in tempColor and sets it to the dot color
            tempColor.a = tempColor.a + (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            decrease = true;
        }

    }

}
