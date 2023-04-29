using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSaver : Saver
{
    [Serializable]
    private class SaveData
    {
        public Vector3Data position;
        public QuaternionData rotation;
        public Vector3Data velocity;
        public Vector3Data angularVelocity;
    }
    
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Rigidbody characterRigidbody;
    
    public override string RecordData(SaveSerializer serializer)
    {
        var saveData = new SaveData()
        {
            position = new Vector3Data(characterTransform.position),
            rotation = new QuaternionData(characterTransform.rotation),
            velocity = new Vector3Data(characterRigidbody.velocity),
            angularVelocity = new Vector3Data(characterRigidbody.angularVelocity)
        };

        return serializer.Serialize(saveData);
    }

    public override void ApplyData(SaveSerializer serializer, string serializedString)
    {
        var saveData = serializer.Deserialize<SaveData>(serializedString);

        if (saveData == null)
        {
            Debug.LogError($"Character Saver received null object after deserialize, base serialized string is {serializedString}", this);
            return;
        }
        
        characterTransform.SetPositionAndRotation(saveData.position.GetVector3(), saveData.rotation.GetQuaternion());
        characterRigidbody.velocity = saveData.velocity.GetVector3();
        characterRigidbody.angularVelocity = saveData.angularVelocity.GetVector3();
    }
}
