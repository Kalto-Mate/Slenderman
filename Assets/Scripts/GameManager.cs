using System;
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
        public GameObject lastNote;

    public GameObject frontSpawn;

    public Slenderman Slenderman;

    int pagesCollected = 0;
    [HideInInspector] public int totalPages = 5;

    void Start()
    {
        GuiManager.singleInstance.UpdatePageCount(pagesCollected);
    }

    // Update is called once per frame
    void Update()
    {
        isGrabbing = Input.GetButton("{GRAB}");


        //DEBUG
        if (Input.GetButton("{DEBUG}")) //Letra T
        {

            Slenderman.AppearInFront();
            AudioManager.singleInstance.AttemptAprehension(100);
        }





        //Adjust overlays during gameplay if Slenderman is not banished
        if (!Slenderman.banished)
        {
            UpdateStatic();
            UpdateStare();

        }
        

        

    }
    //GAMEPLAY
    public void CollectPage()
    {
        pagesCollected++;
        GuiManager.singleInstance.UpdatePageCount(pagesCollected);
        lastNote.SetActive(false);
        inPickUpRange = false;
    }
    

    //INTERNAL
    public float Dist2Slenderman()
    {
        return (Slenderman.transform.position - Jugador.transform.position).magnitude;
    }

    public float AngleDiff()
    {
        float SlendyAngle = Slenderman.transform.rotation.eulerAngles.y;
        float PlayerAngle = Jugador.transform.rotation.eulerAngles.y;
        float angleDiff = Mathf.DeltaAngle(SlendyAngle, PlayerAngle); // -135 a 135 means in frame
        return angleDiff;
    }

    public void UpdateStatic()
    {
        float gradient = 0;
        float distance = Dist2Slenderman();
        float maxradius = Slenderman.interferenceStart;
        float minradius = Slenderman.interferencePeak;

        if (distance > maxradius) gradient = 0;
        else if (distance <= maxradius && distance >= minradius) gradient = 1 - ((distance - minradius) / (maxradius - minradius));
        else if (distance < minradius) gradient = 1;

        GuiManager.singleInstance.UpdateStaticEffect(gradient);
        AudioManager.singleInstance.UpdateStaticEffect(gradient);
    }

    public void UpdateStare()
    {
        float gradient = 0;
        float angleDiff = AngleDiff();
        float visionCone = Slenderman.visionCone;
        float distance = Dist2Slenderman();
        float distanceStare = Slenderman.stareDistance;

        if (angleDiff >= -visionCone && angleDiff <= visionCone) gradient = 0;
        else
        {

            float absAngleDiff = Mathf.Abs(angleDiff);
            gradient = 1 - ((absAngleDiff - 180) / (visionCone - 180));

            float distanceDamp = 0;
            if (distance < distanceStare)   distanceDamp = 1;
            else if (distance <= distanceStare * 2 && distance >= distanceStare) distanceDamp = (distanceStare * 2 - distance) / distanceStare;
            else if (distance > distanceStare * 2) distanceDamp = 0;

            gradient = gradient * distanceDamp;
        }

        GuiManager.singleInstance.UpdateStareEffect(gradient);
        AudioManager.singleInstance.UpdateStareEffect(gradient);


    }
}
