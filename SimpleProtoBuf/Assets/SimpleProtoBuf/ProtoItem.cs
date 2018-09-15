using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Utils;

namespace SimpleProtoBuf
{

    public enum ProtoItemType
    {
        none = 0,
        int8,
        uint8,
        int16,
        uint16,
        int32,
        uint32,
        int64,
        uint64,
        float32,
        float64,
        str,
        array
    }

    public abstract class ProtoItem
    {

        // 类型
        public abstract ProtoItemType _GetItemType();

        private bool CheckType(System.Type type)
        {
            var thisType = _GetItemType();
            if (thisType == ProtoItemType.none)
                return false;
            var protoType = GetProtoType(type);
            if (protoType == ProtoItemType.none)
                return false;
            return thisType == protoType;
        }

        public static ProtoItemType GetProtoType(System.Type type)
        {
            if (type == typeof(int))
                return ProtoItemType.int32;
            if (type == typeof(uint))
                return ProtoItemType.uint32;
            if (type == typeof(sbyte))
                return ProtoItemType.int8;
            if (type == typeof(byte))
                return ProtoItemType.uint8;
            if (type == typeof(short))
                return ProtoItemType.int16;
            if (type == typeof(ushort))
                return ProtoItemType.uint16;
            if (type == typeof(float))
                return ProtoItemType.float32;
            if (type == typeof(double))
                return ProtoItemType.float64;
            if (type == typeof(long))
                return ProtoItemType.int64;
            if (type == typeof(ulong))
                return ProtoItemType.uint64;
            if (type == typeof(string))
                return ProtoItemType.str;
            if (type == typeof(Array))
                return ProtoItemType.array;
            return ProtoItemType.none;
        }

        public virtual int _GetSize()
        {
            ProtoItemType t = _GetItemType();
            switch (t)
            {
                case ProtoItemType.int32:
                case ProtoItemType.uint32:
                case ProtoItemType.float32:
                    return 4;
                case ProtoItemType.int8:
                case ProtoItemType.uint8:
                    return 1;
                case ProtoItemType.uint16:
                case ProtoItemType.int16:
                    return 2;
                case ProtoItemType.int64:
                case ProtoItemType.uint64:
                case ProtoItemType.float64:
                    return 8;
                default:
                    return -1;
            }
        }
    }

    public class ProtoSimpleItem: ProtoItem
    {
        public ProtoSimpleItem(ProtoItemType _type)
        {
            m_Type = _type;
        }

        public override ProtoItemType _GetItemType()
        {
            return m_Type;
        }

        private ProtoItemType m_Type = ProtoItemType.none;
    }

    public class ProtoStringItem: ProtoItem
    {
        public override ProtoItemType _GetItemType()
        {
            return ProtoItemType.str;
        }
    }

}