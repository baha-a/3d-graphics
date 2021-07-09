using Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace GraphicProject
{
    public class World
    {
        ModelContainer 
            windmill,
            radioStaion,
            waterTower,
            observatory,
            tank,
            billboard;

        public Flag flag;
        public Train train;

        public World()
        {
            flag = new Flag("flag");
            train = new Train().Create();

            tank = FilesLoader.GetModelByName("tank.3DS");
            windmill = FilesLoader.GetModelByName("windmill.3DS");

            radioStaion = FilesLoader.GetModelByName("radio.3DS");
            waterTower = FilesLoader.GetModelByName("watertower.3DS");
            observatory = FilesLoader.GetModelByName("observatory.3DS");

            billboard = FilesLoader.GetModelByName("billboard.3DS");
        }

        public void Draw()
        {
            flag.Draw();
            skyboxDraw();
            train.Draw();

            drawWindmill();
            drawRadio();
            drawWaterTower();
            drawObservatory();
            drawTank();
            drawBillboard();
        }

        public void skyboxDraw()
        {
            int width = 960;
            int length = 960;

            int height = 400;

            int x = 10;
            int y = -3;
            int z = 7;

            x = x - width / 2;
            y = y - height / 2;
            z = z - length / 2;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("back"));

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y, z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y, z);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("front"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y, z + length);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y, z + length);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("top"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("left"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y, z + length);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y, z);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("right"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y, z);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y, z + length);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, FilesLoader.GetTextureByName("GRASS2"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(8.0f, 0.0f); Gl.glVertex3d(x, 1, z);
            Gl.glTexCoord2f(8.0f, 8.0f); Gl.glVertex3d(x, 1, z + length);
            Gl.glTexCoord2f(0.0f, 8.0f); Gl.glVertex3d(x + width, 1, z + length);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, 1, z);
            Gl.glEnd();

            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }


        private void drawBillboard()
        {
            float size = 0.005f;
            Gl.glPushMatrix();

            Gl.glTranslated(200, 1, 60);
            Gl.glScalef(size, size, size);
            Gl.glRotated(-60, 0, 1, 0);
            billboard.DrawWithTextures();

            Gl.glPopMatrix();
        }

        private void drawTank()
        {
            float size = 0.08f;
            Gl.glPushMatrix();

            movingTank();

            Gl.glTranslated(-250, 1.1, tankTrack);
            Gl.glRotated(tankAngle, 0, 1, 0);
            Gl.glScalef(size, size, size);
            tank.DrawWithTextures();
            Gl.glPopMatrix();
        }
        bool rotatTank = false;
        double tankTrack = 199, tankmove=-1, tankAngle=0, tankrotat=2;
        private void movingTank()
        {
            if (tankTrack == 0 || tankTrack == 200)
                rotatTank = true;

            if (!rotatTank)
                tankTrack += tankmove;

            if (rotatTank)
            {
                tankAngle -= tankrotat;
                if (tankAngle <= -180 || tankAngle >= 0)
                {
                    rotatTank = false;
                    tankrotat = -tankrotat;

                    if (tankTrack == 200 || tankTrack == 0)
                    {
                        tankmove = -tankmove;
                        tankTrack += tankmove;
                    }
                }
            }
        }


        private void drawObservatory()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(-200, 45, 200);
            observatory.DrawWithTextures();
            Gl.glPopMatrix();
        }

        private void drawWaterTower()
        {
            float size = 0.5f;

            Gl.glPushMatrix();
            Gl.glTranslated(200, 1, -200);
            Gl.glScalef(size, size, size);
            waterTower.DrawWithTextures();
            Gl.glPopMatrix();
        }
        private void drawRadio()
        {
            float size = 1.6f;

            Gl.glPushMatrix();
            Gl.glTranslated(-180, 0, -200);
            Gl.glScalef(size, size, size);
            radioStaion.DrawWithTextures();
            Gl.glPopMatrix();
        }
        private void drawWindmill()
        {
            float size = 0.1f;

            Gl.glPushMatrix();
            Gl.glTranslated(220, 60, 220);
            Gl.glScalef(size, size, size);
            windmill.DrawWithTextures();
            Gl.glPopMatrix();
        }
    }
}
