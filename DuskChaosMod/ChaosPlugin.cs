using BepInEx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

[BepInPlugin("com.newblood.plugins.dusk.chaos", "DUSK Chaos Mod", "0.1.0.0")]
public class ChaosPlugin : BaseUnityPlugin
{
    float eventTimer; //Timer for when events happen
    int eventType; //Value for which event is triggered
    int rngWpn; //Value for the "Free Weapon" event that decides which weapon to give you

    void Awake()
    {
        eventType = 0;
        eventTimer = 15f;
        rngWpn = 0;
    }

    void Update()
    {
        //If the eventTimer reaches 0 then activate a random event
        if (eventTimer <= 0f)
        {
            //Normal code
            eventType = Random.Range(1, 12);
            
            //Debug code
            //eventType = 10;

            eventTrigger();
        }

        if (eventTimer > 0f) eventTimer -= Time.deltaTime; //eventTimer countdown
    }

    void eventTrigger()
    {
        //Player stuff
        var plrRef = GameObject.Find("Player");
        var wpnRef = GameObject.Find("Player/MainCamera/PlayerHand/WeaponAnimator");
        var playerStatus = plrRef.GetComponent<PlayerHealthManagement>();
        var climbPower = plrRef.GetComponent<ClimbingPowerupScript>();
        var playerController = plrRef.GetComponent<MyControllerScript>();
        var attackController = wpnRef.GetComponent<AttackScript>();
        var weaponSelection = wpnRef.GetComponent<SelectionScript>();

        if (eventType == 1)
        {
            //Do the trigger
            ShowMessage("STEROIDS");
            playerStatus.myhealth = 200f;
            playerStatus.myarmor = 200f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 2)
        {
            //Do the trigger
            ShowMessage("fuck you :)");
            playerStatus.myhealth = 1f;
            playerStatus.myarmor = 0f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 3)
        {
            //Do the trigger
            ShowMessage("Drunk");
            playerStatus.drunkness = 4;
            playerStatus.drunkentimer = 30f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 4)
        {
            //Do the trigger
            ShowMessage("High gravity");
            Physics.gravity = new Vector3(0, -19.62f, 0);
            playerController.gravity = 0.026f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 5)
        {
            //Do the trigger
            ShowMessage("Low Gravity");
            Physics.gravity = new Vector3(0, -2.4525f, 0);
            playerController.gravity = 0.00325f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 6)
        {
            //Do the trigger
            ShowMessage("Blistering Heat");
            playerController.superhot = true;
            playerController.superhottimer = 30f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 7)
        {
            //Do the trigger
            ShowMessage("F A S T");
            attackController.firespeed = 20f;
            attackController.firespeedtimer = 30f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 8)
        {
            //Do the trigger
            ShowMessage("Pickpocketed");
            weaponSelection.weaponinventory[0] = false;
            weaponSelection.weaponinventory[1] = false;
            weaponSelection.weaponinventory[2] = false;
            weaponSelection.weaponinventory[3] = false;
            weaponSelection.weaponinventory[4] = false;
            weaponSelection.weaponinventory[5] = false;
            weaponSelection.weaponinventory[6] = false;
            weaponSelection.weaponinventory[7] = false;
            weaponSelection.weaponinventory[8] = false;
            weaponSelection.weaponinventory[9] = false;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 9)
        {
            //Do the trigger
            ShowMessage("Free weapon");
            rngWpn = Random.Range(0, 9);
            weaponSelection.weaponinventory[rngWpn] = true;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 10)
        {
            //Do the trigger
            ShowMessage("Normal gravity");
            Physics.gravity = new Vector3(0, -9.81f, 0);
            playerController.gravity = 0.013f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 11)
        {
            //Do the trigger
            ShowMessage("Spiderman");
            climbPower.havepower = true;
            climbPower.powertimer = 30f;

            //Reset the values
            eventType = 0;
            eventTimer = 15f;
        }
        if (eventType == 12) //This is just here because of some fuckery with the event rng call
        {
            eventType = Random.Range(1, 12);
            eventTrigger();
        }
    }

    //Display the current event on screen
    void ShowMessage(string message)
    {
        var text    = GameObject.Find("SecretText");
        var tmpro   = text.GetComponent<TextMeshProUGUI>();
        var timer   = text.GetComponent<ClearMessageAfterTime>();
        tmpro.text  = message;
        timer.timer = timer.defaulttime;
    }
}

//Hello cass
