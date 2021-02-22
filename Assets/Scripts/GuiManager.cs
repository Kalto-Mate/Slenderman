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
}
