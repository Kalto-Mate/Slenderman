using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuiManager : MonoBehaviour
{
    public static GuiManager singleInstance;
    private void Awake() { if (singleInstance == null) singleInstance = this; }
    //---------------------------------------------------------------------------
    public TextMeshProUGUI pageCounterDisplay;
    public SpriteRenderer nieve;        float maxNieveAlpha = 0.8f;
    public SpriteRenderer faceSilouette;
    public SpriteRenderer slenderman;
    //-----------------------

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdatePageCount(int input)
    {
        pageCounterDisplay.text = input + "/" + GameManager.singleInstance.totalPages;
    }

    //OVERLAY
    void SetSpriteRendererAlpha(SpriteRenderer target, float alpha)
    {
        Color CurrentColor = target.color;
        target.color = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, alpha);
    }
        public void SetStaticAlpha(float alpha) { SetSpriteRendererAlpha(nieve, alpha); }
        public void SetFaceSilouetteAlpha(float alpha) { SetSpriteRendererAlpha(faceSilouette, alpha); }
        public void SetSlendermanAlpha(float alpha) { SetSpriteRendererAlpha(slenderman, alpha); }

    public void UpdateStaticEffect(float distance, float maxradius, float minradius)
    {
        if (distance > maxradius) SetStaticAlpha(0);

        else if (distance <= maxradius && distance >= minradius) //Gradient
        {
            float gradient = 1 - (  (distance - minradius)  /  (maxradius - minradius)   );
            float newAlpha = maxNieveAlpha * gradient;
            SetStaticAlpha(newAlpha);
        }

        else if (distance < minradius) SetStaticAlpha(maxNieveAlpha);
    }
}
