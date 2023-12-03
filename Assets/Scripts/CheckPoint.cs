using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool WasTriggered = false;
    public Sprite newCheckpointSprite;
    private SpriteRenderer checkpointSpriteRenderer;
    [SerializeField] private AudioSource activationSound;
    Animator anim;

    private void Start()
    {
        checkpointSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TriggerChange()
    {
        if (WasTriggered == false)
        {
            activationSound.Play();
        }
        checkpointSpriteRenderer.sprite = newCheckpointSprite;
        //anim.SetTrigger("activation");
        WasTriggered = true;
    }
}
