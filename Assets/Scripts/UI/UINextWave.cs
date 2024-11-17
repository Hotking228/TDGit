using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINextWave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusAmount;
    private EnemyWaveManager manager;
    private float timeToNextWave;
    private void Start()
    {
        manager = FindAnyObjectByType<EnemyWaveManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {
            timeToNextWave = time;
        };
    }

    public void CallWave()
    {
        manager.ForceNextWave();
    }


    private void Update()
    {
        var bonus = (int)timeToNextWave;
        if (bonus < 0) bonus = 0;
        bonusAmount.text =(bonus).ToString();
        timeToNextWave -= Time.deltaTime;
    }
}
