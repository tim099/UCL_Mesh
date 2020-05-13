using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace UCL.MeshLib {
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class UCL_MeshCreator : MonoBehaviour {
        public UnityEngine.Rendering.IndexFormat m_IndexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        public bool f_InitOnStart = true;

        protected MeshFilter m_MeshFilter;
        protected MeshRenderer m_MeshRenderer;
        protected Mesh m_Mesh;

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
            if(m_MeshFilter.mesh == null) {
                m_MeshFilter.mesh = new Mesh();
            }
            m_Mesh = m_MeshFilter.mesh;
            m_Mesh.indexFormat = m_IndexFormat;
            m_Mesh.Clear();
        }
        virtual public void GenerateMesh() {

        }


        // Update is called once per frame
        void Update() {

        }
    }
}