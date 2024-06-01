using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BodyPartChanger : MonoBehaviour
{
    public int bodyPartIndex; // ������ ����� ����, ������� ����� ��������
    public int newBodyPartOptionIndex; // ������ ����� ����� ����� ����

    private BodyPartsSelector bodyPartsSelector;
    private void Start()
    {
        // �������� ��������� BodyPartsSelector �� ���� �� �������
        bodyPartsSelector = GetComponent<BodyPartsSelector>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            //bodyPartsSelector.NextBodyPart(bodyPartIndex);
            bodyPartsSelector.NextBodyPart(bodyPartIndex);
            //bodyPartsSelector.ChangeBodyPart(bodyPartIndex, newBodyPartOptionIndex);
    }
}