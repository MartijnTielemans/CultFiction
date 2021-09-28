using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    MenuManager manager;

    bool doOnce;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            doOnce = true;
        }
        else if (EventSystem.current.currentSelectedGameObject != this.gameObject)
        {
            if (doOnce)
            {
                manager.PlaySound(0);
                doOnce = false;
            }
        }
    }

    public void ChangeScene(string scene)
    {
        if (manager.canMove)
        {
            manager.ChangeScene(scene, 2f);
            manager.PlaySound(1);
        }
    }

    public void ShowControls()
    {
        if (manager.canMove)
        {
            manager.ActivateControlsScreen();
            manager.PlaySound(1);
        }
    }

    public void ExitApplication()
    {
        if (manager.canMove)
        {
            manager.QuitApplication(1f);
            manager.PlaySound(1);
        }
    }
}
