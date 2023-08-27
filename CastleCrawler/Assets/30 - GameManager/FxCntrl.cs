using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxCntrl : MonoBehaviour
{
    [SerializeField] private GameObject bombFx;

    public void Bomb(Vector3 position)
    {
        Instantiate(bombFx, position, Quaternion.identity);
    }
}
