using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Fox myFox;

    private Transform mytransform;
    // Start is called before the first frame update
    void Start()
    {
        mytransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        mytransform.position = new Vector3(myFox.transform.position.x, mytransform.position.y, mytransform.position.z);
    }
}
