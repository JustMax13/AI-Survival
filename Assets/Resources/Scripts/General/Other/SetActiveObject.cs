using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour
{
    public void ObjectON(GameObject obj) => obj.SetActive(true);
    public void ObjectOFF(GameObject obj) => obj.SetActive(false);
}
