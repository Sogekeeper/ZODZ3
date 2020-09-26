using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointer : MonoBehaviour
{
    //pontos vao dizer onde e que tipo de coisa pode ser inserida na sala
    public enum PointSize {small = 1, medium = 2, large = 3}; 
    public PointSize pointSize;
    public bool used = false;
}
