namespace Teko.Graphics;

public class Color(byte r, byte g, byte b, byte a)
{
    public byte R = r, G = g, B = b, A = a;

    public static readonly Color White = new Color(255, 255, 255);
    public static readonly Color Black = new Color(0, 0, 0);
    public static readonly Color Red = new Color(255, 0, 0);
    public static readonly Color Blue = new Color(0, 0, 255);
    public static readonly Color Green = new Color(0, 255, 0);
    
    internal SFML.Graphics.Color ToSfmlColor() =>
        new(R, G, B, A);

    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }
    public Color(uint value) : this(
        (byte)((value & 0xff000000) >> 24), 
        (byte)((value & 0x00ff0000) >> 16), 
        (byte)((value & 0x0000ff00) >> 8), 
        (byte)(value & 0x000000ff)
        ) {}
}