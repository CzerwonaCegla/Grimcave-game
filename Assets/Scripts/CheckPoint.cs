using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool WasTriggered = false;
    public Sprite newCheckpointSprite;
    private SpriteRenderer checkpointSpriteRenderer;

    private void Start()
    {
        checkpointSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TriggerChange()
    {
        WasTriggered = true;
        checkpointSpriteRenderer.sprite = newCheckpointSprite;
    }
}
