using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform[] swords = new Transform[5];

    private Character character;
    private void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i = 0; i < swords.Length; i++)
        {
            swords[i] = transform.GetChild(i);            
        }
    }
    public void Refresh()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            if (i < character.Lives) swords[i].gameObject.SetActive(true);
            else swords[i].gameObject.SetActive(false);
        }
    }
}
