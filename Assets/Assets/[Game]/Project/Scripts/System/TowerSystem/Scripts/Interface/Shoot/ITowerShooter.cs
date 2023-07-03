using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ate� etme �eklini belirleyen bir interface s�n�f� tan�mla
public interface ITowerShooter
{
    public Transform target { get; set; }
    // Interface s�n�f� i�inde sadece metod imzas� olabilir
    void Shoot();
}