using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UCL.Core.Container;
using UnityEngine;
namespace UCL.MeshLib {
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [UCL.Core.ATTR.EnableUCLEditor]
    public class UCL_MeshCreator : MonoBehaviour {
        protected static UCL_Vector<int> m_Tri = new UCL_Vector<int>();
        protected int[] GenTriangles(int m_FaceCount) {
            lock(m_Tri) {
                int len = 6 * m_FaceCount;
                if(m_Tri.Count < len) {
                    int i = m_Tri.Count / 6;

                    m_Tri.Resize(len);
                    for(; i < m_FaceCount; i++) {
                        int p = 6 * i, q = 4 * i;
                        m_Tri[p] = q;
                        m_Tri[p + 1] = q + 3;
                        m_Tri[p + 2] = q + 1;

                        m_Tri[p + 3] = q + 3;
                        m_Tri[p + 4] = q + 2;
                        m_Tri[p + 5] = q + 1;
                    }
                }
            }
            return m_Tri.m_Arr;
        }

        public UnityEngine.Rendering.IndexFormat m_IndexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        public bool f_InitOnStart = true;

        protected MeshFilter m_MeshFilter;
        protected MeshRenderer m_MeshRenderer;
        protected Mesh m_Mesh {
            get {
                if(m_MeshFilter == null) {
                    m_MeshFilter = GetComponent<MeshFilter>();
                }
                if(m_MeshFilter.sharedMesh == null) {
                    m_MeshFilter.sharedMesh = new Mesh();
                }
                return m_MeshFilter.sharedMesh;
            }
        }

        void Start() {
            if(f_InitOnStart) Init();
        }

        virtual public void Init() {
            m_MeshRenderer = GetComponent<MeshRenderer>();
            if(m_MeshRenderer == null) {
                m_MeshRenderer = gameObject.AddComponent<MeshRenderer>();
            }

            m_MeshFilter = GetComponent<MeshFilter>();
            if(m_MeshFilter == null) {
                m_MeshFilter = gameObject.AddComponent<MeshFilter>();
            }
            if(m_MeshFilter.sharedMesh == null) {
                m_MeshFilter.sharedMesh = new Mesh();
            }
            ClearMesh();
        }
        [Core.ATTR.UCL_FunctionButton]
        virtual public void ClearMesh() {
            if(m_MeshFilter == null) Init();
            m_Mesh.indexFormat = m_IndexFormat;
            m_Mesh.Clear();
        }
        virtual public void GenerateMesh() {

        }

    }
}