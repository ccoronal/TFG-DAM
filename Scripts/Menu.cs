using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu, opciones, info;

    // ir a ver opciones
    public void verOpciones()
    {
        menu.SetActive(false);
        info.SetActive(false);
        opciones.SetActive(true);
    }

    // ir a ver información extra
    public void verInfo()
    {
        menu.SetActive(false);
        opciones.SetActive(false);
        info.SetActive(true);
    }

    // volver al menú
    public void verMenu()
    {
        opciones.SetActive(false);
        info.SetActive(false);
        menu.SetActive(true);
    }

    // entrar al juego per se (nivel 1)
    public void Jugar(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    // salir del juego
    public void Salir()
    {
        Application.Quit();
    }

}
