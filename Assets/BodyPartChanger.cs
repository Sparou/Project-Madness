using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BodyPartChanger : MonoBehaviour
{
    public int bodyPartIndex; // Индекс части тела, которую нужно поменять
    public int newBodyPartOptionIndex; // Индекс новой опции части тела

    private BodyPartsSelector bodyPartsSelector;
    private void Start()
    {
        // Получаем компонент BodyPartsSelector на этом же объекте
        bodyPartsSelector = GetComponent<BodyPartsSelector>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            //bodyPartsSelector.NextBodyPart(bodyPartIndex);
            bodyPartsSelector.NextBodyPart(bodyPartIndex);
            //bodyPartsSelector.ChangeBodyPart(bodyPartIndex, newBodyPartOptionIndex);
    }
}