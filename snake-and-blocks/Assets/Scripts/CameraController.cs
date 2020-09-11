﻿ using UnityEngine;
 public class CameraController : MonoBehaviour
 {

     #region Variables
     public Transform m_Target;
     public float m_Height = 10f;
     public float m_Distance = 20f;
     public float m_Angle = 45f;
     #endregion

    #region Main Methods
    private void Start()
    {
        HandleCamera();
    }

    private void Update()
    {
        HandleCamera();
    }
    #endregion

    #region Helper Methods
    protected virtual void HandleCamera()
    {
        if (!m_Target)
        {
            return;
        }

        //Build world position vector
        Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
        Debug.DrawLine(m_Target.position, worldPosition, Color.red);

        //Build rotated vector
        Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
        Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

        //Move our position
        Vector3 flatTargetPosition = m_Target.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotatedVector;
        Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

        transform.position = finalPosition;
        transform.LookAt(m_Target);
    }
    #endregion
}