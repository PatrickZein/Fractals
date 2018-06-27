using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Input;

public class DrawPicture : Form
{
    int scrWidth = Screen.PrimaryScreen.Bounds.Width;
    int scrHeight = Screen.PrimaryScreen.Bounds.Height;

	double zoomFactor  = 1.5;
    int    maxIter     = 2048;
	int    maxColour   = 2048;
    double targetX     = 0;
    double targetY     = 0;
    double paramA      = 0;
    double paramB      = 0;
    int    minpixWidth = 1;

    string functionType;
    string graphName;
    bool   repaint = true;

    public int MaxColour { get => maxColour; set => maxColour = value; }

    private void mouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.Text = "[Left Button Click]";
        }
    }

    public void setHeader()
    {
        if (functionType == "Mandelbrot") graphName = "Mandelbrot z'= z^2 + c";
        else if (functionType == "Julia") graphName = "Julia Set z' = z " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";
        else graphName = "Julia Set z' = " + functionType + " + " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";

        graphName = graphName + "     [maxiter: " + maxIter
                                + "] [colours: " + maxColour
                                + "] [x: " + targetX.ToString()
                                + "] [y: " + targetY.ToString()
                                + "] [zoom: " + zoomFactor.ToString() + "]";

        this.Text = graphName;
    }

    public DrawPicture()
    {
        // functionType = "Mandelbrot"; maxColour = 256; maxIter = 2048;
        functionType = "Julia"; paramA = -0.5; paramB = 0.6; maxColour = 256; maxIter = 2048;
        functionType = "Julia"; paramA = -0.835; paramB = -0.2321; maxColour = 256; maxIter = 2048;
        // functionType = "Julia"; paramA = -0.75; paramB = 0.11; maxColour = 256; maxIter = 2048;

        // functionType = "z^2"; paramA = -0.8; paramB = 0.156;
        // functionType = "z^2"; paramA = -0.5; paramB = 0.59999; maxColour = 256;
        // functionType = "z^2"; paramA = -0.1; paramB = 0.6517; maxColour = 2048;
        // functionType = "z^2"; paramA = -0.456; paramB = 0.5678; maxColour = 512;
        // functionType = "z^3"; paramA = 0.4; paramB = 0;
        // functionType = "z^3"; paramA = -0.475; paramB = 0.567; maxColour = 1024;
        // functionType = "z^4"; paramA = 0.484; paramB = 0;
        // functionType = "z^4"; paramA = 0.620855; paramB = 0.69; maxColour = 256;
        // functionType = "z^5"; paramA = 0.544; paramB = 0; maxColour = 256;
        // functionType = "z^5"; paramA = 0.463; paramB = 0.66; maxColour = 256;
        // functionType = "z^5"; paramA = 0.55; paramB = 0.762; maxColour = 256;
        // functionType = "z^6"; paramA = 0.5899; paramB = 0.0007; maxColour = 1024;
        // functionType = "z^7"; paramA = 0.6267; paramB = 0.001; maxColour = 1024;
        // functionType = "z^8"; paramA = 0.679555; paramB = 0.00735; maxColour = 256; maxIter = 2048;
        // functionType = "z^9"; paramA = 0.684999; paramB = 0.001; maxColour = 256; maxIter = 2048;
        // functionType = "z^9"; paramA = 0.7; paramB = 0.0068; maxColour = 256; maxIter = 2048;
        // functionType = "z^9"; paramA = 0.701; paramB = 0.0068; maxColour = 256; maxIter = 2048; zoomFactor = 2;
        // functionType = "z^9"; paramA = 0.7015022; paramB = 0.0068; maxColour = 256; maxIter = 2048; zoomFactor = 1;

        // functionType = "exp(z)"; paramA = -0.988; paramB = 0.003; maxColour = 256; maxIter = 256;
        // functionType = "exp(z^2)"; paramA = -0.5; paramB = -0.17; maxColour = 256; maxIter = 1048; zoomFactor = 0.25;
        // functionType = "exp(z^3)"; paramA = -0.620921; paramB = 0.00127; maxColour = 256; maxIter = 265; zoomFactor = 0.5;
        // functionType = "exp(z^3)"; paramA = -0.615; paramB = 0.0022; maxColour = 256; maxIter = 265; zoomFactor = 10;
        // functionType = "exp(z^4)"; paramA = -0.53; paramB = 0.00057; maxColour = 256; maxIter = 265; zoomFactor = 0.5;
        // functionType = "exp(z^5)"; paramA = -0.54; paramB = 0.77; maxColour = 256; maxIter = 512; zoomFactor = 0.75;

        // functionType = "z*exp(z)"; paramA = 0.01; paramB = 0.00015; maxColour = 512; maxIter = 1024;
        // functionType = "z^2*exp(z)"; paramA = 0.21; paramB = 0.009; maxColour = 512; maxIter = 1024;
        // functionType = "z^3*exp(z)"; paramA = 0.45; paramB = 0.2; maxColour = 512; maxIter = 512;
        // functionType = "z^4*exp(z)"; paramA = 0.45; paramB = 0.024; maxColour = 128; maxIter = 128; zoomFactor = 0.3; targetX = -10; targetY = 5;
        // functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; maxColour = 512; maxIter = 1024; zoomFactor = 1;
        // functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; maxColour = 512; maxIter = 1024; zoomFactor = 0.1; targetX = -20; targetY = 10;
        // functionType = "z^6*exp(z)"; paramA = 0.8; paramB = 0.31251; maxColour = 512; maxIter = 512;
        // functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044804; maxColour = 512; maxIter = 512;
        // functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044; maxColour = 512; maxIter = 512;

        // Initiera skärmen med att anropa grafikrutinen
        this.Size = new Size(scrWidth, scrHeight);
        this.WindowState = FormWindowState.Maximized;
        //this.Opacity = .75;
        //this.StartPosition = 0;
        setHeader();
        this.Paint += new PaintEventHandler(Paintgraph);
    }

    void Paintgraph(object sender, PaintEventArgs e)
    {
        bool keyboardInterrupt = false;
        bool modified = false;
        double coorX;
        double coorY;
        double iterations;
        int landColor;

        while (repaint)
        {
            // Rutans nedre vänstra hörn = (pixelX, pixelY)
            for (int pixWidth = 8; pixWidth >= minpixWidth; pixWidth = pixWidth / 2)
            { 
                for (int pixelX = 1; pixelX <= scrWidth; pixelX += pixWidth)
                { 
                    for (int pixelY = 1; pixelY <= scrHeight; pixelY += pixWidth)
                    {
                        // Beräkna koordinater för en pixel
                        coorX = Convert.ToDouble( pixelX - scrWidth / 2)  / scrHeight * 2.8 / zoomFactor;
                        coorY = Convert.ToDouble(scrHeight / 2 - pixelY) / scrHeight * 2.8 / zoomFactor;

                        // Beräkna iterationer för en pixel
                        iterations = FrakIt(coorX + targetX, coorY + targetY, paramA, paramB);

                        // Välj färgnummer mellan 0 och 2048
                        landColor = Convert.ToInt16((iterations %  MaxColour) / MaxColour * 2048);
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

                    // Check keyboard interrupts after drawing each column

                    if (keyboardInterrupt) break;
                    // arrow --> pan 1/zoomfactor * 30% in chosen direction
                    // in/out --> 30% larger/smaller
                    // iterations --> 128, 256, 512, <1024>, 2048, 4096
                    // colourdepth --> 128, 256, 512, <1024>, 2048, 4096

                }

                uint mouseX = (uint)System.Windows.Forms.Cursor.Position.X;
                uint mouseY = (uint)System.Windows.Forms.Cursor.Position.Y;

                targetX += Convert.ToDouble(mouseX - scrWidth / 2) / scrHeight * 2.8 / zoomFactor;
                targetY += Convert.ToDouble(scrHeight / 2 - mouseY) / scrHeight * 2.8 / zoomFactor;

                this.MouseClick += mouseClick;

                //if (Mouse.LeftButton == MouseButtonState.Pressed) { zoomFactor *= 1.5; modified = true; }
                zoomFactor *= 2;

                this.Text = "[zooming in on position: " + targetX + ":" + targetY + "] [factor: " + zoomFactor + "]";
                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                // if (leftclick) { zoomFactor *= 1.5; modified = true; }
                // if (rightclick) { zoomFactor /= 1.5; modified = true; }
                
                if (modified)
                {
                    setHeader();
                    break;
                }

                if (modified) break;
            }
            if (modified) break;
            //else repaint = false;
        }
    }

    public double FrakIt(double x, double y, double constA, double constB)
    {
        // https://en.wikipedia.org/wiki/Julia_set <-- Dessa har jag testat att återskapa
        // https://en.wikipedia.org/wiki/Newton_fractal <-- Dessa har jag inte testat...
        // https://www.wolframalpha.com/input/?i=exp(a%2Bbi)
        // https://www.wolframalpha.com/input/?i=exp((a%2Bbi)%5E2) <-- Förenklar formler till "alternate form"!

        int iteration = 0;
        double a = x;
        double b = y;
        double aprim = 0;
        double bprim = 0;
        double radius = 0;

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
            switch (functionType)
            {
                case "z^2":
                case "Mandelbrot":
                case "Julia":
                    { 
                        aprim = a * a - b * b + constA;
                        bprim = 2 * a * b + constB;
                        break;
                    }
                case "z^3":
                    {
                        aprim = a * a * a - 3 * a * b * b + constA;
                        bprim = 3 * a * a * b - b * b * b + constB;
                        break;
                    }
                case "z^4":
                    {
                        aprim = a * a * a * a - 6 * a * a * b * b + b * b * b * b + constA;
                        bprim = 4 * a * a * a * b - 4 * a * b * b * b + constB;
                        break;
                    }
                case "z^5":
                    {
                        aprim = a * a * a * a * a - 10 * a * a * a * b * b + 5 * a * b * b * b * b + constA;
                        bprim = 5 * a * a * a * a * b - 10 * a * a * b * b * b + b * b * b * b * b + constB;
                        break;
                    }
                case "z^6":
                    {
                        aprim = a * a * a * a * a * a - 15 * a * a * a * a * b * b + 15 * a * a * b * b * b * b - b * b * b * b * b * b + constA;
                        bprim = 6 * a * a * a * a * a * b - 20 * a * a * a * b * b * b + 6 * a * b * b * b * b * b + constB;
                        break;
                    }
                case "z^7":
                    {
                        aprim = a * a * a * a * a * a * a - 21 * a * a * a * a * a * b * b + 35 * a * a * a * b * b * b * b - 7 * a * b * b * b * b * b * b + constA;
                        bprim = 7 * a * a * a * a * a * a * b - 35 * a * a * a * a * b * b * b + 21 * a * a * b * b * b * b * b - b * b * b * b * b * b * b + constB;
                        break;
                    }
                case "z^8":
                    {
                        aprim =      a * a * a * a * a * a * a * a - 28 * a * a * a * a * a * a * b * b + 70 * a * a * a * a * b * b * b * b
                              - 28 * a * a * b * b * b * b * b * b +      b * b * b * b * b * b * b * b + constA;
                        bprim =  8 * a * a * a * a * a * a * a * b - 56 * a * a * a * a * a * b * b * b + 56 * a * a * a * b * b * b * b * b
                               - 8 * a * b * b * b * b * b * b * b  + constB;
                        break;
                    }
                case "z^9":
                    {
                        aprim =      a * a * a * a * a * a * a * a * a - 36 * a * a * a * a * a * a * a * b * b + 126 * a * a * a * a * a * b * b * b * b
                              - 84 * a * a * a * b * b * b * b * b * b +  9 * a * b * b * b * b * b * b * b * b + constA;
                        bprim =  9 * a * a * a * a * a * a * a * a * b - 84 * a * a * a * a * a * a * b * b * b + 126 * a * a * a * a * b * b * b * b * b 
                              - 36 * a * a * b * b * b * b * b * b * b +      b * b * b * b * b * b * b * b * b + constB;
                        break;
                    }
                case "exp(z)":
                    {
                        // exp(a+bi)+a+bi = (e^a cos(b) + a) + i (e^a sin(b) + b)
                        aprim = Math.Exp(a) * Math.Cos(b) + constA;
                        bprim = Math.Exp(a) * Math.Sin(b) + constB;
                        break;
                    }
                case "exp(z^2)":
                    {
                        // exp((a+bi)^2)+a+bi = (e ^ (a ^ 2 - b ^ 2) cos(2 a b) + a) + i(e ^ (a ^ 2 - b ^ 2) sin(2 a b) + b)
                        aprim = Math.Exp(a * a - b * b) * Math.Cos(2 * a * b) + constA;
                        bprim = Math.Exp(a * a - b * b) * Math.Sin(2 * a * b) + constB;
                        break;
                    }
                case "exp(z^3)":
                    {
                        // exp((a+bi)^3)+a+bi = (e^(a^3 - 3 a b^2) cos(3 a^2 b - b^3) + a) + i (e^(a^3 - 3 a b^2) sin(3 a^2 b - b^3) + b)
                        aprim = Math.Exp(a * a * a - 3 * a * b * b) * Math.Cos(3 * a * a * b - b * b * b) + constA;
                        bprim = Math.Exp(a * a * a - 3 * a * b * b) * Math.Sin(3 * a * a * b - b * b * b) + constB;
                        break;
                    }
                case "exp(z^4)":
                    {
                        // exp((a+bi)^4)+a+bi = (e^(a^4 - 6 a^2 b^2 + b^4) cos(4 a^3 b - 4 a b^3) + a) + i (e^(a^4 - 6 a^2 b^2 + b^4) sin(4 a^3 b - 4 a b^3) + b) 
                        aprim = Math.Exp(a * a * a * a - 6 * a * a * b * b + b * b * b * b) * Math.Cos(4 * a * a * a * b - 4 * a * b * b * b) + constA;
                        bprim = Math.Exp(a * a * a * a - 6 * a * a * b * b + b * b * b * b) * Math.Sin(4 * a * a * a * b - 4 * a * b * b * b) + constB;
                        break;
                    }
                case "exp(z^5)":
                    {
                        // exp((a+bi)^5)+a+bi = (e^(a^5 - 10 a^3 b^2 + 5 a b^4) cos(5 a^4 b - 10 a^2 b^3 + b^5) + a) + i (e^(a^5 - 10 a^3 b^2 + 5 a b^4) sin(5 a^4 b - 10 a^2 b^3 + b^5) + b) 
                        aprim = Math.Exp(a * a * a * a * a - 10 * a * a * a * b * b + 5 * a * b * b * b * b) * Math.Cos(5 * a * a * a * a * b - 10 * a * a * b * b * b + b * b * b * b * b) + constA;
                        bprim = Math.Exp(a * a * a * a * a - 10 * a * a * a * b * b + 5 * a * b * b * b * b) * Math.Sin(5 * a * a * a * a * b - 10 * a * a * b * b * b + b * b * b * b * b) + constB;
                        break;
                    }
                case "z*exp(z)":
                    {
                        // (a+bi)*exp(a+bi))+a+bi = (-e^a b sin(b) + e^a a cos(b) + a)
                        //                      + i ( e^a a sin(b) + e^a b cos(b) + b)
                        aprim = -Math.Exp(a) * b * Math.Sin(b) + Math.Exp(a) * a * Math.Cos(b) + constA;
                        bprim =  Math.Exp(a) * a * Math.Sin(b) + Math.Exp(a) * b * Math.Cos(b) + constB;
                        break;
                    }
                case "z^2*exp(z)":
                    {
                        // (a+bi)^2*exp(a+bi))+a+bi = (e^a a^2 cos(b) - e^a b^2 cos(b) - 2 e^a a b sin(b) + a)
                        //                        + i (e^a a^2 sin(b) - e^a b^2 sin(b) + 2 e^a a b cos(b) + b)
                        aprim = Math.Exp(a) * a * a * Math.Cos(b) - Math.Exp(a) * b * b * Math.Cos(b) - 2 * Math.Exp(a) * a * b * Math.Sin(b) + constA;
                        bprim = Math.Exp(a) * a * a * Math.Sin(b) - Math.Exp(a) * b * b * Math.Sin(b) + 2 * Math.Exp(a) * a * b * Math.Cos(b) + constB;
                        break;
                    }
                case "z^3*exp(z)":
                    {
                        // (a+bi)^3*exp(a+bi))+a+bi = (e^a a^3 cos(b) - 3 e^a a^2 b sin(b) + e^a b^3 sin(b) - 3 e^a a b^2 cos(b) + a)
                        //                        + i (e^a a^3 sin(b) + 3 e^a a^2 b cos(b) - e^a b^3 cos(b) - 3 e^a a b^2 sin(b) + b) 
                        aprim = Math.Exp(a) * a * a * a * Math.Cos(b) - 3 * Math.Exp(a) * a * a * b * Math.Sin(b) + Math.Exp(a) * b * b * b * Math.Sin(b) - 3 * Math.Exp(a) * a * b * b * Math.Cos(b) + constA;
                        bprim = Math.Exp(a) * a * a * a * Math.Sin(b) + 3 * Math.Exp(a) * a * a * b * Math.Cos(b) - Math.Exp(a) * b * b * b * Math.Cos(b) - 3 * Math.Exp(a) * a * b * b * Math.Sin(b) + constB;
                        break;
                    }
                case "z^4*exp(z)":
                    {
                        // (a+bi)^4*exp(a+bi))+a+bi = e^a a^4 cos(b) - 4 e^a a^3 b sin(b) - 6 e^a a^2 b^2 cos(b) + e^a b^4 cos(b) + 4 e^a a b^3 sin(b) + a
                        //                       + i (e^a a^4 sin(b) + 4 e^a a^3 b cos(b) - 6 e^a a^2 b^2 sin(b) + e^a b^4 sin(b) - 4 e^a a b^3 cos(b) + b)
                        aprim = Math.Exp(a) * a * a * a * a * Math.Cos(b) - 4 * Math.Exp(a) * a * a * a * b * Math.Sin(b) - 6 * Math.Exp(a) * a * a * b * b * Math.Cos(b) + Math.Exp(a) * b * b * b * b * Math.Cos(b) + 4 * Math.Exp(a) * a * b * b * b * Math.Sin(b) + constA;
                        bprim = Math.Exp(a) * a * a * a * a * Math.Sin(b) + 4 * Math.Exp(a) * a * a * a * b * Math.Cos(b) - 6 * Math.Exp(a) * a * a * b * b * Math.Sin(b) + Math.Exp(a) * b * b * b * b * Math.Sin(b) - 4 * Math.Exp(a) * a * b * b * b * Math.Cos(b) + constB;
                        break;
                    }
                case "z^5*exp(z)":
                    {
                        // (a+bi)^5*exp(a+bi))+a+bi = (e^a a^5 cos(b) - 5 e^a a^4 b sin(b) - 10 e^a a^3 b^2 cos(b) + 10 e^a a^2 b^3 sin(b) - e^a b^5 sin(b) + 5 e^a a b^4 cos(b) + a)
                        //                        + i (e^a a^5 sin(b) + 5 e^a a^4 b cos(b) - 10 e^a a^3 b^2 sin(b) - 10 e^a a^2 b^3 cos(b) + e^a b^5 cos(b) + 5 e^a a b^4 sin(b) + b)
                        aprim = Math.Exp(a) * a * a * a * a * a * Math.Cos(b) - 5 * Math.Exp(a) * a * a * a * a * b * Math.Sin(b) - 10 * Math.Exp(a) * a * a * a * b * b * Math.Cos(b) + 10 * Math.Exp(a) * a * a * b * b * b * Math.Sin(b) - Math.Exp(a) * b * b * b * b * b * Math.Sin(b) + 5 * Math.Exp(a) * a * b * b * b * b * Math.Cos(b) + constA;
                        bprim = Math.Exp(a) * a * a * a * a * a * Math.Sin(b) + 5 * Math.Exp(a) * a * a * a * a * b * Math.Cos(b) - 10 * Math.Exp(a) * a * a * a * b * b * Math.Sin(b) - 10 * Math.Exp(a) * a * a * b * b * b * Math.Cos(b) + Math.Exp(a) * b * b * b * b * b * Math.Cos(b) + 5 * Math.Exp(a) * a * b * b * b * b * Math.Sin(b) + constB;
                        break;
                    }
                case "z^6*exp(z)":
                    {
                        // (a+bi)^6*exp(a+bi))+a+bi = (e^a a^6 cos(b) - 6 e^a a^5 b sin(b) - 15 e^a a^4 b^2 cos(b) + 20 e^a a^3 b^3 sin(b) + 15 e^a a^2 b^4 cos(b) - e^a b^6 cos(b) - 6 e^a a b^5 sin(b) + a)
                        //                        + i (e^a a^6 sin(b) + 6 e^a a^5 b cos(b) - 15 e^a a^4 b^2 sin(b) - 20 e^a a^3 b^3 cos(b) + 15 e^a a^2 b^4 sin(b) - e^a b^6 sin(b) + 6 e^a a b^5 cos(b) + b)
                        aprim = Math.Exp(a) * a * a * a * a * a * a * Math.Cos(b) - 6 * Math.Exp(a) * a * a * a * a * a * b * Math.Sin(b) - 15 * Math.Exp(a) * a * a * a * a * b * b * Math.Cos(b) + 20 * Math.Exp(a) * a * a * a * b * b * b * Math.Sin(b) + 15 * Math.Exp(a) * a * a * b * b * b * b * Math.Cos(b) - Math.Exp(a) * b * b * b * b * b * b * Math.Cos(b) - 6 * Math.Exp(a) * a * b * b * b * b * b * Math.Sin(b) + constA;
                        bprim = Math.Exp(a) * a * a * a * a * a * a * Math.Sin(b) + 6 * Math.Exp(a) * a * a * a * a * a * b * Math.Cos(b) - 15 * Math.Exp(a) * a * a * a * a * b * b * Math.Sin(b) - 20 * Math.Exp(a) * a * a * a * b * b * b * Math.Cos(b) + 15 * Math.Exp(a) * a * a * b * b * b * b * Math.Sin(b) - Math.Exp(a) * b * b * b * b * b * b * Math.Sin(b) + 6 * Math.Exp(a) * a * b * b * b * b * b * Math.Cos(b) + constB;
                        break;
                    }
                case "z^7*exp(z)":
                    {
                        // (a+bi)^7*exp(a+bi))+a+bi = (e^a a^7 cos(b) - 7 e^a a^6 b sin(b) - 21 e^a a^5 b^2 cos(b) + 35 e^a a^4 b^3 sin(b) + 35 e^a a^3 b^4 cos(b) - 21 e^a a^2 b^5 sin(b) + e^a b^7 sin(b) - 7 e^a a b^6 cos(b) + a)
                        //                        + i (e^a a^7 sin(b) + 7 e^a a^6 b cos(b) - 21 e^a a^5 b^2 sin(b) - 35 e^a a^4 b^3 cos(b) + 35 e^a a^3 b^4 sin(b) + 21 e^a a^2 b^5 cos(b) - e^a b^7 cos(b) - 7 e^a a b^6 sin(b) + b)
                        aprim = Math.Exp(a) * a * a * a * a * a * a * a * Math.Cos(b) - 7 * Math.Exp(a) * a * a * a * a * a * a * b * Math.Sin(b) - 21 * Math.Exp(a) * a * a * a * a * a * b * b * Math.Cos(b) + 35 * Math.Exp(a) * a * a * a * a * b * b * b * Math.Sin(b) + 35 * Math.Exp(a) * a * a * a * b * b * b * b * Math.Cos(b) - 21 * Math.Exp(a) * a * a * b * b * b * b * b * Math.Sin(b) + Math.Exp(a) * b * b * b * b * b * b * b * Math.Sin(b) - 7 * Math.Exp(a) * a * b * b * b * b * b * b * Math.Cos(b) + constA;
                        bprim = Math.Exp(a) * a * a * a * a * a * a * a * Math.Sin(b) + 7 * Math.Exp(a) * a * a * a * a * a * a * b * Math.Cos(b) - 21 * Math.Exp(a) * a * a * a * a * a * b * b * Math.Sin(b) - 35 * Math.Exp(a) * a * a * a * a * b * b * b * Math.Cos(b) + 35 * Math.Exp(a) * a * a * a * b * b * b * b * Math.Sin(b) + 21 * Math.Exp(a) * a * a * b * b * b * b * b * Math.Cos(b) - Math.Exp(a) * b * b * b * b * b * b * b * Math.Cos(b) - 7 * Math.Exp(a) * a * b * b * b * b * b * b * Math.Sin(b) + constB;
                        break;
                    }
            }
            a = aprim;
            b = bprim;
            radius = Math.Sqrt(a * a + b * b);
        } while (iteration < maxIter && radius < 2);
        return --iteration;
    }

    public static void Main()
    {
        Application.Run(new DrawPicture());
    }
    // End of class
}