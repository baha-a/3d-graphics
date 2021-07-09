using System;
using System.Runtime.InteropServices;
using Tao.OpenGl;

namespace Loader.OpenGL
{
    public class OpenGLControl
    {
        #region Data Types

        // PIXELFORMATDESCRIPTOR flags
        [Flags]
        enum PFD_Flags : uint
        {
            PFD_DOUBLEBUFFER = 0x00000001,
            PFD_STEREO = 0x00000002,
            PFD_DRAW_TO_WINDOW = 0x00000004,
            PFD_DRAW_TO_BITMAP = 0x00000008,
            PFD_SUPPORT_GDI = 0x00000010,
            PFD_SUPPORT_OPENGL = 0x00000020,
            PFD_GENERIC_FORMAT = 0x00000040,
            PFD_NEED_PALETTE = 0x00000080,
            PFD_NEED_SYSTEM_PALETTE = 0x00000100,
            PFD_SWAP_EXCHANGE = 0x00000200,
            PFD_SWAP_COPY = 0x00000400,
            PFD_SWAP_LAYER_BUFFERS = 0x00000800,
            PFD_GENERIC_ACCELERATED = 0x00001000,
            PFD_SUPPORT_DIRECTDRAW = 0x00002000,
        }

        // Structure of PIXELFORMATDESCRIPTOR used by ChoosePixelFormat()
        [StructLayout(LayoutKind.Sequential)]
        struct PIXELFORMATDESCRIPTOR
        {
            public ushort nSize;
            public ushort nVersion;
            public uint dwFlags;
            public byte cColorBits;
            public byte cDepthBits;
            public byte cStencilBits;
        }

        // External Win32 libraries
        const string KER_DLL = "kernel32.dll";	// Import library for Kernel on Win32
        const string OGL_DLL = "opengl32.dll";	// Import library for OpenGL on Win32
        const string GDI_DLL = "gdi32.dll";		// Import library for GDI on Win32
        const string USR_DLL = "user32.dll";	// Import library for User on Win32

        // Class variables
        static uint m_hDC = 0;					// Display device context
        static uint m_hRC = 0;					// OpenGL rendering context

        #endregion

        #region Imported functions

        // Define all of the exported functions from unmanaged Win32 DLLs

        // kernel32.dll unmanaged Win32 DLL
        [DllImport(KER_DLL)]
        public static extern uint LoadLibrary(string lpFileName);

        // opengl32.dll unmanaged Win32 DLL
        [DllImport(OGL_DLL)]
        static extern uint wglGetCurrentContext();
        [DllImport(OGL_DLL)]
        static extern bool wglMakeCurrent(uint hdc, uint hglrc);
        [DllImport(OGL_DLL)]
        static extern uint wglCreateContext(uint hdc);
        [DllImport(OGL_DLL)]
        static extern int wglDeleteContext(uint hglrc);


        // gdi32.dll unmanaged Win32 DLL
        [DllImport(GDI_DLL)]
        unsafe static extern int ChoosePixelFormat(uint hdc, PIXELFORMATDESCRIPTOR* ppfd);
        [DllImport(GDI_DLL)]
        unsafe static extern int SetPixelFormat(uint hdc, int iPixelFormat,
                                                              PIXELFORMATDESCRIPTOR* ppfd);

        // user32.dll unmanaged Win32 DLL
        [DllImport(USR_DLL)]
        static extern uint GetDC(uint hWnd);
        [DllImport(USR_DLL)]
        static extern int RelaseDC(uint hWnd, uint hDC);

        #endregion

        ~OpenGLControl()
        {
            if (m_hRC != 0)
                wglDeleteContext(m_hRC);
        }

        public static unsafe bool OpenGLInit(ref uint handle, int width, int height, ref string error)
        {
            uint m_hModuleOGL = LoadLibrary(OGL_DLL); // Explicitly load the OPENGL32.DLL library

            // Retrieve a handle to display device context for client area of specified window
            uint hdc = GetDC((uint)handle);

            PFD_Flags BitFlags = new PFD_Flags();
            BitFlags |= PFD_Flags.PFD_DRAW_TO_WINDOW | PFD_Flags.PFD_SUPPORT_OPENGL | PFD_Flags.PFD_DOUBLEBUFFER;

            PIXELFORMATDESCRIPTOR pfd;
            pfd.nSize = (ushort)sizeof(PIXELFORMATDESCRIPTOR);
            pfd.nVersion = 1;
            pfd.dwFlags = (uint)BitFlags;
            pfd.cColorBits = 24;

            pfd.cDepthBits = 16;
            pfd.cStencilBits = 1;

            int iPixelformat;
            if ((iPixelformat = ChoosePixelFormat(hdc, &pfd)) == 0)
                return false;

            if (SetPixelFormat(hdc, iPixelformat, &pfd) == 0)
                return false;

            m_hRC = wglCreateContext(hdc); // Create a new OpenGL rendering contex

            if (m_hRC == 0)
                return false;

            m_hDC = hdc;
            handle = hdc;

            if (!wglMakeCurrent(m_hDC, m_hRC))
                return false;

            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            Gl.glClearDepth(1.0f);
            Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
            Gl.glClearColor(0, 0, 0, 1f);

            return true;
        }
    }
}
