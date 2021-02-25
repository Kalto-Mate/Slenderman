using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota : MonoBehaviour
{
    public GameObject pickupPrompt;
    public GameObject self;
    void Start()
    {
        pickupPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleInstance.inPickUpRange && GameManager.singleInstance.isGrabbing)
        {
            GameManager.singleInstance.CollectPage();
            self.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider intruso)
    {
        if (intruso.CompareTag("Player"))
        {
            GameManager.singleInstance.inPickUpRange = true;
            pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider intruso)
    {
        if (intruso.CompareTag("Player"))
        {
            GameManager.singleInstance.inPickUpRange = false;
            pickupPrompt.SetActive(false);
        }
    }
}
