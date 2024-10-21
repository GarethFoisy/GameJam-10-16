using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteAllocator : MonoBehaviour
{
    [SerializeField] private SpriteAtlas spriteAtlas;
    [SerializeField] private string spriteName;
    [SerializeField] private string[] spriteNames;
    [SerializeField] private Image[] spriteImages;

    private void Awake()
    {
        this.GetComponent<Image>().sprite = spriteAtlas.GetSprite(spriteName);
    }

    //Create a Sprite Allocater (empty) game object to use the code below
    private void AssignSprite()
    {
        for(int i = 0; i < spriteImages.Length; i++)
        {
            spriteImages[i].sprite = spriteAtlas.GetSprite(spriteNames[i]);
        }
    }
}
