using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VitaSoftware.Audio;

namespace VitaSoftware.General
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject menuWindow;
        [SerializeField] private MusicManager musicPlayer;
        [SerializeField] private SoundManager soundPlayer;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;

        private void Awake()
        {
            ToggleMenu();
            soundToggle.isOn = soundPlayer.IsSoundEnabled;
            musicToggle.isOn = musicPlayer.IsMusicActive;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            menuWindow.SetActive(!menuWindow.activeSelf);
            Time.timeScale = menuWindow.activeSelf ? 0 : 1;
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OnSoundChanged(bool value)
        {
            soundPlayer.ToggleSounds(value);
        }

        public void OnMusicChanged(bool value)
        {
            if(value)
                musicPlayer.EnableMusic();
            else
                musicPlayer.DisableMusic();
        }
    }
}