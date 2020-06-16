using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HalamanManager : MonoBehaviour
{
    public bool isEscapeToExit; 
    AudioSource audio;
    public AudioClip soundPlay;
    public AudioClip soundPause;
    public AudioClip soundFinish;
    GameObject panelSelesai;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = soundPlay;
        PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
            {
                Application.Quit();
            }
            else
            {
                KembaliKeMenu();
            }
        }


        panelSelesai = GameObject.Find("PanelSelesai");
        if (panelSelesai != null)
        {
            if (panelSelesai.activeInHierarchy)
            {
                audio = GetComponent<AudioSource>();
                audio.clip = soundFinish;
                PlaySound();
            }
        }

    }

    public void MulaiPermainan()
    {
        SceneManager.LoadScene("Main");
    }

    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PausePermainan()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = soundPause;
        PlaySound();
    }

    public void LanjutkanPermainan()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = soundPlay;
        PlaySound();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        audio.Play();
    }

}