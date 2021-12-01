using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {

        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform _cubeTransform;
        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to "greenery"
            RC.ClearColor = new float4(1f, 1f, 1f, 1f);

            // Create a scene with a cube
            // The three components: one Transform, one ShaderEffect (blue material) and the Mesh
            _cubeTransform = new Transform { Translation = new float3(0f, 0f, 50f) };

            var cubeShader = MakeEffect.FromDiffuseSpecular((float4)ColorUint.YellowGreen);
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10f, 10f, 10f));

        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();


            // Clear the backbuffer

            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
            // Set the rendering area to the entire window size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }

    }
}