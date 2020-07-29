using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UCL.MeshLib {
    [UCL.Core.ATTR.EnableUCLEditor]
    public class UCL_MeshCreatorBasic : UCL_MeshCreator {
        public List<Vector3> m_Vertices = new List<Vector3>();
        public List<int> m_Triangles = new List<int>();
        public List<Vector2> m_UV = new List<Vector2>();
        public bool m_ShowInEditMode = true;

        [UCL.Core.ATTR.UCL_FunctionButton]
        public override void Init() {
            base.Init();
            GenerateMesh();
        }
#if UNITY_EDITOR
        private void OnValidate() {
            if(m_ShowInEditMode) Init();
        }
#endif
        override public void GenerateMesh() {
            //m_Mesh.subMeshCount = 2;
            //Debug.LogWarning("arr2");
            m_Mesh.vertices = m_Vertices.ToArray();
            //m_Mesh.SetTriangles(m_Triangles.ToArray(), 0,true, 0); //SetTriangles(int[] triangles, int trianglesStart
            //var tri_arr = m_Triangles.ToArray();
            //m_Mesh.SetTriangles(tri_arr, 0, 3, 0);
            //m_Mesh.SetTriangles(tri_arr, 3, 3, 1);
            m_Mesh.triangles = m_Triangles.ToArray();
            //SetTriangles(m_Triangles.ToArray());
            m_Mesh.uv = m_UV.ToArray(); // add this line to the code here
            m_Mesh.Optimize();
            m_Mesh.RecalculateNormals();
        }
    }
}