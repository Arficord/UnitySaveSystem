using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arficord.SavingSystem.Demo.Controllers
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] [Range(1, 20)] private float speed;

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            x *= speed;
            y *= speed;

            rigidbody.velocity = new Vector3(x, y, 0);
        }
    }
}