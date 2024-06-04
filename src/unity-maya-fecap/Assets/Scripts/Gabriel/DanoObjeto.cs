using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoObjeto : MonoBehaviour
{
    public int damageDealt;

    private void OnTriggerStay(Collider other){
        other.GetComponent<MayaVida>().TakeDamage(damageDealt);
    }

}
