using UnityEngine;

public class DepthObject : MonoBehaviour
{
   [SerializeField] Transform lookTarget;

   [ContextMenu("LookAtCamera")]
   public void LookAtCamera()
   {
      transform.rotation = Quaternion.LookRotation((lookTarget.position - transform.position).normalized, Vector3.up);
   }


   public void LookAtCamera(Transform cameraTransform)
   {
      //transform.LookAt(cameraTransform, Vector3.up);
   }
}