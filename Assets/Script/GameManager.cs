using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;

    [SerializeField] GameObject _pauseMenuPanel;
    Animator _anim;


    void Start()
    {
        _anim = GameObject.Find("Pause_Menu_panel").GetComponent<Animator>();
        _anim.updateMode = AnimatorUpdateMode.UnscaledTime;  //bỏ qua TimeScale


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);


        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuPanel.SetActive(true);
            _anim.SetBool("Paused", true);
            Time.timeScale = 0; //dung game
        }


    }


    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }


}
