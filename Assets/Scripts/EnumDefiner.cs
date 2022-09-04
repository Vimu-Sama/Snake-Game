using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefiner : MonoBehaviour
{
    [SerializeField] tagType typeofTag;

    public tagType GetTagType()
    {
        return typeofTag;
    }

}
