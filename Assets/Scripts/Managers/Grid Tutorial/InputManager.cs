using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;



    [SerializeField]
    
    //To decide which plane takes part on the input detection of the mouse position on our plane
    private LayerMask placementaLayermask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane; //Does not allow to select objects that are not rendered by the camera 
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementaLayermask))
        {
            lastPosition = hit.point;

        }
        return lastPosition;
    }

}
