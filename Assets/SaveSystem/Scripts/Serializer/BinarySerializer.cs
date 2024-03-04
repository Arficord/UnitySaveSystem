using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Arficord.SavingSystem.Serializers.Surrogates;
using UnityEngine;

namespace Arficord.SavingSystem.Serializers
{
    [CreateAssetMenu(fileName = "BinarySerializer", menuName = "SaveSystem/Serializers/BinarySerializer", order = 2)]
    public class BinarySerializer : SaveSerializer
    {
        public override string Serialize(object data)
        {
            //example: https://zetcode.com/csharp/base64/
            var binaryFormatter = CreateBinaryFormatter();
            using var ms = new MemoryStream();
            binaryFormatter.Serialize(ms, data);
            var biteData = ms.ToArray();
            return Convert.ToBase64String(biteData);
        }

        public override bool TryDeserialize<T>(out T result, string serializedString)
        {
            var biteData = Convert.FromBase64String(serializedString);
            var binaryFormatter = CreateBinaryFormatter();

            using var memStream = new MemoryStream();
            memStream.Write(biteData, 0, biteData.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            result = (T) binaryFormatter.Deserialize(memStream);

            return result != null;
        }

        private BinaryFormatter CreateBinaryFormatter()
        {
            var binaryFormatter = new BinaryFormatter
            {
                SurrogateSelector = CreateSurrogateSelector()
            };

            return binaryFormatter;
        }

        private SurrogateSelector CreateSurrogateSelector()
        {
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All),
                new Vector3SerializationSurrogate());
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),
                new QuaternionSerializationSurrogate());
            return surrogateSelector;
        }
    }
}