using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{

    [Header(header: "Time out:*")] public float tiempo;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(textKiller());
    }

    private IEnumerator textKiller()
    {
        yield return  new  WaitForSeconds(tiempo);
        gameObject.SetActive(false);
       // Destroy(gameObject);
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
