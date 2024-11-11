using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public GameObject defaultMouth;
    public GameObject laughedMouth;
    // Start is called before the first frame update
    void Start()
    {
        defaultMouth.SetActive(true);
        laughedMouth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ("Failed" != AngleController.instance.WhichCompletionStatus())
        {
            defaultMouth.SetActive(false);
            laughedMouth.SetActive(true);
        }
        else
        {
            defaultMouth.SetActive(true);
            laughedMouth.SetActive(false);
        }
    }
}
