using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ProtoBuf;

namespace ClientNamespace
{
    public class ProtoSerialize
    {
        public static byte[] Serialize<T>(T obj) where T : class
        {
            if (null == obj) return null;
            try
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    ProtoBuf.Serializer.SerializeWithLengthPrefix<T>(stream, obj, ProtoBuf.PrefixStyle.Base128);
                    return stream.ToArray();
                }
            }
            catch
            {
                throw;
            }
        }


        public static T Deserialize<T>(byte[] bytes) where T : class
        {
            if (null == bytes) return null;
            try
            {
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    return ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(stream, ProtoBuf.PrefixStyle.Base128);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
