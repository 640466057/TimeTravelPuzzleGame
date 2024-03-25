using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public int selectedKeyIndex = -1;
    public List<GameObject> keyTexts = new List<GameObject> { };
    [SerializeField] KeyCode oldKey;
    [SerializeField] KeyCode currentKey;

    void OnGUI()
    {
        Event e = Event.current;
        currentKey = e.keyCode;
    }

    private void Start()
    {
        int i = 0;
        foreach (KeyCode key in StaticControls.keys)
        {
            keyTexts[i++].TryGetComponent(out TextMeshProUGUI text);
            text.text = key.ToString();
        }
    }

    void Update()
    {
        if (currentKey != oldKey)
        {
            oldKey = currentKey;
            if (currentKey != KeyCode.None)
            {
                ChangeKey(currentKey);
            }
        }
    }

    public void SetKeyIndex(int index)
    {
        selectedKeyIndex = index;
    }

    public void ChangeKey(KeyCode key)
    {
        keyTexts[selectedKeyIndex].TryGetComponent(out TextMeshProUGUI text); 
        text.text = key.ToString();
        StaticControls.keys[selectedKeyIndex] = key;
        selectedKeyIndex = -1;
    }
}
