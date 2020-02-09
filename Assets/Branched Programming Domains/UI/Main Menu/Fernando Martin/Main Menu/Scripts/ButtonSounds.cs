using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource mySound;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        mySound.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        mySound.PlayOneShot(clickSound);
    }
}
