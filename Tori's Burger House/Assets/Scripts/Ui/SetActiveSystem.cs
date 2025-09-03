using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveSystem : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
