using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleEffects;

    public void Toggle()
    {
        if (_toggleMusic) SoundManager.instance.ToggleMusic();
        if (_toggleEffects) SoundManager.instance.ToggleEffects();
    }
}
