using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

public class DrawLandscape : Form
{
    double[,] grid = new double[257, 257];
    Random rnd1 = new Random(1);
        
    // Välj hur höga topparna får vara
    int MountainHeight = 100;

    // Definiera färger 0..12
    Color[] myColor = new Color[13] { Color.DarkBlue, Color.MediumBlue, Color.CornflowerBlue, Color.LightSkyBlue, Color.NavajoWhite,
                                      Color.LightGreen, Color.LimeGreen, Color.DarkGreen, Color.Gray, Color.Silver, Color.LightGray, Color.Snow, Color.White};

    public DrawLandscape()
    {
        for (int i = 0; i < 257; i++)
        {
            for (int j = 0; j < 257; j++)
            {
                grid[i, j] = 9999;
            }
        }

        // Initiera skärmen med att anropa grafikrutinen
        this.Text = "z = sin(r) + sin(2r)";
        this.Size = new Size(1920, 1080);
        this.Paint += new PaintEventHandler(SquareOne);
    }

    public void SquareOne(object sender, PaintEventArgs e)
    {
        // Utgå från den maximala rutstorleken 256*256 pixlar
        int StartWidth = 256;
        DivideSquare(e, 0, 0, StartWidth);
    }

    public double Function(double a, double b)
    {
        double xx = (a - 128) / 29.5;
        double yy = (b - 128) / 29.5;
        double r = Math.Sqrt(xx * xx + yy * yy);

        return Math.Sin(r) + Math.Sin(2 * r) ;
    }

    public void DivideSquare(PaintEventArgs e, int px, int py, int CurWidth)
    {
        // Rutans nedre vänstra hörn = (px, py)
        // Rutans bredd = w (värden 256, 128, 64, 32, 16...)

        int NewWidth = CurWidth / 2;
        double z1 = grid[px, py] = Function(px, py);
        double z2 = grid[px, py + CurWidth] = Function(px, py + CurWidth);
        double z3 = grid[px + CurWidth, py + CurWidth] = Function(px + CurWidth, py + CurWidth);
        double z4 = grid[px + CurWidth, py] = Function(px + CurWidth, py);

        // Ska rutan delas upp i mindre delar?
        if (CurWidth > 4)
        {
            DivideSquare(e, px, py + NewWidth, NewWidth);
            DivideSquare(e, px + NewWidth, py + NewWidth, NewWidth);
            DivideSquare(e, px, py, NewWidth);
            DivideSquare(e, px + NewWidth, py, NewWidth);
        }
        // Annars är denna ruta färdig att visas
        else
        {
            // Räkna ut hörnpositioner för den aktuella rutan 
            int x1 = 0 + (py + 0) * 2 + (px + 0) * 3;
            int y1 = 480 + (px + 0) * 1 - (py + 0) * 2;
            int x2 = 0 + (py + CurWidth) * 2 + (px + 0) * 3;
            int y2 = 480 + (px + 0) * 1 - (py + CurWidth) * 2;
            int x3 = 0 + (py + CurWidth) * 2 + (px + CurWidth) * 3;
            int y3 = 480 + (px + CurWidth) * 1 - (py + CurWidth) * 2;
            int x4 = 0 + (py + 0) * 2 + (px + CurWidth) * 3;
            int y4 = 480 + (px + CurWidth) * 1 - (py + 0) * 2;

            // Välj en färg 0..12
            int LandColor = Convert.ToInt16(((z1 + z2 + z3 + z4)) * 50) + 512;
            if (LandColor < 0) LandColor = 0;
            if (z1 < -0.0) z1 = -0.0;
            if (z2 < -0.0) z2 = -0.0;
            if (z3 < -0.0) z3 = -0.0;
            if (z4 < -0.0) z4 = -0.0;

            // Måla den aktuella rutan på skärmen
            Graphics g = e.Graphics;
            SolidBrush MapBrush = new SolidBrush(Color.White);

            int ColorTrans = 0;
            int ColorRed   = 0;
            int ColorGreen = 0;
            int ColorBlue  = 0;
            if (LandColor < 0) LandColor *= -1;

            /* Black - Red */    if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = LandColor;       ColorGreen = 0;               ColorBlue = 0; }
            else LandColor = LandColor - 256;
            /* Red - Yelow */    if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 255;             ColorGreen = LandColor;       ColorBlue = 0; }
            else LandColor = LandColor - 256;
            /* Yellow - Green */ if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 256 - LandColor; ColorGreen = 255;             ColorBlue = 0; }
            else LandColor = LandColor - 256;
            /* Green - Teal */   if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 0;               ColorGreen = 255;             ColorBlue = LandColor; }
            else LandColor = LandColor - 256;
            /* Teal - Blue */    if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 0;               ColorGreen = 256 - LandColor; ColorBlue = 255; }
            else LandColor = LandColor - 256;
            /* Blue - Purple */  if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = LandColor;       ColorGreen = 0;               ColorBlue = 255; }
            else LandColor = LandColor - 256;
            /* Purple - White */ if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 255;             ColorGreen = LandColor;       ColorBlue = 255; }
            else LandColor = LandColor - 256;
            /* White - Black */  if (ColorTrans == 0 && LandColor > 0 && LandColor < 256) { ColorTrans = 255; ColorRed = 256 - LandColor; ColorGreen = 256 - LandColor; ColorBlue = 256 - LandColor; }
            else LandColor = LandColor - 256;

            MapBrush.Color = 
                Color.FromArgb(ColorTrans, // Specifies the transparency of the color.
                               ColorRed,   // Specifies the amount of red.
                               ColorGreen, // specifies the amount of green.
                               ColorBlue); // Specifies the amount of blue.

            e.Graphics.FillPolygon(MapBrush, new Point[]{
                new Point(x1, y1 - Convert.ToInt16(z1 * MountainHeight)),
                new Point(x2, y2 - Convert.ToInt16(z2 * MountainHeight)),
                new Point(x3, y3 - Convert.ToInt16(z3 * MountainHeight)),
                new Point(x4, y4 - Convert.ToInt16(z4 * MountainHeight))});

            // Måla en ram runt rutan
            Pen edgeBrush = new Pen(Color.DimGray, 1);
            g.DrawPolygon(edgeBrush, new Point[]{
                new Point(x1, y1 - Convert.ToInt16(z1 * MountainHeight)),
                new Point(x2, y2 - Convert.ToInt16(z2 * MountainHeight)),
                new Point(x3, y3 - Convert.ToInt16(z3 * MountainHeight)),
                new Point(x4, y4 - Convert.ToInt16(z4 * MountainHeight))});
        }
    }

    public static void Main()
    {
        Application.Run(new DrawLandscape());
    }
    // End of class
}