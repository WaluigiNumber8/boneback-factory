using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of Matrix4x4
    /// </summary>
    [System.Serializable]
    public class SerializedMatrix4x4
    {
        public readonly float m00;
        public readonly float m01;
        public readonly float m02;
        public readonly float m03;
        public readonly float m10;
        public readonly float m11;
        public readonly float m12;
        public readonly float m13;
        public readonly float m20;
        public readonly float m21;
        public readonly float m22;
        public readonly float m23;
        public readonly float m30;
        public readonly float m31;
        public readonly float m32;
        public readonly float m33;

        public SerializedMatrix4x4(Matrix4x4 matrix) : this(matrix.m00, matrix.m01, matrix.m02, matrix.m03,
                                                            matrix.m10, matrix.m11, matrix.m12, matrix.m13,
                                                            matrix.m20, matrix.m21, matrix.m22, matrix.m23,
                                                            matrix.m30, matrix.m31, matrix.m32, matrix.m33) { }

        public SerializedMatrix4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m03 = m03;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m30 = m30;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }
    }
}