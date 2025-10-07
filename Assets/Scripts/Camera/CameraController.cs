using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera maxCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            maxCam.gameObject.SetActive(!maxCam.gameObject.activeSelf);
        }
    }
}
