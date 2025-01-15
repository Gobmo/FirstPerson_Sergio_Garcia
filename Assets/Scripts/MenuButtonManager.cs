using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
