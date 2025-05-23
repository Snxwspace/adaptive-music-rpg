using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        imageLoader.sprite = fallbackSprite;    // idk why this errors i think its harmless just ignore it
    }

    // Update is called once per frame
    void Update()
    {
        if(!SceneManager.GetActiveScene().name.StartsWith("B")) {
            gameObject.SetActive(false);
        }
        
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
