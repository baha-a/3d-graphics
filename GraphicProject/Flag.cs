using System;
using System.Collections.Generic;
using System.Text;
using Loader;
using Tao.OpenGl;

namespace GraphicProject
{
    public class Flag
    {
        int textureFlag;

        public Flag(string textureflag)
        {
            textureFlag = FilesLoader.GetTextureByName(textureflag);
        }

        public void Draw()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(10, 0, 10);

            Gl.glColor3d(0.7, 0.2, 0.2);
            double r = 0.3;
            int height = 60;
            for (double i = 0 ; i <= 1 ; i += 0.1)
            {
                Gl.glBegin(Gl.GL_POLYGON);
                Gl.glVertex3d(Math.Cos(i * Math.PI * 2) * r, 0, Math.Sin(i * Math.PI * 2) * r);
                Gl.glVertex3d(Math.Cos(i * Math.PI * 2) * r, height, Math.Sin(i * Math.PI * 2) * r);
                i += 0.1;
                Gl.glVertex3d(Math.Cos(i * Math.PI * 2) * r, height, Math.Sin(i * Math.PI * 2) * r);
                Gl.glVertex3d(Math.Cos(i * Math.PI * 2) * r, 0, Math.Sin(i * Math.PI * 2) * r);
                Gl.glEnd();
                i -= 0.1;
            }
            Gl.glColor3d(1, 1, 1);


            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureFlag);
            double h = 15, w = 20;

            Gl.glTranslated(0, height - h, 0);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3d(0, 0, 0);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3d(w, 0, 0);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3d(w, h, 0);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3d(0, h, 0);
            Gl.glEnd();

            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }
    }
}
