using System.Collections;
using UnityEngine;

public class CutScene : MonoBehaviour
{

    public GameObject Camera1;
    public GameObject Camera2;

    void Start()
    {
        StartCoroutine(theCutScene());
    }      

    IEnumerator theCutScene ()
    {
        yield return new WaitForSeconds(7);
        Camera2.SetActive(true);
        Camera1.SetActive(false);
        yield return new WaitForSeconds(20);
    }
}