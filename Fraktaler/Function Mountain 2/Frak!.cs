using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

public class DrawPicture : Form
{
    int minpixWidth = 1;
	int zoomFactor = 10;
    int maxIter     = 1024;
	int maxColour   = 1024;
    int scrWidth    = 1920;
    int scrHeight   = 1080;

    double paramA = 0;
    double paramB = 0;
    
    string functionType;
    string graphName;

	public int MaxColour { get => maxColour; set => maxColour = value; }

	public DrawPicture()
    {
        // functionType = "Mandelbrot";
         functionType = "z^2"; paramA = -0.8; paramB = 0.156;
		// functionType = "z^3"; paramA = 0.4; paramB = 0;
		// functionType = "z^4"; paramA = 0.484; paramB = 0;
		// functionType = "z^5"; paramA = 0.544; paramB = 0;

		// scrWidth = screen.Primaryscreen.Bounds.Width;
		// scrHeight = screen.Primaryscreen.Bounds.Height;

		// Initiera skärmen med att anropa grafikrutinen
		if (functionType == "Mandelbrot") graphName = "Mandelbrot z'= z^2 + c";
        else graphName = "Julia Set z' = " + functionType + " + " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";

        this.Text = graphName;
        this.Size = new Size(scrWidth, scrHeight);
		//this.Opacity = .75;
		//this.StartPosition = 0;
		this.Paint += new PaintEventHandler(Paintgraph);
    }

     void Paintgraph(object sender, PaintEventArgs e)
    {
        // Rutans nedre vänstra hörn = (pixelX, pixelY)
        for (int pixWidth = 8; pixWidth >= minpixWidth; pixWidth = pixWidth / 2)
            for (int pixelX = 1; pixelX <= scrWidth; pixelX += pixWidth)
                for (int pixelY = 1; pixelY <= scrHeight; pixelY += pixWidth)
                {
                    // Beräkna koordinater för en pixel
                    double coorX = Convert.ToDouble(pixelX - scrWidth / 2)  / scrHeight * 2.8 / zoomFactor + 0.0000235;
                    double coorY = Convert.ToDouble(pixelY - scrHeight / 2) / scrHeight * 2.8 / zoomFactor;
                    
                    // Beräkna iterationer för en pixel
                    // double iterations = FrakIt(coorX - 1.9, coorY - 1.1, paramA, paramB)
                    double iterations = FrakIt(coorX, coorY, paramA, paramB);

                    // Välj färgnummer mellan 0 och 2048
                    int landColor = Convert.ToInt16((iterations %  MaxColour) / MaxColour * 2048);
                    if (landColor < 0) landColor = 0;

                    // Greppa penseln
                    Graphics g = e.Graphics;
                    SolidBrush paintBrush = new SolidBrush(Color.White);

                    // Ställ in rätt nyans på penseln
                    int colorTrans = 0;
                    int colorRed   = 0;
                    int colorGreen = 0;
                    int colorBlue  = 0;

                    /* Black - Red */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = landColor; colorGreen = 0; colorBlue = 0; }
                    else landColor = landColor - 256;
                    /* Red - Yelow */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 255; colorGreen = landColor; colorBlue = 0; }
                    else landColor = landColor - 256;
                    /* Yellow - Green */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 255 - landColor; colorGreen = 255; colorBlue = 0; }
                    else landColor = landColor - 256;
                    /* Green - Teal */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 0; colorGreen = 255; colorBlue = landColor; }
                    else landColor = landColor - 256;
                    /* Teal - Blue */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 0; colorGreen = 255 - landColor; colorBlue = 255; }
                    else landColor = landColor - 256;
                    /* Blue - Purple */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = landColor; colorGreen = 0; colorBlue = 255; }
                    else landColor = landColor - 256;
                    /* Purple - White */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 255; colorGreen = landColor; colorBlue = 255; }
                    else landColor = landColor - 256;
                    /* White - Black */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255; colorRed = 255 - landColor; colorGreen = 255 - landColor; colorBlue = 255 - landColor; }
                    else landColor = landColor - 256;
                    /* overflow --> Black */
                    if (colorTrans == 0 && landColor >= 0 && landColor < 256) { colorTrans = 255 - landColor; colorRed = 0; colorGreen = 0; colorBlue = 0; }

                    paintBrush.Color =
                        Color.FromArgb(255, // Specifies the transparency of the color.
                                       colorRed,   // Specifies the amount of red.
                                       colorGreen, // specifies the amount of green.
                                       colorBlue); // Specifies the amount of blue.

                    e.Graphics. FillPolygon(paintBrush, new Point[]{
                        new Point(pixelX, pixelY),
                        new Point(pixelX + pixWidth, pixelY),
                        new Point(pixelX + pixWidth, pixelY + pixWidth),
                        new Point(pixelX, pixelY + pixWidth)});
                 }
    }

    public double FrakIt(double x, double y, double constA, double constB)
    {
        int iteration = 0;
        double a = x;
        double b = y;
        double aprim = 0;
        double bprim = 0;
        double modulus = 0;

        if (functionType == "Mandelbrot")
        {
            // För Mandelbrot är alltid konstanterna = koordinaterna
            constA = x;
            constB = y;
			aprim = a * a - b * b + constA;
			bprim = 2 * a * b + constB;
		}
		do
        {
            iteration++;

            if (functionType == "z^2" || functionType == "Mandelbrot")
            { 
                aprim = a * a - b * b + constA;
                bprim = 2 * a * b + constB;
            }
            else if (functionType == "z^3")
            {
                aprim = a * a * a - 3 * a * b * b + constA;
                bprim = 3 * a * a * b - b * b * b + constB;
            }
            else if (functionType == "z^4")
            {
                aprim = a * a * a * a - 6 * a * a * b * b + b * b * b * b + constA;
                bprim = 4 * a * a * a * b - 4 * a * b * b * b + constB;
            }
            else if (functionType == "z^5")
            {
                aprim = a * a * a * a * a - 10 * a * a * a * b * b + 5 * a * b * b * b * b + constA;
                bprim = 5 * a * a * a * a * b - 10 * a * a * b * b * b + b * b * b * b * b + constB;
            }

            a = aprim;
            b = bprim;
            modulus = Math.Sqrt(a * a + b * b);
        } while (iteration < maxIter && modulus < 2);

        return --iteration;
    }

    public static void Main()
    {
        Application.Run(new DrawPicture());
    }
    // End of class
}