﻿using Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public class LockCameraX : CinemachineExtension
    {
    [Tooltip("Lock the camera's X position to this value")]
    public float m_XPosition = 10;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = m_XPosition;
            state.RawPosition = pos;
        }
    }
}

    private void Update()
    {
        if (SnakeMovement.Instance.bodyParts.Count == 0)
            return;

        virtualCamera.m_Follow = SnakeMovement.Instance.bodyParts[0];
        virtualCamera.m_LookAt = SnakeMovement.Instance.bodyParts[0]; 

    }
}



//  using UnityEngine;
//  public class CameraController : MonoBehaviour
//  {  
//      #region Variables
//      public Transform m_Target;
//      public float m_Height = 25f;
//      public float m_Distance = 15f;
//      public float m_Angle = 45f;
//      #endregion

//     #region Main Methods
//     private void Start()
//     {
//         HandleCamera();
//     }
//     private void Update()
//     {
//         HandleCamera();
//     }
//     private void LateUpdate()
//     {
//         if (SnakeMovement.Instance.bodyParts.Count == 0)
//             return;

//         m_Target = SnakeMovement.Instance.bodyParts[0];   
//     }

//     #endregion
    
//     #region Helper Methods
//     protected virtual void HandleCamera()
//     {
//         if (m_Target == null)
//             return;
            
//         //Build world position vector
//         Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
//         Debug.DrawLine(m_Target.position, worldPosition, Color.red);

//         //Build rotated vector
//         Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
//         Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

//         //Move our position
//         Vector3 flatTargetPosition = m_Target.position;
//         flatTargetPosition.y = 0f;
//         Vector3 finalPosition = flatTargetPosition + rotatedVector;
//         Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

//         transform.position = Vector3.Lerp(transform.position, finalPosition, 0.125F);
//         //transform.LookAt(m_Target);
//     }
//     #endregion
// }