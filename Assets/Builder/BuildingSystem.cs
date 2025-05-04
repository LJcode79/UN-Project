using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentobject;
    private Vector3 currentpos;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding = false;
    void Start()
    {
        currentobject = objects[0];
        ChangeCurrentBuilding();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(isBuilding)
        {
            startPreview();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Build();
        }

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    isBuilding = !isBuilding;
        //    if (isBuilding && currentobject != null)
        //    {
        //        ChangeCurrentBuilding(); // Instantiate preview when building starts
        //    }
        //}
    }

    public void ChangeCurrentBuilding()
    {
        GameObject curprev = Instantiate(currentobject.preview, currentpos, Quaternion.identity) as GameObject;
        currentPreview = curprev.transform;
    }

    public void startPreview()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit, 20, layer))
        {
            if (hit.transform != this.transform)
                showPreview(hit);
        }
    }

    public void showPreview(RaycastHit hit2)
    {
        currentpos = hit2.point;
        currentpos -= Vector3.one * offset;
        currentpos /= gridSize;
        currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
        currentpos *= gridSize;
        currentpos += Vector3.one * offset;
        currentPreview.position = currentpos;
    }

    public void Build()
    {
        previewObject PO = currentPreview.GetComponent<previewObject>();
        if(PO.isBuildable)
        {
            Instantiate(currentobject.prefab, currentpos, Quaternion.identity);
        }
    }
}

[System.Serializable]
public class buildObjects
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public int gold;
}


