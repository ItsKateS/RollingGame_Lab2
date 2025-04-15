using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour
{
    public Image[] levelButtons;

    private void Start()
    {
        int currentLVL = PlayerPrefs.GetInt("currentLVL", 1);

        for(int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > currentLVL)
                levelButtons[i].GetComponent<Button>().interactable = false;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            PlayerPrefs.DeleteAll();
    }
}
