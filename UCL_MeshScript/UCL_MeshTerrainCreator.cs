using UnityEngine;

namespace UCL.MeshLib {
    [UCL.Core.ATTR.EnableUCLEditor]
    public class UCL_MeshTerrainCreator : UCL_MeshCreatorBasic {
        public float m_BlockSize = 1f;
        public float m_HeightMult = 0.1f;
        public int m_Width;
        public int m_Height;
        float[,] m_Terrain;


        [UCL.Core.ATTR.UCL_FunctionButton]
        public override void Init() {
            base.Init();
            GenerateMesh();
        }
        public void SetTerrain(float[,] _Terrain) {
            ClearMesh();
            m_Vertices.Clear();
            m_Triangles.Clear();
            m_UV.Clear();

            m_Terrain = _Terrain;
            
            m_Width = m_Terrain.GetLength(0);
            m_Height = m_Terrain.GetLength(1);
            float[,] vert = new float[m_Width+1, m_Height+1];

            //int count = 0;
            for(int y = 0; y <= m_Height; y++) {
                for(int x = 0; x <= m_Width; x++) {
                    int h_c = 0;
                    float h = 0;

                    if(x < m_Width && y < m_Height) {
                        h += m_Terrain[x, y];
                        h_c++;
                    }

                    if(x > 1 && y < m_Height) {
                        h += m_Terrain[x - 1, y];
                        h_c++;
                    }
                    if(x > 1 && y > 1) {
                        h += m_Terrain[x - 1, y - 1];
                        h_c++;
                    }
                    if(y > 1 && x < m_Width) {
                        h += m_Terrain[x, y - 1];
                        h_c++;
                    }
                    if(h_c > 0) {
                        vert[x, y] = m_HeightMult * m_Width * (h / h_c);
                    } else {
                        vert[x, y] = 0;
                    }
                    
                    //m_Vertices.Add(new Vector3(m_BlockSize * x, h / h_c, m_BlockSize * y));
                    //m_UV.Add(Vector2.one);
                }
            }
            //int at = 0;
            for(int y = 0; y < m_Height; y++) {
                for(int x = 0; x < m_Width; x++) {
                    m_Vertices.Add(new Vector3(m_BlockSize * x, vert[x, y], m_BlockSize * y));
                    m_Vertices.Add(new Vector3(m_BlockSize * (x + 1), vert[x + 1, y], m_BlockSize * y));
                    m_Vertices.Add(new Vector3(m_BlockSize * (x + 1), vert[x + 1, y + 1], m_BlockSize * (y + 1)));
                    m_Vertices.Add(new Vector3(m_BlockSize * x, vert[x, y + 1], m_BlockSize * (y + 1)));

                    m_UV.Add(Vector2.zero);
                    m_UV.Add(Vector2.right);
                    m_UV.Add(Vector2.one);
                    m_UV.Add(Vector2.up);

                    //int q = 4 * at;
                    //m_Triangles.Add(q);
                    //m_Triangles.Add(q + 1); 
                    //m_Triangles.Add(q + 2);

                    //m_Triangles.Add(q);
                    //m_Triangles.Add(q + 2);
                    //m_Triangles.Add(q + 3);

                    //at++;
                }
            }
            GenerateMesh();
        }
        override public void GenerateMesh() {
            

            m_Mesh.vertices = m_Vertices.ToArray();
            m_Mesh.uv = m_UV.ToArray(); // add this line to the code here
            //m_Mesh.SetTriangles(m_Triangles.ToArray(), 0);
            int m_FaceCount = (m_Width) * (m_Height);
            m_Mesh.SetTriangles(GenTriangles(m_FaceCount+1), 0, 6 * m_FaceCount, 0);//m_Triangles.ToArray();

            m_Mesh.Optimize();
            m_Mesh.RecalculateNormals();
        }
    }
}