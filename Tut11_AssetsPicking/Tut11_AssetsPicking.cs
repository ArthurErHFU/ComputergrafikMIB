using Fusee.Base.Common;
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
        private ScenePicker _scenePicker;

        //Tires
        private Transform _mainPart;
        private Transform _frontRightTire;
        private Transform _frontLeftTire;
        private Transform _midTireRight;
        private Transform _midTireLeft;
        private Transform _backRightTire;
        private Transform _backLeftTire;

        //RotateAxel
        private Transform _frontRightWing;
        private Transform _frontLeftWing;
        private Transform _backRightWing;
        private Transform _backLeftWing;

        //Weapon
        private Transform _cannonBase;
        private Transform _cannon;

        //Picking
        private PickResult _currentPick;
        private float4 _oldColor;

        // Init is called on startup. 
        public override void Init()
        {
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);
            _scene = AssetStorage.Get<SceneContainer>("rover2.fus");
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);

            //apply transforms
            //Tires
            _mainPart = getTransform("MainPart");
            _frontRightTire = getTransform("FrontRightTire");
            _frontLeftTire = getTransform("FrontLeftTire");
            _midTireRight = getTransform("MidTireRight");
            _midTireLeft = getTransform("MidTireLeft");
            _backRightTire = getTransform("BackRightTire");
            _backLeftTire = getTransform("BackLeftTire");

            //RotationPlatform
            _frontRightWing = getTransform("FrontRightWing");
            _frontLeftWing = getTransform("FrontLeftWing");
            _backRightWing = getTransform("BackRightWing");
            _backLeftWing = getTransform("BackLeftWing");

            //Weapon

            _cannonBase = getTransform("CannonBase");
            _cannon = getTransform("Cannon");
            //scenepicker
            _scenePicker = new ScenePicker(_scene);

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

            //_mainPart.Rotation = new float3(0, M.MinAngle(TimeSinceStart / 10), 0);


            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 20) * float4x4.CreateRotationX(-(float)Math.Atan(15.0 / 40.0));

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.

            // PlyeerControll
            driveForword(3);
            turnLeftRight(1);
            pickNode();
            picAnimation(_currentPick);
            weaponTrun(1.5f);
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
        private Transform getTransform(string name)
        {
            return _scene.Children.FindNodes(node => node.Name == name)?.FirstOrDefault()?.GetTransform();
        }

        private void pickNode()
        {
            if (Mouse.LeftButton)
            {
                float2 clipPos = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1.0f, 1.0f);
                //works not well wiht my rover
                PickResult newPick = _scenePicker.Pick(RC, clipPos).OrderBy(pr => pr.ClipPos.z).FirstOrDefault();
                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        //var ef = _currentPick.Node.GetComponent<DefaultSurfaceEffect>();
                        //ef.SurfaceEffect.Albedo = _oldColor;
                    }

                    if (newPick != null)
                    {
                        var ef = newPick.Node.GetComponent<SurfaceEffect>();
                        _oldColor = ef.SurfaceInput.Albedo;
                        ef.SurfaceInput.Albedo = (float4)ColorUint.Greenery;
                        _currentPick = newPick;
                    }
                }

            }

        }

        private void picAnimation(PickResult pick)
        {
            if (pick != null)
            {
                var trans = pick.Node.Parent.GetComponent<Transform>();
                float time = M.MinAngle(TimeSinceStart);
                float speed = 3f;

                if (trans != null)
                {
                    if (Keyboard.GetKey(KeyCodes.Up))
                    {
                        trans.Rotation = new float3((speed * 10 + trans.Rotation.x), trans.Rotation.y, 0);
                    }
                    else if (Keyboard.GetKey(KeyCodes.Down))
                    {
                        trans.Rotation = new float3((-speed * 10 + trans.Rotation.x), trans.Rotation.y, 0);
                    }
                    else if (Keyboard.GetKey(KeyCodes.Left))
                    {
                        trans.Rotation = new float3(trans.Rotation.x, (speed + trans.Rotation.y), 0);
                    }
                    else if (Keyboard.GetKey(KeyCodes.Right))
                    {
                        trans.Rotation = new float3(trans.Rotation.x, (-speed + trans.Rotation.y), 0);
                    }
                }
            }
        }


        // animation

        private void driveForword(float speed)
        {
            if (Keyboard.GetKey(KeyCodes.W))
            {
                float time = -M.MinAngle(TimeSinceStart);
                _frontRightTire.Rotation = new float3((time * speed), 0, 0);
                _frontLeftTire.Rotation = new float3((time * speed), 0, 0);
                _midTireRight.Rotation = new float3((time * speed), 0, 0);
                _midTireLeft.Rotation = new float3((time * speed), 0, 0);
                _backRightTire.Rotation = new float3((time * speed), 0, 0);
                _backLeftTire.Rotation = new float3((time * speed), 0, 0);
            }
            else if (Keyboard.GetKey(KeyCodes.S))
            {
                float time = M.MinAngle(TimeSinceStart);
                _frontRightTire.Rotation = new float3((time * speed), 0, 0);
                _frontLeftTire.Rotation = new float3((time * speed), 0, 0);
                _midTireRight.Rotation = new float3((time * speed), 0, 0);
                _midTireLeft.Rotation = new float3((time * speed), 0, 0);
                _backRightTire.Rotation = new float3((time * speed), 0, 0);
                _backLeftTire.Rotation = new float3((time * speed), 0, 0);
            }

        }

        private void turnLeftRight(float speed)
        {
            speed = speed / 100;
            if (Keyboard.GetKey(KeyCodes.D) && (_frontRightWing.Rotation.y <= M.Pi / 2))
            {
                _frontRightWing.Rotation = new float3(0, ((speed + _frontRightWing.Rotation.y) % M.TwoPi), 0);
                _frontLeftWing.Rotation = new float3(0, ((speed + _frontLeftWing.Rotation.y) % M.TwoPi), 0);

                _backRightWing.Rotation = new float3(0, ((-speed + _backRightWing.Rotation.y) % M.TwoPi), 0);
                _backLeftWing.Rotation = new float3(0, ((-speed + _backLeftWing.Rotation.y) % M.TwoPi), 0);
            }
            else if (Keyboard.GetKey(KeyCodes.A) && (_backRightWing.Rotation.y <= M.Pi / 2))
            {
                _frontRightWing.Rotation = new float3(0, ((-speed + _frontRightWing.Rotation.y) % M.TwoPi), 0);
                _frontLeftWing.Rotation = new float3(0, ((-speed + _frontLeftWing.Rotation.y) % M.TwoPi), 0);

                _backRightWing.Rotation = new float3(0, ((speed + _backRightWing.Rotation.y) % M.TwoPi), 0);
                _backLeftWing.Rotation = new float3(0, ((speed + _backLeftWing.Rotation.y) % M.TwoPi), 0);
            }
        }

        private void weaponTrun(float speed)
        {
            speed = speed / 100;
            if (Keyboard.GetKey(KeyCodes.Q))
            {
                _cannonBase.Rotation = new float3(0, ((-speed + _cannonBase.Rotation.y) % M.TwoPi), 0);
            }
            else if (Keyboard.GetKey(KeyCodes.E))
            {
                _cannonBase.Rotation = new float3(0, ((speed + _cannonBase.Rotation.y) % M.TwoPi), 0);
            }


            if (Keyboard.GetKey(KeyCodes.F) && _cannon.Rotation.x <= M.DegreesToRadians(16))
            {
                _cannon.Rotation = new float3(((speed + _cannon.Rotation.x) % M.TwoPi), 0, 0);
            }

            else if (Keyboard.GetKey(KeyCodes.R) && _cannon.Rotation.x >= M.DegreesToRadians(-60))
            {
                _cannon.Rotation = new float3(((-speed + _cannon.Rotation.x) % M.TwoPi), 0, 0);
            }

        }

        //calculate angel of the wehls and turn the Body
        private void bodyRotationSpeed()
        {

        }


    }

}