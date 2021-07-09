using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using Loader;
using System.Runtime.InteropServices;

namespace GraphicProject
{
    public class Camera
    {
        public static double 
            eyex, eyez, eyey = -7,
            angleY, angleX,
            rotationSpeed = 0.2,
            aspctratio = 1;

        public static int
            forwardSpeed = 1,
            zooming = 60;

        public static bool 
            freestyle = false,
            EnableCurserControling = true,
            InTrain = true;

        static int StairLocation = 0;
        static System.Drawing.Point MouseLastposition = MainForm.MousePosition;
        

        public static void ResetZoom()
        {
            zooming = 60;
        }
        public static double DegreeToRad(double pAngle)
        {
            return (pAngle * Math.PI / 180);
        }
        public static void SpeedChanger()
        {
            if (forwardSpeed == 1)
                forwardSpeed = 5;
            else
                forwardSpeed = 1;
        }
        public static void updateViewPort(int width, int height)
        {
            aspctratio = width * 1.0 / height;
            Gl.glViewport(0, 0, width, height);
        }
        public static void GoToLoction(double x, double z, double y = -7)
        {
            eyex = x;
            eyey = y;
            eyez = z;
        }
        public static void CenterMouse()
        {
            if (EnableCurserControling)
            {
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(400, 400);
                MouseLastposition = MainForm.MousePosition;
            }
        }
        public static void CenterCameraInZeroZero()
        {
            eyex = eyez = 0;
        }
        public static void changeCamera()
        {
            if (freestyle)
            {
                freestyle = false;
                eyey = -7;
            }
            else
                freestyle = true;
        }
        public static void JumpToTrain()
        {
            if (InTrain)
            {
                InTrain = false;
                eyex = -Train.x + eyex;
                eyez = -Train.z + eyez;
            }
            else
            {
                CenterCameraInZeroZero();
                InTrain = true;
            }
        }



        public static void SetupCamera()
        {
            if (zooming < 10)
                zooming = 10;
            else if (zooming > 150)
                zooming = 150;

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(zooming, aspctratio, 0.1, 1000);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();



            Gl.glRotated(angleX, 1, 0, 0);

            if (InTrain)
                Gl.glRotated(angleY - DegreeToRad(Train.theta), 0, 1, 0);
            else
                Gl.glRotated(angleY, 0, 1, 0);


            Gl.glTranslated(eyex, eyey, eyez);

            if (InTrain)
            {
                Gl.glRotated(-Train.theta, 0, 1, 0);
                Gl.glTranslated(-Train.x, 0, -Train.z);
            }
        }

        public static void Update(int pressedButton)
        {
            if (!EnableCurserControling)
                return;

            int difX = (MouseLastposition.X) - MainForm.MousePosition.X;
            int difY = (MouseLastposition.Y) - MainForm.MousePosition.Y;

            if (MainForm.MousePosition.Y < 0)
                angleX -= rotationSpeed * difY;
            else if (MainForm.MousePosition.Y > 0)
                angleX += rotationSpeed * -difY;

            if (MainForm.MousePosition.X < 0)
                angleY += rotationSpeed * -difX;
            else if (MainForm.MousePosition.X > 0)
                angleY -= rotationSpeed * difX;

            MouseLastposition = MainForm.MousePosition;

            CenterMouse();
            SetupCamera();


            if (-eyex >= 196 && -eyex <= 205 && -eyez <= -175 && -eyez >= -180)
                StairLocation = 1;
            else if (-eyex <= -200 && -eyex >= -205 && -eyez <= -208 && -eyez >= -210)
                StairLocation = 2;
            else
                StairLocation = 0;


            if (pressedButton == 1)
            {
                double newEyeX = eyex + -Math.Sin(DegreeToRad(angleY)) * forwardSpeed;
                double newEyeY = eyey + Math.Sin(DegreeToRad(angleX)) * forwardSpeed;
                double newEyeZ = eyez + Math.Cos(DegreeToRad(angleY)) * forwardSpeed;

                if (freestyle && -newEyeY >= 1)
                    eyey = newEyeY;


                if (StairLocation == 0)
                {
                    //if (!InTrain || (InTrain && Train.InTrainBoundaries(newEyeX + Train.x, newEyeZ + Train.z)))
                    {
                        eyex = newEyeX;
                        eyez = newEyeZ;
                    }
                }
                else
                {
                    if (StairLocation == 1 && -eyey < 70)
                        eyey -= forwardSpeed;
                    else if (StairLocation == 2 && -eyey < 90)
                        eyey -= forwardSpeed;
                }
            }

            if (pressedButton == -1)
            {
                double newEyeX = eyex - -Math.Sin(DegreeToRad(angleY)) * forwardSpeed;
                double newEyeY = eyey - Math.Sin(DegreeToRad(angleX)) * forwardSpeed;
                double newEyeZ = eyez - Math.Cos(DegreeToRad(angleY)) * forwardSpeed;

                if (freestyle && -newEyeY >= 1)
                    eyey = newEyeY;

                if (StairLocation == 0)
                {
                    //if (!InTrain || (InTrain && Train.InTrainBoundaries(newEyeX + Train.x, newEyeZ + Train.z)))
                    {
                        eyex = newEyeX;
                        eyez = newEyeZ;
                    }
                }
                else
                {
                    eyey += forwardSpeed;
                    if (-eyey <= 7.3f)
                    {
                        eyey = -7.3f;
                        eyez--;
                        StairLocation = 0;
                    }
                }
            }
        }
    }
}