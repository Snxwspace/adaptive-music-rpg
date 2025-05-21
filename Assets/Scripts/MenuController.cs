using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Image[] listSprites;
    public GameObject[] listObjects;
    public MenuHighlightHandler[] listScripts;
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listSprites = gameObject.GetComponentsInChildren<Image>(true);
        listObjects = new GameObject[listSprites.Length];
        listScripts = new MenuHighlightHandler[listObjects.Length];
        for(int i = 0; i < listSprites.Length; i++) {
            listObjects[i] = listSprites[i].gameObject;
        }
        for(int i = 0; i < listObjects.Length; i++) {
            listScripts[i] = listObjects[i].GetComponent<MenuHighlightHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!SceneManager.GetActiveScene().name.StartsWith("B")) {
            gameObject.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.A)) {
            if (index == 0) {
                index = listObjects.Length - 1;
            } else {
                index -= 1;
            }
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (index == listObjects.Length - 1) {
                index = 0;
            } else {
                index += 1; 
            }
        }

        RefreshList();
    }

    private void RefreshList() {
        for(int i = 0; i < listObjects.Length; i++) {
            if(i == index) {
                listSprites[i].sprite = listScripts[i].selected;
            } else {
                listSprites[i].sprite = listScripts[i].deselected;
            }
        }
    }
}
