using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multilanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        LocalizationManager.Language = "Ukrainian";
    }
}
