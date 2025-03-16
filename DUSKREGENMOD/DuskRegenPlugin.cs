using BepInEx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

[BepInPlugin("com.newblood.plugins.dusk.regen", "DUSK Regen Mod", "1.0.0.0")]
public class DuskRegenPlugin : BaseUnityPlugin
{
    float regenTimer; //Cooldown for health regen
    float currentWaveValue = 1; //Current wave reference thing

    void Awake()
    {
    }

    void Update()
    {
        GameObject plrRef = GameObject.Find("Player");

        GameObject endRef = GameObject.Find("Endless Mode Controller");

        PlayerHealthManagement playerHP = plrRef.GetComponent<PlayerHealthManagement>();

        EndlessModeSpawnerControllerScript endlessWave = endRef.GetComponent<EndlessModeSpawnerControllerScript>();

        Scene endlessScene = SceneManager.GetActiveScene();

        //Check if the map is indeed an endless map
        if (endlessScene.name == "EndlessArena1" || endlessScene.name == "EndlessArena2" || endlessScene.name == "EndlessArena3")
        {
            //Countdown for health regen cooldown (idk why I formated it like this)
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }

            //If the cooldown hits 0 and the player is below 50HP then increase it by 10, if not then don't do that lmao
            if (regenTimer <= 0f)
            {
                if (playerHP.myhealth < 50f)
                {
                    playerHP.myhealth += 10f;
                }
            regenTimer = 5f;
            }

            //If the the current wave reference doesn't match the current wave then heal the player back to 100HP (There's probably a better way of doing this that I don't know about)
            if (currentWaveValue != endlessWave.currentwave)
            {
                if (playerHP.myhealth < 100f)
                {
                    playerHP.myhealth = 100f;
                }
                currentWaveValue = endlessWave.currentwave;
            }
        }
    }
}
