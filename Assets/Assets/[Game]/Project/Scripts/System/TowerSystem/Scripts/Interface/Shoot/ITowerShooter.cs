using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ateþ etme þeklini belirleyen bir interface sýnýfý tanýmla
public interface ITowerShooter
{
    public Transform target { get; set; }
    // Interface sýnýfý içinde sadece metod imzasý olabilir
    void Shoot();
}