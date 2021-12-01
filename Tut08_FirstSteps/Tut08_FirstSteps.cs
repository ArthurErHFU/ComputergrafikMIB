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
        private Transform[] _cubeTransform = new Transform[3];
        private float _camAngle;

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to "greenery"
            RC.ClearColor = new float4(1f, 1f, 1f, 1f);
            /*
            // Create a scene with a cube
            // The three components: one Transform, one ShaderEffect (blue material) and the Mesh
            _cubeTransform = new Transform { Scale = new float3(1, 1, 1), Translation = new float3(0f, 0f, 0f) };
            _cubeTransform.Rotation = new float3(20, 0, 0);

            var cubeShader = MakeEffect.FromDiffuseSpecular((float4)ColorUint.YellowGreen);
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10f, 10f, 10f));

            // Assemble the cube node containing the three components
            var cubeNode = new SceneNode();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);
            */


            // Create the scene containing the cube as the only object -->brute force
            _scene = new SceneContainer();
            _scene.Children.Add(cubes(((float4)ColorUint.YellowGreen), (new float3(1f, 1f, 1f)), (new float3(0f, 0f, 0f)), 0));
            _scene.Children.Add(cubes(((float4)ColorUint.Greenery), (new float3(1f, 0.5f, 1f)), (new float3(0f, 0f, 50f)), 1));
            _scene.Children.Add(cubes(((float4)ColorUint.BlanchedAlmond), (new float3(0.5f, 0.5f, 0.5f)), (new float3(50f, 0f, 0f)), 2));
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();


            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            //<---ANIMATION---->
            //Animate CamRotation
            _camAngle += 90f * M.Pi / 180.0f * DeltaTime;

            _cubeTransform[0].Translation = new float3(0, 5 * M.Sin(3 * TimeSinceStart - 120), 30);
            _cubeTransform[1].Translation = new float3(0, 3 * M.Sin(4 * TimeSinceStart + 50), 0);
            _cubeTransform[2].Translation = new float3(10, 1 * M.Sin(2 * TimeSinceStart), 10);




            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);
            // Create the scene containing the cube as the only object
            _sceneRenderer.Render(RC);

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


        //myPlayground --> create won Objekt?
        public SceneNode cubes(float4 color, float3 scale, float3 trans, int index)
        {
            // Create a scene with a cube
            // The three components: one Transform, one ShaderEffect (blue material) and the Mesh
            _cubeTransform[index] = new Transform { Scale = scale, Translation = trans };
            _cubeTransform[index].Rotation = new float3(20 * index, 0, 0);

            var cubeShader = MakeEffect.FromDiffuseSpecular(color);
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10f, 10f, 10f));

            // Assemble the cube node containing the three components
            var cubeNode = new SceneNode();
            cubeNode.Components.Add(_cubeTransform[index]);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);

            return cubeNode;
        }
    }
}