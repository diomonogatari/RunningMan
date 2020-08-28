using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Linq;

public class Destroyable : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float distanceToCull = 35f;
        var cameraPosition = Camera.main.transform.position;
        var vectorToItem = (this.gameObject.transform.position - cameraPosition);
        if (Vector3.Angle(vectorToItem, Camera.main.transform.forward) > 90) //It's behind us
        {
            //Perhaps ensure it's far enough away
            if (vectorToItem.sqrMagnitude > distanceToCull * distanceToCull)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
