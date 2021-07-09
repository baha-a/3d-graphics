using System;
using System.Collections.Generic;
using System.Text;

namespace Loader
{
    public struct Vertex
    {
        public float x, y, z;  //point coordenates
        public float u, v;     //texture coordinates
    }

    public struct Material
    {
        public string name;
        public byte[] ambient;
        public float[] ambientf;

        public byte[] diffuse;
        public float[] diffusef;

        public byte[] specular;
        public float[] specularf;

        public Material(string name)
        {
            this.name = name;
            ambient = new byte[3];
            ambientf = new float[4]; 
            diffuse = new byte[3];
            diffusef = new float[4]; 
            specular = new byte[3];
            specularf = new float[4]; 
        }
    }

    public struct Face
    {
        public Vertex[] vertex;
        public Vector3[] normal;
    }

    public struct MeshIndexed
    {
        public FaceIndexed[] faceList;
        public Vertex[] vertexList;
        public TextureCoord[] textures;
        public Vector3[] normals;
        public string meshName;
        public string textureName;
        public Material material;
        public Matrix matrix;

        public void Allocate(uint nVerts, uint nFaces)
        {
            vertexList = new Vertex[nVerts];
            faceList = new FaceIndexed[nFaces];
            normals = new Vector3[nFaces * 3];
            textures = new TextureCoord[nVerts * 2];
            material = new Material("null");
        }

        public void AddMaterialAmbient(byte ra, byte ga, byte ba)
        {
            material.ambient[0] = ra;
            material.ambient[1] = ga;
            material.ambient[2] = ba;
            material.ambientf[0] = (float)ra / 255f;
            material.ambientf[1] = (float)ga / 255f;
            material.ambientf[2] = (float)ba / 255f;
            material.ambientf[3] = 1;
            material.name = "notnull"; 
        }

        public void AddMaterialDifusse(byte ra, byte ga, byte ba)
        {
            material.diffuse[0] = ra;
            material.diffuse[1] = ga;
            material.diffuse[2] = ba;
            material.diffusef[0] = (float)ra / 255f;
            material.diffusef[1] = (float)ga / 255f;
            material.diffusef[2] = (float)ba / 255f;
            material.diffusef[3] = 1;
            material.name = "notnull";
        }

        public void AddMaterialSpecular(byte ra, byte ga, byte ba)
        {
            material.specular[0] = ra;
            material.specular[1] = ga;
            material.specular[2] = ba;
            material.specularf[0] = (float)ra / 255f;
            material.specularf[1] = (float)ga / 255f;
            material.specularf[2] = (float)ba / 255f;
            material.specularf[3] = 1;
            material.name = "notnull";
        }

        public void SetVertexPosition(uint i, Vector3 v)
        {
            Vertex vertex = new Vertex();
            vertex.x = v.X;
            vertex.y = v.Y;
            vertex.z = v.Z;
            vertexList[i] = vertex;
        }

        public void SetFaceIndicies(uint i, int a, int b, int c)
        {
            int[] indicies = { a, b, c };
            faceList[i].vertexIndexes = indicies;
        }

        public void SetVertexNormal(uint i, Vector3 v)
        {
            normals[i] = v;
        }
    }

    public struct FaceIndexed
    {
        public int[] vertexIndexes;
        public int[] textureIndexes;
        public Vector3[] normal;
    }

    public struct TextureCoord
    {
        public float x;
        public float y;
    };


    public struct Matrix
    {
        float[,] values;

        public float[,] Values
        {
            get { return values; }
            set { values = value; }
        }

        public Matrix(float[,] values)
        {
            this.values = values;
        }

        public Matrix(float m11, float m12, float m13, float m14, float m21,
           float m22, float m23, float m24, float m31, float m32, float m33,
           float m34, float m41, float m42, float m43, float m44)
        {
            values = new float[4, 4];
            values[0, 0] = m11;
            values[0, 1] = m12;
            values[0, 2] = m13;
            values[0, 3] = m14;
            values[1, 0] = m21;
            values[1, 1] = m22;
            values[1, 2] = m23;
            values[1, 3] = m24;
            values[2, 0] = m31;
            values[2, 1] = m32;
            values[2, 2] = m33;
            values[2, 3] = m34;
            values[3, 0] = m41;
            values[3, 1] = m42;
            values[3, 2] = m43;
            values[3, 3] = m44;
        }

        public static Matrix Identity()
        {
            return new Matrix(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        }

        public static float[] IdentityF()
        {
            float[] identity = { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            return identity;
        }

        public static Matrix CreateTranslation(float x, float y, float z)
        {
            return new Matrix(new float[4, 4]);
        }

        public static Matrix CreateScale(float x, float y, float z)
        {
            return new Matrix(new float[4, 4]);
        }

        public static Matrix CreateFromAxisAngle(Vector3 v, float r)
        {
            return new Matrix(new float[4, 4]);
        }

        public static Matrix operator *(Matrix first, Matrix other)
        {
            float[,] values = new float[4, 4];
            values[0, 0] = first.values[0, 0] * other.values[0, 0];
            values[0, 1] = first.values[1, 0] * other.values[0, 1];
            values[0, 2] = first.values[2, 0] * other.values[0, 2];
            values[0, 3] = first.values[3, 0] * other.values[0, 3];

            values[1, 0] = first.values[0, 1] * other.values[1, 0];
            values[1, 1] = first.values[1, 1] * other.values[1, 1];
            values[1, 2] = first.values[2, 1] * other.values[1, 2];
            values[1, 3] = first.values[3, 1] * other.values[1, 3];

            values[2, 0] = first.values[0, 2] * other.values[2, 0];
            values[2, 1] = first.values[1, 2] * other.values[2, 1];
            values[2, 2] = first.values[2, 2] * other.values[2, 2];
            values[2, 3] = first.values[3, 2] * other.values[2, 3];

            values[3, 0] = first.values[0, 3] * other.values[3, 0];
            values[3, 1] = first.values[1, 3] * other.values[3, 1];
            values[3, 2] = first.values[2, 3] * other.values[3, 2];
            values[3, 3] = first.values[3, 3] * other.values[3, 3];
            return new Matrix(values);
        }
    }

    public struct Vector2
    {
        int x, y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Vector2F
    {
        float x, y;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector2F(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Vector3
    {
        float x, y, z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public float sqrMagnitude()
        {
            return (float)(x * x + y * y + z * z);
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 Normalize()
        {
            float len = (float)Math.Sqrt(x * x + y * y + z * z);
            len = 1.0f / len;
            this.x *= len;
            this.y *= len;
            this.z *= len;
            return this;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            float x = ((v1.Y * v2.Z) - (v1.Z * v2.Y));
            float y = ((v1.Z * v2.X) - (v1.X * v2.Z));
            float z = ((v1.X * v2.Y) - (v1.Y * v2.X));
            return new Vector3(x, y, z);
        }

        public static Vector3 operator *(Vector3 first, float other)
        {
            return new Vector3(first.X * other, first.Y * other, first.Z * other);
        }

        public static Vector3 operator *(float other, Vector3 first)
        {
            return new Vector3(first.X * other, first.Y * other, first.Z * other);
        }

        public static Vector3 operator /(Vector3 first, ushort other)
        {
            return new Vector3(first.X / other, first.Y / other, first.Z / other);
        }

        public static Vector3 operator +(Vector3 first, Vector3 other)
        {
            return new Vector3(first.X + other.X, first.Y + other.Y, first.Z + other.Z);
        }

        public static Vector3 operator -(Vector3 first, Vector3 other)
        {
            return new Vector3(first.X - other.X, first.Y - other.Y, first.Z - other.Z);
        }

        public float DistanceTo(Vector3 other)
        {
            return (float)Math.Sqrt((x - other.x) * (x - other.x) + (y - other.y) * (y - other.y));
        }

        public static float Dot(Vector3 v, Vector3 u)
        {
            return v.x * u.x + v.y * u.y + v.z * u.z;
        }

        /// This function  calculates 2D distances between two points
        public static float DistPointToPoint(Vector3 first, Vector3 second)
        {
            return (float)Math.Sqrt((first.x - second.x) * (first.x - second.x) + (first.y - second.y) * (first.y - second.y));
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }
    }
}
