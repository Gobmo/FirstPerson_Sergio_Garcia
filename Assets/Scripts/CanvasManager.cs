using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Jugador player;
    [SerializeField] RoundsController roundsController;
    [SerializeField] GameObject settings, panelMuerte;
    [SerializeField] TextMeshProUGUI textoNumRondas;

    private bool activo = true;

    void Start()
    {
        settings.SetActive(false);
        panelMuerte.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        activo = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && activo == true)
        {
            AlternarRaton();
        }
    }

    void AlternarRaton()
    {
        if (Cursor.visible)
        {
            // Ocultar el cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        else
        {
            // Mostrar el cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void MostrarAjustes()
    {
        if (activo == true)
        {
            settings.SetActive(true); 
        }
    }

    public void Reanudar()
    {
        settings.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void VolverAlMenuInicial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuInicial");
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }

    public void MostrarPanelMuerte()
    {
        settings.SetActive(false);
        panelMuerte.SetActive(true);
        textoNumRondas.text = "You have survived " + roundsController.NumeroRonda + " rounds!";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        activo = false;
    }
}
