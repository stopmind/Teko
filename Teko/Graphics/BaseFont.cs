using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace Teko.Graphics;

internal class BaseFont : SFML.Graphics.Font
{
    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfFont_destroy(IntPtr cPtr);
    
    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfFont_createFromStream(IntPtr stream);
    
    private readonly StreamAdaptor _adaptor;
    
    public BaseFont(Stream stream) : base(stream)
    {
        _adaptor = new StreamAdaptor(stream);
        sfFont_destroy(CPointer);
        CPointer = sfFont_createFromStream(_adaptor.InputStreamPtr);
    }
}