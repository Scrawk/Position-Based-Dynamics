using UnityEngine;
using System;

namespace Common.Unity.Cameras
{

    public delegate void CameraEventHandler(Camera camera);

    [RequireComponent(typeof(Camera))]
    public class RenderEvent : MonoBehaviour
    {

        public CameraEventHandler OnPostRenderEvent = delegate { };

        private Camera m_camera;

        void Start()
        {
            m_camera = GetComponent<Camera>();
        }

        void OnPostRender()
        {
            OnPostRenderEvent(m_camera);
        }

        public static void AddRenderEvent(Camera camera, CameraEventHandler onPostRenderEvent)
        {
            if (camera == null) return;

            RenderEvent renderEvent = camera.GetComponent<RenderEvent>();
            if (renderEvent != null)
                renderEvent.OnPostRenderEvent += onPostRenderEvent;
        }

        public static void RemoveRenderEvent(Camera camera, CameraEventHandler onPostRenderEvent)
        {
            if (camera == null) return;

            RenderEvent renderEvent = camera.GetComponent<RenderEvent>();
            if (renderEvent != null)
                renderEvent.OnPostRenderEvent -= onPostRenderEvent;
        }

    }

}
