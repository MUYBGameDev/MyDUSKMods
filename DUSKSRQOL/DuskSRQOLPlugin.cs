using BepInEx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking;
using Steamworks;
using UnityEngine.UI;
using System.Transactions;

[BepInPlugin("com.newblood.plugins.dusk.srqol", "DUSK Speedrunning QOL Mod", "1.0.0.0")]
public class DuskSRQOLPlugin : BaseUnityPlugin
{
    float speedometerType; //Speedometer type (idk how else to explain this lol)
    float speedometerSpeed; //Speedometer speed value
    float SecondarySpeedometerSpeed; //Other speedometer speed value (only used for the last speedometer type)
    string speedometerDisplay; //Speedometer string
    bool permStatus; //Persistent level stats bool
    bool speedDisplayType; //Speedometer display type (idk how else to explain this one too)
    //Text linger fix stuff {
    bool textLingerFix;
    float textLingerFixTimer;
    //}
    void Awake()
    {
        speedometerType = 0f;
        speedDisplayType = true;
        textLingerFix = false;
        textLingerFixTimer = 0.01f;
    }

    void Update()
    {
        var plrRef = GameObject.Find("Player");
        var lvlName = GameObject.Find("Canvas/SmallLevelName");
        var lvlDifficulty = GameObject.Find("Canvas/SmallDifficultyLabel");
        var lvlTime = GameObject.Find("Canvas/LevelTime");
        var lvlKills = GameObject.Find("Canvas/KillLabel");
        var lvlSecret = GameObject.Find("Canvas/SecretsLabel");
        var playerSpeed = plrRef.GetComponent<CharacterController>();
        var StatsD = lvlName.GetComponent<TextMeshProUGUI>();
        var StatsN = lvlDifficulty.GetComponent<TextMeshProUGUI>();
        var StatsT = lvlTime.GetComponent<TextMeshProUGUI>();
        var StatsK = lvlKills.GetComponent<TextMeshProUGUI>();
        var StatsS = lvlSecret.GetComponent<TextMeshProUGUI>();
        
        speedometerDisplay = speedometerSpeed.ToString(); //Display the speed onto the message

        //Speedometer type toggle
        if(Input.GetKeyDown(KeyCode.F9))
        {
            if (speedometerType < 4f)
                speedometerType += 1f;
            else
            {
                speedometerType = 0;
            }
        }

        //Speedometer display type toggle
        if(Input.GetKeyDown(KeyCode.F10))
        {
            if (speedDisplayType)
                speedDisplayType = false;
            else
                speedDisplayType = true;
                textLingerFix = true;
                textLingerFixTimer = 0.01f;
        }

        //Persistent level stats toggle
        if(Input.GetKeyDown(KeyCode.F11))
        {
            if (permStatus)
                permStatus = false;
            else
                permStatus = true;
        }

        //More text linger fix stuff
        if (textLingerFix && textLingerFixTimer == 0)
        {
            textLingerFix = false;
        }

        //Speedometer type stuff
        if (speedometerType == 1f) //Overall units
        {
            speedometerSpeed = Mathf.Round(playerSpeed.velocity.magnitude * 1f);

            SpeedometerShow(speedometerDisplay + "u");
        }
        if (speedometerType == 2f) //Horizontal units
        {
            speedometerSpeed = Mathf.Round(Mathf.Abs(playerSpeed.velocity.x + playerSpeed.velocity.z * 1f));

            SpeedometerShow(speedometerDisplay + "H/u");
        }
        if (speedometerType == 3f) //Vertical units
        {
            speedometerSpeed = Mathf.Round(Mathf.Abs(playerSpeed.velocity.y * 1f));

            SpeedometerShow(speedometerDisplay + "V/u");
        }
        if (speedometerType == 4f) //Horizontal + Vertical units
        {
            speedometerSpeed = Mathf.Round(Mathf.Abs(playerSpeed.velocity.x + playerSpeed.velocity.z * 1f));
            SecondarySpeedometerSpeed = Mathf.Round(Mathf.Abs(playerSpeed.velocity.y * 1f ));

            SpeedometerShow(speedometerDisplay + "H/u " + SecondarySpeedometerSpeed + "V/u");
        }

        //Show the level stats if persistent level stats is enabled
        if (permStatus)
        {
            StatsN.alpha = 0.8f;
            StatsD.alpha = 0.8f;
            StatsT.alpha = 0.8f;
            StatsK.alpha = 0.8f;
            StatsS.alpha = 0.8f;
        }

        if (textLingerFixTimer > 0) textLingerFixTimer -= Time.deltaTime; //Timer countdown
    }

    void SpeedometerShow(string message)
    {
        var Ttext    = GameObject.Find("TutorialMessageText");
        var Mtext    = GameObject.Find("MessageText");
        var Ttmpro   = Ttext.GetComponent<TextMeshProUGUI>();
        var Mtmpro   = Mtext.GetComponent<TextMeshProUGUI>();
        var Ttimer   = Ttext.GetComponent<ClearMessageAfterTime>();
        var Mtimer   = Mtext.GetComponent<ClearMessageAfterTime>();

        //If there's no speedometer then revert text back to default colors
        if (speedometerType == 0)
        {
            Ttmpro.color = new Color(1f, 0.1324f, 0.1324f, 1f);
            Mtmpro.color = new Color(0.5735f, 0f, 0f, 1f);
        }

        if (speedDisplayType)
        {
            Ttmpro.text  = message;
            Ttimer.timer = Ttimer.defaulttime;

            if (speedometerType != 0) //If there's a speedometer and the display is true then make the tutorial prompt text white
            {
                Ttmpro.color = Color.white;
                Mtmpro.color = new Color(0.5735f, 0f, 0f, 1f);
            }
        }
        else
        {
            Mtmpro.text  = message;
            Mtimer.timer = Mtimer.defaulttime;

            if (speedometerType != 0) //If false then make the message prompt text white
            {
                Ttmpro.color = new Color(1f, 0.1324f, 0.1324f, 1f);
                Mtmpro.color = Color.white;
            }
        }

        //Even more text linger fix bullshit
        if (textLingerFix)
        {
            if (speedometerType != 0)
            {
                if (speedDisplayType)
                    Mtimer.timer = 0;
                else
                    Ttimer.timer = 0;
            }
        }
    }
}
