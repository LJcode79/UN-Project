using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewObject : MonoBehaviour
{
    public bool foundation;
    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool isBuildable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && foundation)
            col.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && foundation)
            col.Remove(other);
    }

    public void changecolor()
    {
        if (col.Count == 0)
            isBuildable = true;
        else
            isBuildable = false;

        if (isBuildable)
            foreach(Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = green;
            }
        else
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changecolor();
    }
}
