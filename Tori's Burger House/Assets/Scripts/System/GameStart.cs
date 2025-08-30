using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
