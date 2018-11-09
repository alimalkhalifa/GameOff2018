using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed = 6f;

    private CharacterController body;
    private Animator anim;

	void Start () {
        body = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}

    void Update() {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var mouse_pos = Input.mousePosition;
        var mouse_ray = Camera.main.ScreenPointToRay(mouse_pos);
        RaycastHit hit;
        if ( Physics.Raycast(mouse_ray, out hit, 1000f) ){
            var dir = Vector3.Scale(hit.point - transform.position, new Vector3(1f, 0f, 1f));
            transform.rotation = Quaternion.LookRotation(dir);
        }

        var movement = Quaternion.LookRotation(Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f))) * new Vector3(horizontal, 0f, vertical).normalized;
        var velocity = movement * speed;
        body.SimpleMove(velocity);

        if (velocity.sqrMagnitude > 0f) {
            anim.SetBool("Moving", true);
            var walkanim_direction = Quaternion.Inverse(transform.rotation) * (velocity / speed);
            anim.SetFloat("WalkForward", walkanim_direction.z);
            anim.SetFloat("WalkRight", walkanim_direction.x);
        } else {
            anim.SetBool("Moving", false);
        }
    }
    }