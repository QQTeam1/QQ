using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void UnmuteToggle(bool muted)
    {
        if (muted)
        { 
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
