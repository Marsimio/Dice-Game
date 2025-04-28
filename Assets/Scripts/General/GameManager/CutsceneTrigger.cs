using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Camera playerCamera;
    public CinemachineCamera cutsceneCamera;

    
    private void Awake()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (playableDirector != null)
        {
            playableDirector.stopped += OnCutsceneEnd;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(false);
        }

        if (cutsceneCamera != null)
        {
            cutsceneCamera.Priority = 10;
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            SwitchToPlayerCamera();
        }
    }

    private void SwitchToPlayerCamera()
    {
        playerCamera.gameObject.SetActive(true);
        GameObject.FindWithTag("MainCamera").SetActive(false);
    }
}
