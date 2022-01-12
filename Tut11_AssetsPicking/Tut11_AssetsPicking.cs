﻿using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut11_AssetsPicking", Description = "Yet another FUSEE App.")]
    public class Tut11_AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;


        private Transform _mainPart;
        private Transform _frontRightTire;
        private Transform _frontLeftTire;


        // Init is called on startup. 
        public override void Init()
        {
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);
            _scene = AssetStorage.Get<SceneContainer>("rover.fus");
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);

            //apply transforms
            _mainPart = getTransform("MainPart");
            _frontRightTire = getTransform("FrontRightTire");
            _frontLeftTire = getTransform("FrontLeftTire");


        }
        //returns scene for Helper class
        public SceneContainer getScenConteiner()
        {
            return _scene;
        }


        public override async Task InitAsync()
        {
            await base.InitAsync();
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            _mainPart.Rotation = new float3(0, M.MinAngle(TimeSinceStart / 10), 0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 40) * float4x4.CreateRotationX(-(float)Math.Atan(15.0 / 40.0));

            // Render the scene on the current render context
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
        public Transform getTransform(string name)
        {
            return _scene.Children.FindNodes(node => node.Name == name)?.FirstOrDefault()?.GetTransform();
        }
        public void pickPos(float clipPos)
        {
            //_scene.Children.FindNodes(RC => node.Name == name)?.FirstOrDefault()?.GetTransform();
        }

        public void getColor(string name)
        {

        }
    }

}