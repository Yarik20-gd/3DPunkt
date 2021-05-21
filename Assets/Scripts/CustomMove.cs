using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CustomMove : MonoBehaviour
{

    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, horizontal * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * vertical;
        controller.SimpleMove(forward * curSpeed);

        if(controller.velocity == Vector3.zero)
        {
         GetComponent<Animator>().SetBool("stay",true);
        }
         if(controller.velocity != Vector3.zero)
        {
         GetComponent<Animator>().SetBool("stay",false);
        }


        if(Input.GetMouseButtonDown(1))
        {
            GetComponent<Animator>().SetBool("aim",true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            GetComponent<Animator>().SetBool("aim",false);
        }

    }
}
