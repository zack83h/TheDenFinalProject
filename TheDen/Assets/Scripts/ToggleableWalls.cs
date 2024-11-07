using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleableWalls : MonoBehaviour
{
    public static ToggleableWalls Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance.GetComponent<TilemapRenderer>().enabled = false;
        Instance.GetComponent<TilemapCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
