using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;


namespace FuseeApp
{
    public static class SimpleMeshes
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z}
                },

                Triangles = new ushort[]
                {
                    // front face
                    0, 2, 1, 0, 3, 2,

                    // right face
                    4, 6, 5, 4, 7, 6,

                    // back face
                    8, 10, 9, 8, 11, 10,

                    // left face
                    12, 14, 13, 12, 15, 14,

                    // top face
                    16, 18, 17, 16, 19, 18,

                    // bottom face
                    20, 22, 21, 20, 23, 22

                },

                Normals = new[]
                {
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0)
                },

                UVs = new[]
                {
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0)
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f * size)
            };
        }

        public static SurfaceEffect MakeMaterial(float4 color)
        {
            return MakeEffect.FromDiffuseSpecular(
                albedoColor: color,
                emissionColor: float3.Zero,
                shininess: 25.0f,
                specularStrength: 1f);
        }

        public static Mesh CreateCylinder(float radius, float height, int segments)
        {
            float uHight = height / 2;
            float bHight = -height / 2;
            // conter clock wise segments rotation
            float alpha = 2 * M.Pi / segments;


            //Double Array Top and Bootom  2Flächen + Mantel
            float3[] verts = new float3[((segments + 1) * 2) + ((segments) * 2)];
            float3[] norms = new float3[((segments + 1) * 2) + ((segments) * 2)];
            ushort[] tris = new ushort[((segments * 3) * 2) + ((segments * 3) * 2)];


            //zwischenwert
            int v = 0; //<-- index wert für die norm>s und die verts
            int t = 0;// 

            //bottompart
            for (int i = 0; i < (segments); i++)
            {
                verts[v] = new float3(radius * M.Cos(i * alpha), uHight, radius * M.Sin(i * alpha));
                norms[v] = new float3(0, 1, 0);

                tris[(t) * 3 + 0] = (ushort)(t);
                if (i < segments - 1)
                {
                    tris[(t) * 3 + 2] = (ushort)segments;
                    tris[(t) * 3 + 1] = (ushort)(t + 1);
                }
                else
                {
                    //flips last triangle --> let it bee visislbe
                    tris[(t) * 3 + 1] = (ushort)(0);
                    tris[(t) * 3 + 2] = (ushort)(t + 1);
                }
                t++;
                v++;
            }
            verts[v] = new float3(0, uHight, 0);
            norms[v] = new float3(0, 1, 0);
            v++;


            //bottompart
            for (int i = 0; i < (segments); i++)
            {
                verts[v] = new float3(radius * M.Cos(i * alpha), bHight, radius * M.Sin(i * alpha));
                norms[v] = new float3(0, 1, 0);


                tris[(t) * 3 + 0] = (ushort)(v);


                if (i < segments - 1)
                {
                    tris[(t) * 3 + 1] = (ushort)(segments * 2 + 1);
                    tris[(t) * 3 + 2] = (ushort)(v + 1);
                }
                else
                {
                    //flips last triangle --> let it bee visislbe
                    tris[(t) * 3 + 2] = (ushort)(segments + 1);
                    tris[(t) * 3 + 1] = (ushort)(v + 1);
                }
                t++;
                v++;
            }
            verts[v] = new float3(0, bHight, 0);
            norms[v] = new float3(0, -1, 0);
            v++;

            for (int i = 0; i < (segments); i++)
            {

                verts[v] = new float3(radius * M.Cos(i * alpha), uHight, radius * M.Sin(i * alpha));
                norms[v] = new float3(M.Cos(i * alpha), 0, M.Sin(i * alpha));

                verts[v + segments] = new float3(radius * M.Cos(i * alpha), bHight, radius * M.Sin(i * alpha));
                norms[v + segments] = new float3(M.Cos(i * alpha), 0, M.Sin(i * alpha));



                if (i < (segments - 1))
                {

                    //halfFace
                    tris[t * 3 + 0] = (ushort)(v);
                    tris[t * 3 + 2] = (ushort)(v + 1);
                    tris[t * 3 + 1] = (ushort)(v + segments);
                    //
                    tris[(t + segments) * 3 + 0] = (ushort)(v + 1);
                    tris[(t + segments) * 3 + 1] = (ushort)(v + segments);
                    tris[(t + segments) * 3 + 2] = (ushort)(v + segments + 1);

                }
                else
                {

                    //last twoe pices!
                    tris[t * 3 + 0] = (ushort)(v);
                    tris[t * 3 + 2] = (ushort)(v + 1);
                    tris[t * 3 + 1] = (ushort)(v + segments);

                    //
                    //last face not right
                    tris[(t + segments) * 3 + 0] = (ushort)(v + 1);
                    tris[(t + segments) * 3 + 1] = (ushort)(v + segments);
                    tris[(t + segments) * 3 + 2] = (ushort)(v + segments + 1); //--> one to much

                }
                v++;
                t++;
            }


            return new Mesh

            {
                Vertices = verts,
                Normals = norms,
                Triangles = tris,
            };
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }


    /*
    public static Mesh CreateCylinder(float radius, float height, int segments)
            {
                float uHight = height / 2;
                float bHight = -height / 2;
                // conter clock wise segments rotation
                float alpha = 2 * M.Pi / segments;


                //Double Array Top and Bootom
                float3[][] verts = new float3[2][];
                float3[][] norms = new float3[2][];
                ushort[][] tris = new ushort[2][];

                //Filling the double array 
                verts[0] = new float3[segments + 1];
                verts[1] = new float3[segments + 1];

                norms[0] = new float3[segments + 1];
                norms[1] = new float3[segments + 1];

                tris[0] = new ushort[segments * 3 + 3];
                tris[1] = new ushort[segments * 3 + 3];




                //Bottom faces!
                verts[0][segments] = new float3(0, uHight, 0);
                for (int i = 0; i < segments; i++)
                {
                    verts[0,i] = new float3(radius * M.Cos(i * alpha), uHight, radius * M.Sin(i * alpha));
                    norms[0,i] = new float3(0, 1, 0);

                    tris[0,i * 3 + 0] = (ushort)(i - 1);
                    tris[0,i * 3 + 1] = (ushort)i;
                    tris[0,i * 3 + 2] = (ushort)segments;
                }


                tris[0][segments * 3 + 0] = (ushort)(segments - 1);
                tris[0][segments * 3 + 1] = (ushort)0;
                tris[0][segments * 3 + 2] = (ushort)(segments);

                //Upper faces!
                verts[1][segments] = new float3(0, bHight, 0);
                for (int i = 0; i < segments; i++)
                {
                    verts[1,i] = new float3(radius * M.Cos(i * alpha), bHight, radius * M.Sin(i * alpha));
                    norms[1,i] = new float3(0, -1, 0);

                    tris[1,i * 3 + 0] = (ushort)(i - 1);
                    tris[1,i * 3 + 1] = (ushort)i;
                    tris[1,i * 3 + 2] = (ushort)segments;
                }

                tris[1][segments * 3 + 0] = (ushort)(segments - 1);
                tris[1][segments * 3 + 1] = (ushort)0;
                tris[1][segments * 3 + 2] = (ushort)(segments);

                return new Mesh
                {

                    Vertices = vertsOut,
                    Normals = norms[1],
                    Triangles = tris[1],
                };
            }

------
            verts[segments] = new float3(0, bHight, 0);
            for (int i = 0; i < segments; i++)
            {
                verts[i] = new float3(radius * M.Cos(i * alpha), bHight, radius * M.Sin(i * alpha));
                norms[i] = new float3(0, -1, 0);

                tris[1 i * 3 + 0] = (ushort)(i); // auf -1 achten!
                tris[i * 3 + 1] = (ushort)(i + 1);
                if (i == segments)
                {
                    tris[i * 3 + 2] = (ushort)0;
                }

            }


-----

 for (int i = 0; i < (segments); i++)
            {

                if (i <= segments / 2)
                {
                    if (i == 0)
                    {
                        verts[segments] = new float3(0, uHight, 0);
                    }
                    verts[i] = new float3(radius * M.Cos(i * alpha), uHight, radius * M.Sin(i * alpha));
                    norms[i] = new float3(0, 1, 0);

                }
                else
                {
                    if (i == (segments / 2) + 1)
                    {
                        verts[segments] = new float3(0, bHight, 0);
                    }
                    verts[i] = new float3(radius * M.Cos(i * alpha), bHight, radius * M.Sin(i * alpha));
                    norms[i] = new float3(0, -1, 0);
                }


                tris[i * 3 + 0] = (ushort)(i);
                tris[i * 3 + 1] = (ushort)(i + 1);

                if (i == segments / 2)
                {
                    tris[i * 3 + 2] = (ushort)0;
                }
                else
                {
                    tris[i * 3 + 2] = (ushort)(i + 2);
                }

            }
    */


}