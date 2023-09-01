using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void  LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadCoopGame()
    {
        SceneManager.LoadScene(2);
    }

    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
