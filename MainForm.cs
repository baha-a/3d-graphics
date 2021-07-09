using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loader;
using Loader.OpenGL;
using Tao.OpenGl;
using System.Runtime.InteropServices;

namespace GraphicProject
{
    public partial class MainForm: Form
    {
        uint hdc; 
        int moveCamera;
        World world;


        public MainForm()
        {
            InitializeComponent();

            Cursor.Hide();

            hdc = (uint)Handle;
            string error = "";
            OpenGLControl.OpenGLInit(ref hdc, Width, Height, ref error);

            FilesLoader.LoadTextures("texture\\");
            FilesLoader.LoadModels("model\\");

            world = new World();

            lblInfo.Text =
@"[Escape]  - close
[W], [S] - move for/backward
[T] - Stop/Start the train
[up], [down] change train speed
[R] - Ride the Train

[P] - Go to Center
[F] - Full screen
[K] - Hide Train's Track
[Space] Change camera style

[D1] to [D5] - Change the track
[X] - draw new train's track

NumPad:
[2], [8] - zooming
[5] - Reset Camera to default
[1], [3], [7], [9] - go places
[0] - stop train 2 sec
";
            tmrPaint.Start();
        }

        [DllImport("GDI32.dll")]
        public static extern void SwapBuffers(uint hdc);

        private void tmrPaint_Tick(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Camera.Update(moveCamera); 
            
            world.Draw();
            lblDebugInfo.Text = string.Format("Camera:\n  X= {0:N2}, Y= {1:N2}, Z= {2:N2}\n V. = {3:N0}, H. = {4:N0}, Zoom = {5}\nTrain\n  X = {6:N2}" +
                "  Z = {7:N2}\n  RotateAngle = {8:N2}  Speed = {9:N2}\nCameraMode = {10},   InTrain? ={11}",
                Camera.eyex, Camera.eyey, Camera.eyez, Camera.angleX, Camera.angleY, Camera.zooming, Train.curnt.X, Train.curnt.Z,
                (-Train.theta), Train.Speed, (Camera.freestyle ? "FreeStyle" : "Human"), Camera.InTrain);



            SwapBuffers(hdc);
            Gl.glFlush();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Activate();
        }

        string ps;
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
                Camera.SpeedChanger();

            if (e.KeyCode == Keys.D1)
                Train.ChangeTrack(0);
            if (e.KeyCode == Keys.D2)
                Train.ChangeTrack(1);
            if (e.KeyCode == Keys.D3)
                Train.ChangeTrack(2);
            if (e.KeyCode == Keys.D4)
                Train.ChangeTrack(3);
            if (e.KeyCode == Keys.D5)
                Train.ChangeTrack(4);
            if (e.KeyCode == Keys.D6)
                Train.ChangeTrack(5);

            if (e.KeyCode == Keys.Space)
                Camera.changeCamera();

            if (e.KeyCode == Keys.K)
                Train.DrawTrainTrack = !Train.DrawTrainTrack;

            if (e.KeyCode == Keys.N)
                Camera.eyey--;
            if (e.KeyCode == Keys.M)
                Camera.eyey++;

            if (e.KeyCode == Keys.Up)
                if (Train.Speed < 10)
                    Train.Speed += 0.1;
            if (e.KeyCode == Keys.Down)
                if (Train.Speed > 0.2)
                    Train.Speed -= 0.1;

            if (e.KeyCode == Keys.X) //// to select track nodes menualy
            {
                if (Train.isMoving)
                {
                    Train.track.Clear();
                    Train.isMoving = false;
                }
                Train.track.Add(new Point(Math.Round(-Camera.eyex, 2), Math.Round(-Camera.eyez, 2)));
                ps += string.Format("track.Add(new Point({0:N}, {1:N}));\n", -Camera.eyex, -Camera.eyez);
                Clipboard.SetText(ps);
            }
            if (e.KeyCode == Keys.Z)
                try
                {
                    Train.track.RemoveAt(Train.track.Count - 1);
                    ps = "";
                    foreach (Point p in Train.track)
                        ps += string.Format("track.Add(new Point({0:N}, {1:N}));\n", p.X, p.Z);
                    Clipboard.SetText(ps);
                }
                catch { }

            if (e.KeyCode == Keys.F)
                FullScreen(); 

            if (e.KeyCode == Keys.P)
                Camera.CenterCameraInZeroZero();

            if (e.KeyCode == Keys.R)
                Camera.JumpToTrain();

            if (e.KeyCode == Keys.NumPad0)
                Train.Station();

            if (e.KeyCode == Keys.NumPad1)
                Camera.GoToLoction(-150, 150);
            if (e.KeyCode == Keys.NumPad3)
                Camera.GoToLoction(150, 150);
            if (e.KeyCode == Keys.NumPad7)
                Camera.GoToLoction(-150, -150);
            if (e.KeyCode == Keys.NumPad9)
                Camera.GoToLoction(200, -150);
 
            if (e.KeyCode == Keys.Escape)
                Close();
            if (e.KeyCode == Keys.W)
                moveCamera = 1;
            if (e.KeyCode == Keys.S)
                moveCamera = -1;

            if(e.KeyCode == Keys.T)
                Train.isMoving = !Train.isMoving;

            if (e.KeyCode == Keys.NumPad8)
                Camera.zooming--;
            if (e.KeyCode == Keys.NumPad2)
                Camera.zooming++;
            if (e.KeyCode == Keys.NumPad5)
                Camera.ResetZoom();

        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
                moveCamera = 0;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                moveCamera = 1;
            if (e.Button == MouseButtons.Right)
                moveCamera = -1;
        }
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            moveCamera = 0;
        }

        public void FullScreen()
        {
            if (WindowState == FormWindowState.Normal)
            {
                TopMost = true;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                Camera.updateViewPort(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
            else
            {
                TopMost = false;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
                WindowState = FormWindowState.Normal;
                Camera.updateViewPort(Width, Height);
            }
        }
        
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            Camera.EnableCurserControling = false;
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            Camera.EnableCurserControling = true;
        }
    }
}
