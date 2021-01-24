using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EETAC : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator ShowArea(string name)
    {
        anim.Play("EETACShow");
        transform.GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(1).GetComponent<Text>().text = name;
        yield return new WaitForSeconds(1f);
        anim.Play("EETACFadeOut");
    }
}
