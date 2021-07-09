using Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Tao.OpenGl;

namespace GraphicProject
{
    public class Train
    {
        public static double theta = 0,size = 0.02,Speed=1;

        public static bool isMoving = true,DrawTrainTrack = true;

        public static List<Point> track = new List<Point>();
        public static Point curnt;
        private static int nextNodeIndex = 0;


        public static double x { get { return curnt.X; } }
        public static double z { get { return curnt.Z; } }



        public static int h = 20 , w = 4;
        public static bool InTrainBoundaries(double px, double pz)
        {
            double 
                x0 = x + h, z0 = z + w,
                x1 = x - h, z1 = z + w,
                x2 = x - h, z2 = z - w,
                x3 = x + h, z3 = z - w;

            double ang = Camera.DegreeToRad(-theta);

            Xs = new double[] {   (Math.Cos(ang) * (x0 - x) - Math.Sin(ang) * (z0 - z) + x),
                                  (Math.Cos(ang) * (x1 - x) - Math.Sin(ang) * (z1 - z) + x),
                                  (Math.Cos(ang) * (x2 - x) - Math.Sin(ang) * (z2 - z) + x),
                                  (Math.Cos(ang) * (x3 - x) - Math.Sin(ang) * (z3 - z) + x)};

            Zs = new double[] {   (Math.Sin(ang) * (x0 - x) + Math.Cos(ang) * (z0 - z) + z),
                                  (Math.Sin(ang) * (x1 - x) + Math.Cos(ang) * (z1 - z) + z),
                                  (Math.Sin(ang) * (x2 - x) + Math.Cos(ang) * (z2 - z) + z),
                                  (Math.Sin(ang) * (x3 - x) + Math.Cos(ang) * (z3 - z) + z)};

            //ceyeX = (Math.Cos(ang) * (Camera.eyex - x) - Math.Sin(ang) * (Camera.eyez - z) + x);
            //ceyeZ = (Math.Sin(ang) * (Camera.eyex - x) + Math.Cos(ang) * (Camera.eyez - z) + z);


            return (px <= Xs.Max() && px >= Xs.Min()) && (pz <= Zs.Max() && pz >= Zs.Min());
        }
        public static double[] Xs=new double[0],Zs=new double[0];

        public Point rotate_point(double cx, double cz, double angle, Point p)
        {
            double s = Math.Sin(Camera.DegreeToRad(angle));
            double c = Math.Cos(Camera.DegreeToRad(angle));

            // translate point back to origin:
            p.X -= cx;
            p.Z -= cz;

            double xnew, ynew;
            // rotate point

            xnew = p.X * c + p.Z * s;
            ynew = -p.X * s + p.Z * c;

            // translate point back:
            p.X = xnew + cx;
            p.Z = ynew + cz;
            return p;
        }
        ModelContainer m;


        public Train Create()
        {
            m = FilesLoader.GetModelByName("train.3DS").CreateDisplayList();

            ChangeTrack();
            return this;
        }

        private static void newTrack0()
        {
            // these lines generated programmatically:
            track.Add(new Point(9.06, 125.84));
            track.Add(new Point(35.05, 124.00));
            track.Add(new Point(62.99, 123.00));
            track.Add(new Point(90.18, 123.01));
            track.Add(new Point(118.96, 124.03));
            track.Add(new Point(133.23, 125.85));
            track.Add(new Point(158.52, 133.19));
            track.Add(new Point(194.31, 149.10));
            track.Add(new Point(218.90, 166.88));
            track.Add(new Point(231.11, 180.54));
            track.Add(new Point(237.57, 198.57));
            track.Add(new Point(240.67, 216.66));
            track.Add(new Point(237.62, 237.12));
            track.Add(new Point(227.39, 262.28));
            track.Add(new Point(217.31, 276.62));
            track.Add(new Point(204.05, 283.87));
            track.Add(new Point(186.82, 290.25));
            track.Add(new Point(163.87, 293.28));
            track.Add(new Point(145.82, 287.33));
            track.Add(new Point(132.65, 272.40));
            track.Add(new Point(121.83, 253.73));
            track.Add(new Point(118.03, 235.80));
            track.Add(new Point(119.76, 215.88));
            track.Add(new Point(122.66, 196.94));
            track.Add(new Point(131.32, 168.67));
            track.Add(new Point(156.70, 101.29));
            track.Add(new Point(164.44, 81.13));
            track.Add(new Point(176.77, 49.01));
            track.Add(new Point(183.67, 26.07));
            track.Add(new Point(187.24, 6.40));
            track.Add(new Point(189.67, -16.66));
            track.Add(new Point(188.99, -39.82));
            track.Add(new Point(185.91, -60.37));
            track.Add(new Point(175.42, -93.96));
            track.Add(new Point(168.51, -111.85));
            track.Add(new Point(157.90, -130.54));
            track.Add(new Point(140.30, -151.27));
            track.Add(new Point(122.77, -167.60));
            track.Add(new Point(105.24, -178.75));
            track.Add(new Point(86.72, -183.12));
            track.Add(new Point(63.55, -182.41));
            track.Add(new Point(39.03, -178.79));
            track.Add(new Point(19.33, -175.32));
            track.Add(new Point(-1.16, -171.78));
            track.Add(new Point(-21.87, -170.96));
            track.Add(new Point(-40.30, -175.98));
            track.Add(new Point(-65.12, -187.06));
            track.Add(new Point(-99.53, -210.39));
            track.Add(new Point(-123.98, -231.00));
            track.Add(new Point(-153.80, -250.93));
            track.Add(new Point(-184.05, -261.23));
            track.Add(new Point(-210.82, -269.46));
            track.Add(new Point(-235.00, -274.90));
            track.Add(new Point(-254.09, -275.04));
            track.Add(new Point(-265.62, -267.96));
            track.Add(new Point(-278.06, -256.71));
            track.Add(new Point(-283.78, -245.29));
            track.Add(new Point(-291.45, -226.83));
            track.Add(new Point(-294.27, -206.30));
            track.Add(new Point(-287.69, -189.29));
            track.Add(new Point(-275.56, -172.42));
            track.Add(new Point(-264.36, -161.01));
            track.Add(new Point(-246.62, -144.86));
            track.Add(new Point(-226.37, -129.21));
            track.Add(new Point(-204.84, -113.95));
            track.Add(new Point(-181.94, -99.28));
            track.Add(new Point(-164.30, -86.83));
            track.Add(new Point(-149.63, -73.28));
            track.Add(new Point(-137.81, -54.45));
            track.Add(new Point(-133.59, -34.21));
            track.Add(new Point(-133.92, -14.35));
            track.Add(new Point(-147.80, 31.48));
            track.Add(new Point(-155.14, 50.08));
            track.Add(new Point(-160.25, 67.72));
            track.Add(new Point(-163.15, 89.07));
            track.Add(new Point(-161.78, 113.74));
            track.Add(new Point(-151.38, 140.40));
            track.Add(new Point(-138.31, 155.43));
            track.Add(new Point(-121.65, 166.39));
            track.Add(new Point(-97.25, 176.34));
            track.Add(new Point(-70.97, 183.15));
            track.Add(new Point(-51.92, 185.34));
            track.Add(new Point(-30.54, 183.39));
            track.Add(new Point(-15.16, 173.69));
            track.Add(new Point(-1.28, 155.27));
        }
        private static void newTrack1()
        {
            // these lines generated programmatically:
            track.Add(new Point(0.0, -125.18));
            track.Add(new Point(-65.55, -123.03));
            track.Add(new Point(-148.79, -124.53));
            track.Add(new Point(-223.96, -175.50));
            track.Add(new Point(-233.59, -232.93));
            track.Add(new Point(-212.37, -280.12));
            track.Add(new Point(-151.29, -290.34));
            track.Add(new Point(-117.53, -236.58));
            track.Add(new Point(-127.80, -156.62));
            track.Add(new Point(-162.20, -73.24));
            track.Add(new Point(-180.21, -1.96));
            track.Add(new Point(-180.94, 62.38));
            track.Add(new Point(-159.30, 124.92));
            track.Add(new Point(-126.36, 166.76));
            track.Add(new Point(-95.57, 181.10));
            track.Add(new Point(-67.07, 180.73));
            track.Add(new Point(3.38, 165.99));
            track.Add(new Point(54.75, 179.81));
            track.Add(new Point(102.15, 203.06));
            track.Add(new Point(151.02, 249.47));
            track.Add(new Point(255.68, 279.15));
            track.Add(new Point(293.61, 213.82));
            track.Add(new Point(210.42, 111.35));
            track.Add(new Point(138.17, 77.01));
            track.Add(new Point(134.03, 12.60));
            track.Add(new Point(162.19, -69.84));
            track.Add(new Point(140.72, -157.75));
            track.Add(new Point(20.28, -180.29));
        }

        private static void newTrack2()
        {
            // these lines generated programmatically:
            track.Add(new Point(-101.67, 307.33));
            track.Add(new Point(-73.55, -208.51));
            track.Add(new Point(-65.13, -235.30));
            track.Add(new Point(-56.61, -248.78));
            track.Add(new Point(-39.30, -258.57));
            track.Add(new Point(-13.09, -268.35));
            track.Add(new Point(10.40, -272.99));
            track.Add(new Point(34.29, -274.59));
            track.Add(new Point(59.89, -270.45));
            track.Add(new Point(94.40, -260.34));
            track.Add(new Point(118.47, -248.45));
            track.Add(new Point(139.41, -224.91));
            track.Add(new Point(164.52, 221.58));
            track.Add(new Point(150.80, 244.79));
            track.Add(new Point(135.52, 263.09));
            track.Add(new Point(121.02, 273.72));
            track.Add(new Point(98.65, 286.91));
            track.Add(new Point(77.93, 296.90));
            track.Add(new Point(58.26, 304.14));
            track.Add(new Point(33.86, 309.47));
            track.Add(new Point(5.19, 313.79));
            track.Add(new Point(-24.64, 315.62));
            track.Add(new Point(-46.58, 317.05));
            track.Add(new Point(-64.55, 317.34));
            track.Add(new Point(-78.52, 316.40));
            track.Add(new Point(-88.41, 314.91));
        }
        private static void newTrack3()
        {
            for (double angle = 0 ; angle <= 1 ; angle += 0.01)
                track.Add(new Point((Math.Sin(angle * Math.PI * 2) * 150), (Math.Cos(angle * Math.PI * 2) * 200)));
        }
        private static void newTrack4()
        {
            double r = 150;
            for (double angle = 0 ; angle <= 1 ; angle += 0.01)
                track.Add(new Point((Math.Sin(angle * Math.PI * 2) * r), (Math.Cos(angle * Math.PI * 2) * r)));
        }
        private static void newTrack5()
        {
            // these lines generated programmatically:
            track.Add(new Point(-189.23, -103.77));
            track.Add(new Point(-66.96, -77.78));
            track.Add(new Point(37.59, -85.58));
            track.Add(new Point(161.72, -71.31));
            track.Add(new Point(226.85, -34.29));
            track.Add(new Point(261.32, 8.52));
            track.Add(new Point(288.56, 56.30));
            track.Add(new Point(309.67, 117.61));
            track.Add(new Point(321.10, 171.40));
            track.Add(new Point(290.78, 228.90));
            track.Add(new Point(255.79, 228.05));
            track.Add(new Point(162.82, 258.59));
            track.Add(new Point(123.05, 262.61));
            track.Add(new Point(92.32, 237.01));
            track.Add(new Point(53.91, 205.00));
            track.Add(new Point(-11.96, 181.29));
        }

        public static void ChangeTrack(int indx = 0)
        {
            track.Clear();

            switch (indx)
            {
                case 0: newTrack0(); break;
                case 1: newTrack1(); break;
                case 2: newTrack2(); break;
                case 3: newTrack3(); break;
                case 4: newTrack4(); break;
                case 5: newTrack5(); break;
            }
            if (track.Count > 0)
                curnt = track[0];
            nextNodeIndex = 0;
        }



        private void drawTrack()
        {
            Gl.glPushMatrix();
            Point mov = track[0];

            int t=0;
            double thta=0;
            bool endflag = false;
            for ( ; true ; )
            {
                if (mov == track[t])
                {
                    if (endflag)
                        break;

                    if (t + 1 == track.Count)
                        endflag = true;

                    t = (t + 1) % track.Count;
                    
                    Point relativePos = track[t] - mov;
                    thta = Math.Atan2(relativePos.Z, relativePos.X) * -180.0 / Math.PI;


                    Gl.glColor3d(0.6, 0.6, 0.6);
                    Point mid = mov.MidPointWith(track[t]);
                    double dist = mov.DistanceFrom(track[t]);


                    // drawing two bar of iron of track
                    Gl.glPushMatrix();
                    Point md=new Point(mid.X, mid.Z);
                    md.X += 3;
                    md.Z += 3;
                    rotate_point(mid.X, mid.Z, 90, md);

                    Gl.glTranslated(md.X, 1.2, md.Z);
                    Gl.glRotated(thta, 0, 1, 0);
                    Gl.glScaled(dist, 1, 1);
                    Glut.glutSolidCube(1);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    md = new Point(mid.X, mid.Z);
                    md.X -= 3;
                    md.Z -= 3;
                    rotate_point(mid.X, mid.Z, 90, md);


                    Gl.glTranslated(md.X, 1.2, md.Z);
                    Gl.glRotated(thta, 0, 1, 0);
                    Gl.glScaled(dist, 1, 1);
                    Glut.glutSolidCube(1);
                    Gl.glPopMatrix();


                    thta += 90;
                    continue;
                }
                else
                    mov = mov.moveTo(track[t], 10);


                // drawing the woods of track
                Gl.glColor3d(1, 0.4, 0.2);
                Gl.glPushMatrix();
                Gl.glTranslated(mov.X, 1, mov.Z);
                Gl.glRotated(thta, 0, 1, 0);
                Gl.glScaled(10, 1, 1);
                Glut.glutSolidCube(1);
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
            Gl.glColor3d(1, 1, 1);
        }


        public void Draw()
        {
            if (track.Count == 0)
                return;

            if (DrawTrainTrack)
                drawTrack();

            Gl.glPushMatrix();

            if (isMoving)
            {
                if (curnt == track[nextNodeIndex])
                {
                    nextNodeIndex = (nextNodeIndex + 1) % track.Count;

                    // pointing train to next Target
                    Point relativePos = track[nextNodeIndex] - curnt;
                    theta = Math.Atan2(relativePos.Z, relativePos.X) * -180.0 / Math.PI;
                }
                else
                    curnt = curnt.moveTo(track[nextNodeIndex]);
            }

            Gl.glTranslated(curnt.X, 1, curnt.Z);
            Gl.glRotated(theta, 0, 1, 0);
            Gl.glScaled(size, size, size);
            m.DrawWithTextures();
            Gl.glPopMatrix();


            ////////////// draw collison area for debugging
            ////Gl.glPushMatrix();
            ////Gl.glColor3d(1, 1, 1);
            ////Gl.glBegin(Gl.GL_POLYGON);
            ////for (int i = 0 ; i < Xs.Length ; i++)
            ////    Gl.glVertex3d(Xs[i], -7, Zs[i]);
            ////Gl.glEnd();
            ////Gl.glPopMatrix();
        }


        static bool thredStarted = false;
        public static void Station()
        {
            if (!thredStarted)
            {
                thredStarted = true;
                new Thread(
                            x =>
                            {
                                Camera.JumpToTrain();
                                isMoving = false;
                                Thread.Sleep(2000);
                                isMoving = true;

                                thredStarted = false;
                            }
                           ).Start();
            }
        }
    }
}