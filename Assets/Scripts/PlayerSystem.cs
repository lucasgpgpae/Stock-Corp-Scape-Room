using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    public static PlayerSystem instance;

    public GameObject mobileController;
    public Button getItem;
    public GameObject centerObject;
    public bool interact;

    Ray ray;
    RaycastHit hit;

    void Awake() 
    {
        //Set the Instance
        if(instance == null) instance = this;
    }

    void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS // If is mobile

            // Activate the mobile controller
            mobileController.SetActive(true);
            // Activate get item button
            getItem.gameObject.SetActive(true);
            // Set it to not interactable
            getItem.interactable = false;

        #else // else (if isn't mobile)

            // Disable Mobile Controller
            mobileController.SetActive(false);
            // Set getItem button to interatable
            getItem.interactable = true;
            // Disable getItem button
            getItem.gameObject.SetActive(false);

        #endif // close if/else
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane)); //get a ray of the middle of screen
        if(Physics.Raycast(ray, out hit)) //If has object in middle of screen
        {
            if(interact) //If you clicked to interact
            {
                interact = false; //Set interact back to false
                
                if(hit.collider.gameObject.GetComponent<Button>()) //If object is a button
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke(); //Execute button onClick Action!
            }
        }
    }
}
