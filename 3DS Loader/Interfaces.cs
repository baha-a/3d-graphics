using System;
using System.Collections.Generic;
using System.Text;

namespace Loader
{ 
    public interface IModelLoader
    {
        ModelContainer LoadModel(string modelPath);
    }

    public interface IModel
    {
        void Create();

        void Update();

        void Draw();
    }
}
