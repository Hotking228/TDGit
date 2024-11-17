using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyBag : MonoSingleton<MyBag>
{
    [SerializeField] private float maxMana;
    private float currentMana;
    public float CurrentMana => currentMana;
    [SerializeField] private Image manaInamge;
    [SerializeField] private float manaRestore = 10;

    
    private void Start()
    {
        currentMana = maxMana;
    }

    private void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += Time.deltaTime * manaRestore;
            if(currentMana >= maxMana)
                currentMana = maxMana;
            ChangeMana(0);
        }
    }

    public bool ChangeMana(float change)
    {
        if(change > currentMana) return false;

        currentMana -= change;

        manaInamge.fillAmount = currentMana / maxMana;
        return true;
    }

}
