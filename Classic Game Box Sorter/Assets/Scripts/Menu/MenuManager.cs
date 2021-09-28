using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameControls controls;
    SoundManager soundManager;

    [SerializeField] Animator fade;
    [SerializeField] Animator controlsAnim;
    [SerializeField] Button[] buttons;
    public bool controlsScreenActive;
    public bool canMove = false;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new GameControls();
        }

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (controls.UI.Cancel.triggered)
        {
            if (controlsScreenActive)
            {
                StartCoroutine(RemoveControlsScreen(1f));
            }
        }
    }

    public void ActivateControlsScreen()
    {
        canMove = false;
        controlsScreenActive = true;
        SetButtonInteractable(false);

        // Play controls screen animation
        controlsAnim.Play("Controls_In");
    }

    IEnumerator RemoveControlsScreen(float time)
    {
        controlsScreenActive = false;

        // Play animation to remove controls screen
        controlsAnim.Play("Controls_Out");

        yield return new WaitForSeconds(time);

        // Set buttons to be interactable again
        SetButtonInteractable(true);
        canMove = true;

        // Select a button again so input works
        buttons[1].Select();
    }

    public void ChangeScene(string scene, float time)
    {
        StartCoroutine(ChangeSceneSequence(scene, time));
    }

    IEnumerator ChangeSceneSequence(string scene, float time)
    {
        canMove = false;
        SetButtonInteractable(false);
        FadeOutSound(soundManager.Music, 1.8f);

        yield return new WaitForSeconds(time);
        fade.Play("Fade_In");

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

    public void QuitApplication(float time)
    {
        StartCoroutine(QuitApplicationSequence(time));
    }

    IEnumerator QuitApplicationSequence(float time)
    {
        fade.Play("Fade_In");
        canMove = false;
        SetButtonInteractable(false);
        FadeOutSound(soundManager.Music, 0.9f);

        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    void SetButtonInteractable(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = value;
        }
    }

    public void PlaySound(int index)
    {
        soundManager.PlaySound(soundManager.menuSounds[index]);
    }

    void FadeOutSound(AudioSource sound, float duration)
    {
        soundManager.StartFadeOut(sound, duration);
    }
}