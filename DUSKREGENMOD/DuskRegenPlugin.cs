using BepInEx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

[BepInPlugin("com.newblood.plugins.dusk.regen", "DUSK Regen Mod", "1.0.0.0")]
public class DuskRegenPlugin : BaseUnityPlugin
{
    float regenTimer;

    float currentWaveValue = 1;

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

        if (endlessScene.name == "EndlessArena1" || endlessScene.name == "EndlessArena2" || endlessScene.name == "EndlessArena3")
        {
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }

            if (regenTimer <= 0f)
            {
                if (playerHP.myhealth < 50f)
                {
                    playerHP.myhealth += 10f;
                }
            regenTimer = 5f;
            }

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
