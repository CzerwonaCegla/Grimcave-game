using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce = 20f;
    [SerializeField] private AudioSource jumpPadSound;
    public Animator anim;

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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            StartCoroutine(DeactivateAnimationAfterDelay());
        }
    }

    private IEnumerator DeactivateAnimationAfterDelay()
    {
        yield return new WaitForSeconds(0.7f);

        anim.SetBool("activate", false);
    }
}
