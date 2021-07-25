using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiBounce : MonoBehaviour
{

    void Start()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.45f).SetLoops(-1, LoopType.Yoyo);
    }


}
