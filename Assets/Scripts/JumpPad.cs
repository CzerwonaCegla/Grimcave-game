using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce = 20f;
    [SerializeField] private AudioSource jumpPadSound;
    public Animator anim;
    [SerializeField] private bool leftBounce = false;
    [SerializeField] private bool rightBounce = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jumpPadSound.Play();
            anim.SetBool("activate", true);
            if (leftBounce == true)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bounce, ForceMode2D.Impulse);
            }
            else if (rightBounce == true)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bounce, ForceMode2D.Impulse);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
            StartCoroutine(DeactivateAnimationAfterDelay());
        }
    }

    private IEnumerator DeactivateAnimationAfterDelay()
    {
        yield return new WaitForSeconds(0.7f);

        anim.SetBool("activate", false);
    }
}
