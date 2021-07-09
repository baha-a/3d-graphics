using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Loader.OpenGL;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;
using Loader;

namespace GraphicProject
{
    public class TexttureInfo
    {
        public int ID;
        public int width;
        public int height;
    }

    public class FilesLoader
    {
        static Dictionary<string, TexttureInfo> textures = new Dictionary<string, TexttureInfo>();
        static Dictionary<string, ModelContainer> models = new Dictionary<string, ModelContainer>();
        static IModelLoader modelLoader = new Loader3DS();

        public static void LoadTextures(string texDir)
        {
            string [] textureDirs = Directory.GetFiles(texDir, "*.jpg");

            foreach (var item in textureDirs)
                textures.Add(item.Substring(texDir.Length).ToLower(), LoadTexture(item));
        }

        public static int GetTextureByName(string textureName)
        {
            return textures[(textureName + ".jpg").ToLower()].ID;
        }


        public static void LoadModels(string modDir)
        {
            string[] modelDirs = Directory.GetFiles(modDir, "*.3ds");

            foreach (string item in modelDirs)
                models.Add(item.Substring(modDir.Length).ToLower(), modelLoader.LoadModel(item));
        }

        public static ModelContainer GetModelByName(string name)
        {
            return models[name.ToLower()].CreateDisplayList();
        }



        public static TexttureInfo LoadTexture(string path)
        {
            int texture;

            Gl.glGenTextures(1, out texture);

            //Gl.glTexParameteri(GL_EXT_TEXTURE_RECTANGLE, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            //Gl.glTexParameteri(GL_EXT_TEXTURE_RECTANGLE, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);

            Bitmap bitmap = new Bitmap(path);

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, bitmap.Width, bitmap.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);

            TexttureInfo info = new TexttureInfo()
            {
                ID = texture,
                width = bitmap.Width,
                height = bitmap.Height
            };

            bitmap.UnlockBits(bitmapData);
            bitmap.Dispose();

            return info;
        }
    }
}
