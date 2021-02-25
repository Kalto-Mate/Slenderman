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
    public SpriteRenderer nieve; float maxNieveAlpha = 0.8f;
    public SpriteRenderer faceSilouette; float maxfaceSilouette = 0.5f;
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

    public void UpdateStaticEffect(float gradient)
    {
        float newAlpha = maxNieveAlpha * gradient;
        SetStaticAlpha(newAlpha);
    }

    public void UpdateStareEffect(float gradient)
    {
        float newAlpha = maxfaceSilouette * gradient;
        SetFaceSilouetteAlpha(newAlpha);
    }
}
