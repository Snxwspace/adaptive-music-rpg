using UnityEngine;
using TMPro;
using System.Reflection;

public class UpdatePlaceholderUI : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textMP;
    public GameObject prefab;
    private StatHandler characterStatHandler;
    public GameObject textObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterStatHandler = gameObject.GetComponent<StatHandler>();

        if(textHP == null) {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            CanvasBehavior canvasBehavior = canvas.GetComponent<CanvasBehavior>();
            textObject = canvasBehavior.InstantiateNewHPText(prefab);
            textHP = textObject.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = ((int)characterStatHandler.currentHP).ToString();
        if(textMP != null) {
            textMP.text = ((int)characterStatHandler.currentMP).ToString();
        }
    }
}
