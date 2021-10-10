using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;
    public Transform cam;
    public Rigidbody rb;

    public float speed = 6.0f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float vSpeed = 0.0f;
    public float gravity = 9.8f;

    int coinCount = 0;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //vSpeed -= gravity * Time.deltaTime;
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            //rb.AddForce(moveDir.normalized * speed * Time.deltaTime);

            moveDir.y += vSpeed;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (coinCount == 10)
        {
            SceneManager.LoadScene("End");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Coin")
        {
            Debug.Log("Hit!");
            Destroy(col.gameObject);
            coinCount++;
        }
    }
}
