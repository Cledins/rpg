using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UIPanels;

    [SerializeField]
    private ThirdPersonOrbitCamBasic playerCameraScript;

    private float defaultHorizontalAimingSpeed;
    private float defaultVerticalAimingSpeed;

    [HideInInspector]
    public bool atLeastOnePanelOpen;
    void Start()
    {
        defaultHorizontalAimingSpeed = playerCameraScript.horizontalAimingSpeed;
        defaultVerticalAimingSpeed = playerCameraScript.verticalAimingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        atLeastOnePanelOpen = UIPanels.Any((panel) => panel == panel.activeSelf);
        if (atLeastOnePanelOpen) 
        {
            playerCameraScript.horizontalAimingSpeed = 0;
            playerCameraScript.verticalAimingSpeed = 0;
        } else
        {
            playerCameraScript.horizontalAimingSpeed = defaultHorizontalAimingSpeed;
            playerCameraScript.verticalAimingSpeed = defaultVerticalAimingSpeed;
        }
    }
}
