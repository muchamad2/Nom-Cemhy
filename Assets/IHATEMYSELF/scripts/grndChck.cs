using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grndChck : MonoBehaviour
{
    private readonly string grnd = "Ground";
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == grnd) {
            GetComponentInParent<plyerMov>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.tag == grnd) {
            GetComponentInParent<plyerMov>().isGrounded = false;
        }
    }
}
