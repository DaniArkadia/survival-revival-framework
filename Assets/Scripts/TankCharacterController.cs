using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class TankCharacterController : MonoBehaviour
{
   [SerializeField] float walkSpeed;
   [SerializeField] float turnSpeed;

   CharacterController characterController;

   void Awake()
   {
      characterController = GetComponent<CharacterController>();
   }

   void Update()
   {
      var forwardVelocity = Input.GetAxis("Vertical") * transform.forward * walkSpeed * Time.deltaTime;
      var turnVelocity = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

      characterController.Move(forwardVelocity);
      transform.Rotate(0, turnVelocity, 0);
   }

}
