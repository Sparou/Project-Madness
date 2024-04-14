using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------ Audio Sorce -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------ Audio Clip -----------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip attack;
    public AudioClip walking;
    public AudioClip door;
    public AudioClip chest;
}
