using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] AudioClip saveSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] Sprite breakableSprite;
    [SerializeField] GameObject[] goalkeepers;

    // cached reference
    Level level;

    // state variables
    [SerializeField] int timesHit; // TODO only serialized for debug

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit();
    }

    private void HandleHit()
    {
        if (tag == "Breakable")
        {
            timesHit++;
            int maxHits = hitSprites.Length + 1;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
                CheckGoalkeepers();
            }

            else
                ShowNextHitSprite();
        }
        if (tag == "Goalkeeper")
        {
            if (FindObjectOfType<Level>().numberOfBlocks>2)
            {
                AudioSource.PlayClipAtPoint(saveSound, Camera.main.transform.position, 0.1f);
            }

            else
            {
                DestroyBlock();
            }
        }
    }

    private void CheckGoalkeepers()
    {
        if (FindObjectOfType<Level>().numberOfBlocks <= 2)
        {
                ShowBreakableSprite();
        }
    }

    private void ShowBreakableSprite()
    {
        foreach (GameObject goalkeeper in goalkeepers)
        {
            goalkeeper.GetComponent<SpriteRenderer>().sprite = goalkeeper.GetComponent<Block>().breakableSprite;
        }

    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("In " + gameObject.name + " block sprite is missing from array");
    }

    private void DestroyBlock()
    {

        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
        level.BrokenBlock();
        FindObjectOfType<GameSession>().AddToScore();
        TriggerSparklesVFX();
        
    }

    private void Start()
    {
        CountBreakableBlocks();
        goalkeepers = GameObject.FindGameObjectsWithTag("Goalkeeper");
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable" || tag=="Goalkeeper")
            level.CountBlocks();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

}
