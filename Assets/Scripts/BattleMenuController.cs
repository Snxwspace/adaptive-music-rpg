using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuController : MonoBehaviour
{
    public Sprite fallbackSprite;
    public Sprite[] selectionSprites;
    public Image imageLoader;

    void Start()
    {
        imageLoader = gameObject.GetComponent<Image>();
    }

    // OnEnable is called every time the object gets enabled
    void OnEnable()
    {
        imageLoader.sprite = fallbackSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            int spriteIndex = Array.IndexOf(selectionSprites, imageLoader.sprite);
            if (spriteIndex == -1) {
                imageLoader.sprite = selectionSprites[0];
            }
            else if (spriteIndex == 0) {
                imageLoader.sprite = selectionSprites[^1];
            } else {
                imageLoader.sprite = selectionSprites[spriteIndex - 1];
            }
        } else if (Input.GetKeyDown(KeyCode.D)) {
            int spriteIndex = Array.IndexOf(selectionSprites, imageLoader.sprite);
            if (spriteIndex == -1) {
                imageLoader.sprite = selectionSprites[0];
            }
            else if (spriteIndex == selectionSprites.Length - 1) {
                imageLoader.sprite = selectionSprites[0];
            } else {
                imageLoader.sprite = selectionSprites[spriteIndex + 1];
            }
        }
    }
}
