using System;
using System.Collections;
using System.Collections.Generic;
using Arficord.SavingSystem.Savers;
using Arficord.SavingSystem.Serializers;
using UnityEngine;

namespace Arficord.SavingSystem.Demo.Savers
{
    public class CharacterSaver : Saver
    {
        [Serializable]
        private class SaveData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 velocity;
            public Vector3 angularVelocity;
        }

        [SerializeField] private Transform characterTransform;
        [SerializeField] private Rigidbody characterRigidbody;

        public override string RecordData(SaveSerializer serializer)
        {
            var saveData = new SaveData()
            {
                position = characterTransform.position,
                rotation = characterTransform.rotation,
                velocity = characterRigidbody.velocity,
                angularVelocity = characterRigidbody.angularVelocity,
            };

            return serializer.Serialize(saveData);
        }

        public override void ApplyData(SaveSerializer serializer, string serializedString)
        {
            if (!serializer.TryDeserialize(out SaveData saveData, serializedString))
            {
                Debug.LogError(
                    $"Character Saver received null object after deserialize, base serialized string is {serializedString}",
                    this);
                return;
            }

            characterTransform.SetPositionAndRotation(saveData.position, saveData.rotation);
            characterRigidbody.velocity = saveData.velocity;
            characterRigidbody.angularVelocity = saveData.angularVelocity;
        }
    }
}