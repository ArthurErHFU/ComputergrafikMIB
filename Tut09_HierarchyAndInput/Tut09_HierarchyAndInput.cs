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
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _baseTransform;
        private Transform _bodyTransform;

        private Transform _upperArmTransform;
        private Transform _upperArmPivot;

        private Transform _lowerArmPivot;
        private Transform _lowerArmTransform;

        private Transform _grapple1Transform;
        private Transform _grapple1Pivot;

        private Transform _grapple2Transform;
        private Transform _grapple2Pivot;

        //rotation and lerp falues
        private float lerpValue = 0.1f;

        private float bodyRot = 0f;
        private float upperRot = 0f;
        private float lowerRot = 0f;

        private float rotSpeed = 3f;


        //GrappleValues
        private float grapOpen = 45f;
        private float grapClose = -2f;
        private float grapRot = 1f;
        private float grapActualSpeed = 0;

        private bool grapISclosing = false;


        SceneContainer CreateScene()
        {
            // Initialize nCuboid shape
            float3 shape = new float3(2, 10, 2);
            // Initialize GrappleCuboid shape
            float3 gPshape = new float3(1, 4, 1);
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };
            _upperArmPivot = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };
            _upperArmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 4, 0)
            };
            _lowerArmPivot = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 4, 0)
            };
            _lowerArmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 4, 0)
            };
            _grapple1Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 2, 0)
            };
            _grapple1Pivot = new Transform
            {
                Rotation = new float3(0, 0, M.DegreesToRadians(-45)),//45
                Scale = new float3(1, 1, 1),
                Translation = new float3(1, 5, 0)
            };
            _grapple2Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 2, 0)
            };
            _grapple2Pivot = new Transform
            {
                Rotation = new float3(0, 0, M.DegreesToRadians(45)),//45
                Scale = new float3(1, 1, 1),
                Translation = new float3(-1, 5, 0)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                {
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                        //RED BODY
                        new SceneNode
                        {
                            Components = new List<SceneComponent>
                            {
                                _bodyTransform,
                                MakeEffect.FromDiffuseSpecular((float4) ColorUint.Red),
                                SimpleMeshes.CreateCuboid(shape)
                            },
                            //GREEN BODY Pivot
                            Children = new ChildList
                            {
                                new SceneNode
                                {
                                    Components = new List<SceneComponent>
                                    {
                                        _upperArmPivot
                                    },
                                    //GREEN BODY 
                                    Children = new ChildList
                                    {
                                        new SceneNode
                                        {
                                            Components = new List<SceneComponent>
                                            {
                                                _upperArmTransform,
                                                MakeEffect.FromDiffuseSpecular((float4) ColorUint.Green),
                                                SimpleMeshes.CreateCuboid(shape)
                                            },
                                            //BLUE BODY Pivot
                                            Children = new ChildList
                                            {
                                                new SceneNode
                                                {
                                                    Components = new List<SceneComponent>
                                                    {
                                                         _lowerArmPivot

                                                    },
                                                    Children = new ChildList
                                                    {
                                                        new SceneNode
                                                        {
                                                            Components= new List<SceneComponent>
                                                            {
                                                                _lowerArmTransform,
                                                                MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue),
                                                                SimpleMeshes.CreateCuboid(shape)
                                                            },
                                                            Children = new ChildList{
                                                                new SceneNode{
                                                                    Components = new List<SceneComponent>{
                                                                        _grapple1Pivot,
                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.Greenery),
                                                                        SimpleMeshes.CreateCuboid(gPshape)
                                                                    }
                                                                },
                                                                new SceneNode{
                                                                    Components = new List<SceneComponent>{
                                                                        _grapple2Pivot,
                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.Greenery),
                                                                        SimpleMeshes.CreateCuboid(gPshape)
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                }
            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            //RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);
            RC.ClearColor = new float4((float3)ColorUint.WhiteSmoke, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);
            //My Stuff  -->DeltaTime
            //BSP:


            if (Keyboard.GetKey(KeyCodes.NumPad4))
            {
                bodyRot = M.Lerp(bodyRot, rotSpeed, lerpValue);

            }

            else if (Keyboard.GetKey(KeyCodes.NumPad6))
            {
                bodyRot = M.Lerp(bodyRot, -rotSpeed, lerpValue);
            }

            else
            {
                bodyRot = M.Lerp(bodyRot, 0, lerpValue);
            }

            if (Keyboard.GetKey(KeyCodes.NumPad7))
            {
                upperRot = M.Lerp(upperRot, rotSpeed, lerpValue);
            }

            else if (Keyboard.GetKey(KeyCodes.NumPad1))
            {
                upperRot = M.Lerp(upperRot, -rotSpeed, lerpValue);
            }

            else
            {
                upperRot = M.Lerp(upperRot, 0, lerpValue);
            }

            if (Keyboard.GetKey(KeyCodes.NumPad9))
            {
                lowerRot = M.Lerp(lowerRot, rotSpeed, lerpValue);
            }

            else if (Keyboard.GetKey(KeyCodes.NumPad3))
            {
                lowerRot = M.Lerp(lowerRot, -rotSpeed, lerpValue);
            }

            else
            {
                lowerRot = M.Lerp(lowerRot, 0, lerpValue);
            }

            //grapOpen => 45f
            //grapClose => -2f
            //grapRot = openGrap(_grapple2Pivot, grapOpen);
            //grapRot = closeGrap(_grapple2Pivot, grapClose);

            grapRot = closeGrap(_grapple2Pivot, grapClose);
            //KEY PRESS

            if (Keyboard.IsKeyDown(KeyCodes.NumPad5))
            {
                if (grapISclosing)
                {
                    grapISclosing = false;
                }
                else
                {
                    grapISclosing = true;
                }
            }

            if (!grapISclosing)
            {
                grapRot = openGrap(_grapple2Pivot, grapOpen);
            }
            else if (grapISclosing)
            {
                grapRot = closeGrap(_grapple2Pivot, grapClose);
            }

            //armParts
            _bodyTransform.Rotation += new float3(0, bodyRot * Time.DeltaTime, 0);
            _upperArmPivot.Rotation += new float3(upperRot * Time.DeltaTime, 0, 0);
            _lowerArmPivot.Rotation += new float3(lowerRot * Time.DeltaTime, 0, 0);

            //Grapple stuff
            _grapple1Pivot.Rotation += new float3(0, 0, M.DegreesToRadians(grapRot) * Time.DeltaTime);
            _grapple2Pivot.Rotation += new float3(0, 0, M.DegreesToRadians(-grapRot) * Time.DeltaTime);

            //END_BSP

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

        private float openGrap(Transform _grapple2Pivot, float grapOpen)
        {
            //maxspeed -> -50f
            //lerpValues
            if (M.RadiansToDegrees(_grapple2Pivot.Rotation.z) <= grapOpen)
            {
                //Open
                grapActualSpeed = M.Lerp(grapActualSpeed, -50f, lerpValue + 0.3f);

            }
            else if (M.RadiansToDegrees(_grapple2Pivot.Rotation.z) >= grapOpen) //grapOpen = 45f
            {
                //stops
                grapActualSpeed = M.Lerp(grapActualSpeed, 0f, lerpValue + 0.3f);

            }
            return grapActualSpeed;
        }
        private float closeGrap(Transform _grapple2Pivot, float grapClose)
        {
            //speed
            //lerpValues
            if (M.RadiansToDegrees(_grapple2Pivot.Rotation.z) >= grapClose) //grapClose = -2f;
            {
                //Open
                grapActualSpeed = M.Lerp(grapActualSpeed, 50f, lerpValue + 0.3f);
            }
            else if (M.RadiansToDegrees(_grapple2Pivot.Rotation.z) <= grapClose)
            {
                //stops
                grapActualSpeed = M.Lerp(grapActualSpeed, 0f, lerpValue + 0.3f);
            }
            return 0f;
        }
    }


}