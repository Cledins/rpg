using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isMenuOpen = false;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private ThirdPersonOrbitCamBasic cameraScript;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;

            pauseMenu.SetActive(isMenuOpen);
            optionsMenu.SetActive(false);

            Time.timeScale = isMenuOpen ? 0 : 1;
            cameraScript.enabled = !isMenuOpen;
        }
    }
}
