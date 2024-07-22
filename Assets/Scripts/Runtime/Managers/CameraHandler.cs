using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] SubmitManager submitManager;
    [SerializeField] Transform tileParent, blockParent;
    [SerializeField] float offset = 20;
}
