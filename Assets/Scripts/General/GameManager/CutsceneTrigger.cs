using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Camera playerCamera;
    public CinemachineCamera cutsceneCamera;

    private void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnCutsceneEnd;
        }

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
        if (cutsceneCamera != null)
        {
            cutsceneCamera.Priority = 0;
        }

        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(true);
        }
    }
}
