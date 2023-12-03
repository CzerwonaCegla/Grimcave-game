using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    public PlatformStateSprites[] sprites;
    [SerializeField] private PlatformStages currentStage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentStage == PlatformStages.Idle)
        {
            StartCoroutine(PlatformChangingCor());
        }
    }

    public void NextPlatformStage()
    {
        PlatformStages newState = currentStage + 1;
        if (newState >= PlatformStages.count)
        {
            newState = PlatformStages.Idle;
        }
        PlatformChange(newState);
    }

    //Zmienia stany
    private IEnumerator PlatformChangingCor()
    {
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        NextPlatformStage();

        //recharge platformy i jej respawn
        yield return new WaitForSeconds(3f);
        NextPlatformStage();
    }

    // Zmiana stagow
    void PlatformChange(PlatformStages state)
    {
        currentStage = state;
        DoStuffWithState();
    }

    private void DoStuffWithState()
    {
        switch (currentStage)
        {
            case PlatformStages.Idle:
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    break;
                }
            case PlatformStages.Broken:
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    break;
                }
        }

        foreach(var sprite in sprites)
        {
            if(sprite.state == currentStage)
            {
                GetComponent<SpriteRenderer>().sprite = sprite.sprite;
                break;
            }
        }
    }
}

public enum PlatformStages 
{ 
    Idle,
    Breaking1,
    Breaking2,
    Breaking3,
    Breaking4,
    Breaking5,
    Broken,
    //Respawn1,
    //Respawn2,
    //Respawn3,

    count
}

[System.Serializable]
public struct PlatformStateSprites
{
    public PlatformStages state;
    public Sprite sprite;
}
