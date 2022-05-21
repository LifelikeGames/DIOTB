using System;
using UnityEngine;

namespace VitaSoftware
{
    public class ExitGame : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}