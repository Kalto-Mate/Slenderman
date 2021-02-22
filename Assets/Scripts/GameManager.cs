using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleInstance;
    private void Awake()    {if (singleInstance == null) singleInstance = this; }
    ////////////////////////////////////////

    public GameObject Jugador;
        [HideInInspector]   public bool inPickUpRange = false;
        [HideInInspector]   public bool isGrabbing = false;
        public GameObject frontSpawn;

    public Slenderman Slenderman;

    int pagesCollected = 0;
    [HideInInspector] public int totalPages = 8;

    void Start()
    {
        GuiManager.singleInstance.UpdatePageCount(pagesCollected);
    }

    // Update is called once per frame
    void Update()
    {
        isGrabbing = Input.GetButton("{GRAB}");
        if (Input.GetButton("{DEBUG}"))
        {
            
            Slenderman.AppearInFront();
        }
        

    }

    public void CollectPage()
    {
        pagesCollected++;
        GuiManager.singleInstance.UpdatePageCount(pagesCollected);
    }
}
