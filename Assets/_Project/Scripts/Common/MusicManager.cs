using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceCalm;
    [SerializeField] private AudioSource audioSourceBattle;
    [SerializeField] private AudioClip trackCalm;
    [SerializeField] private AudioClip trackBattle;

    [SerializeField] private float changeVolumeMultiplier = 2f;

    private float targetCalmVolume = 1f;
    private float targetBattleVolume = 0f;


    // Update is called once per frame
    void Update()
    {
        // Смена громкости треков в зависимости от наличия врагов
        if (EnemyCounter.Instance != null)
        {
            if (EnemyCounter.Instance.GetCountOfEnemies() > 0)
            {
                targetCalmVolume = 0f;
                targetBattleVolume = 1f;
            }
            else
            {
                targetCalmVolume = 1f;
                targetBattleVolume = 0f;
            }

            audioSourceCalm.volume = Mathf.Lerp(audioSourceCalm.volume, targetCalmVolume, Time.deltaTime * changeVolumeMultiplier);
            audioSourceBattle.volume = Mathf.Lerp(audioSourceBattle.volume, targetBattleVolume, Time.deltaTime * changeVolumeMultiplier);
        }
    }
}
