using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public Transform prefabItemWorld;

    public Sprite sprite1, sprite2, sprite3, sprite4, sprite5, sprite6; //TODO change names to match item names


}
