using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerHealth : MonoBehaviour
{
    public static UIManagerHealth Instance;
    [SerializeField] Image healthBar;
    float currentHp;
    float maxHp;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        //take the current and max hp
        //and set the healthbar to that %
        var hp = currentHp / maxHp;
        healthBar.fillAmount = hp;
    }
}
