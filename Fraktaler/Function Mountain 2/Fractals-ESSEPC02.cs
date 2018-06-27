using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Numerics;

namespace FractalExplorer
{
    public class Fractal : Form
    {
        private static int scrWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int scrHeight = Screen.PrimaryScreen.Bounds.Height;

        private double paramA = 0;
        private double paramB = 0;
        private double targetX = 0;
        private double targetY = 0;
        private double indicateX = 0;
        private double indicateY = 0;
        private double zoomFactor = 1;
        private int startPixWidth = 8;
        private int minPixWidth = 1;
        private int maxIter = 1024;
        private int maxColour = 1024;
        private bool repaint = true;
        private int formulaNr;
        private string functionType;
        private string graphName;

        public Fractal()
        {
            // This constructur selects the formula that you want to view.

            new Thread(() =>
            {
                this.Paint += new PaintEventHandler(PaintGraph);
            }).Start();

        }

        public bool SelectFormula(int i)
        {
            bool selection = true;
            switch (i)
            {
                case 0:
                    functionType = "Mandelbrot"; maxColour = 512; maxIter = 2048; targetX = -0.75; break;
                case 1:
                    functionType = "PHZ"; paramA = 0; paramB = 0; maxColour = 256; maxIter = 1024; zoomFactor = 0.75;  targetX = 0; break;
                case 2:
                    functionType = "PHZ"; paramA = -0.735; paramB = 0.35; maxColour = 128; maxIter = 1024; zoomFactor = 1; targetX = 0; break;
                case 4:
                    functionType = "Julia"; paramA = -0.5; paramB = 0.6; maxColour = 256; maxIter = 1024; zoomFactor = 1; targetX = 0; break;
                case 6:
                    functionType = "Julia"; paramA = -0.835; paramB = -0.2321; maxColour = 256; maxIter = 1024; targetX = 0; zoomFactor = 2; break;
                case 8:
                    functionType = "Julia"; paramA = -0.75; paramB = 0.11; maxColour = 256; maxIter = 1024; zoomFactor = 1.25; break;

                case 10:
                    functionType = "z^2"; paramA = -0.8; paramB = 0.156; maxColour = 512; maxIter = 512; zoomFactor = 2; break;
                case 12:
                    functionType = "z^2"; paramA = -0.5; paramB = 0.59999; maxColour = 256; maxIter = 512; zoomFactor = 1; break;
                case 14:
                    functionType = "z^2"; paramA = -0.1; paramB = 0.6517; maxColour = 1024; maxIter = 1024; break;
                case 16:
                    functionType = "z^2"; paramA = -0.456; paramB = 0.5678; maxColour = 512; maxIter = 1024; zoomFactor = 1.2; break;
                case 18:
                    functionType = "z^3"; paramA = 0.4; paramB = 0; maxColour = 128; maxIter = 128; zoomFactor = 1.1; break;
                case 20:
                    functionType = "z^3"; paramA = -0.475; paramB = 0.567; maxColour = 1024; maxIter = 1024; targetY = 0.05; break;
                case 22:
                    functionType = "z^4"; paramA = 0.484; paramB = 0; maxColour = 1024; maxIter = 1024; targetY = 0; break;
                case 24:
                    functionType = "z^4"; paramA = 0.620855; paramB = 0.69; maxIter = 1024; maxColour = 256; break;
                case 26:
                    functionType = "z^5"; paramA = 0.544; paramB = 0; maxColour = 256; break;
                case 28:
                    functionType = "z^5"; paramA = 0.463; paramB = 0.66; maxColour = 256; maxIter = 512; targetY = -0.05; break;
                case 30:
                    functionType = "z^5"; paramA = 0.55; paramB = 0.762; maxColour = 256; targetY = -0.05; break;
                case 32:
                    functionType = "z^6"; paramA = 0.5899; paramB = 0.0007; maxColour = 512; targetY = 0; zoomFactor = 1; break;
                case 34:
                    functionType = "z^7"; paramA = 0.6267; paramB = 0.001; maxColour = 512; zoomFactor = 1.1; break;
                case 36:
                    functionType = "z^8"; paramA = 0.679555; paramB = 0.00735; maxColour = 256; maxIter = 1024; zoomFactor = 1.1; break;
                case 38:
                    functionType = "z^9"; paramA = 0.684999; paramB = 0.001; maxColour = 256; maxIter = 1024; zoomFactor = 1.1; break;
                case 40:
                    functionType = "z^9"; paramA = 0.7; paramB = 0.0068; maxColour = 256; maxIter = 1024; zoomFactor = 1.1; break;
                case 42:
                    functionType = "z^9"; paramA = 0.701; paramB = 0.0068; maxColour = 256; maxIter = 1024; zoomFactor = 2; break;
                case 44:
                    functionType = "z^9"; paramA = 0.7015022; paramB = 0.0068; maxColour = 256; maxIter = 1024; zoomFactor = 1; break;

                case 100:
                    functionType = "exp(z)"; paramA = -0.988; paramB = 0.003; maxColour = 256; maxIter = 256; targetY = -0.2; zoomFactor = 5; break;
                case 102:
                    // Mitt Z!
                    functionType = "exp(z^2)"; paramA = -0.5; paramB = -0.17; maxColour = 256; maxIter = 256; targetY = 0; zoomFactor = 0.3;
                    break;
                case 103:
                    // Nytt Z!
                    functionType = "exp(z^2)"; paramA = -0.54; paramB = -0.16; maxColour = 256; maxIter = 256; targetY = 0; zoomFactor = 0.5;
                    break;
                case 104:
                    // Nytt Z!
                    functionType = "exp(z^2)"; paramA = -0.55; paramB = -0.175; maxColour = 256; maxIter = 256; targetY = 0; zoomFactor = 0.5;
                    break;
                case 105:
                    functionType = "exp(z^3)"; paramA = -0.620921; paramB = 0.00127; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 106:
                    functionType = "exp(z^3)"; paramA = -0.615; paramB = 0.0022; maxColour = 256; maxIter = 265; zoomFactor = 1; break;
                case 108:
                    functionType = "exp(z^4)"; paramA = -0.53; paramB = 0.00057; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 110:
                    functionType = "exp(z^5)"; paramA = -0.54; paramB = 0.77; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break;
                case 112:
                    functionType = "exp(z^6)"; paramA = -0.59; paramB = 0.73; maxColour = 256; maxIter = 1024; zoomFactor = 0.75; break; //#####
                case 114:
                    functionType = "exp(z^7)"; paramA = -0.69; paramB = 0.79; maxColour = 256; maxIter = 1024; zoomFactor = 0.5; break; //#####
                case 116:
                    functionType = "exp(z^8)"; paramA = -0.75; paramB = 0.83; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break; //#####
                case 118:
                    functionType = "exp(z^9)"; paramA = -0.83002; paramB = 0.87; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break; //#####

                case 200:
                    functionType = "z*exp(z)"; paramA = 0.01; paramB = 0.00015; maxColour = 512; maxIter = 1024; zoomFactor = 0.1; break;
                case 202:
                    functionType = "z^2*exp(z)"; paramA = 0.21; paramB = 0.009; maxColour = 512; maxIter = 1024; zoomFactor = 1; break;
                case 204:
                    functionType = "z^3*exp(z)"; paramA = 0.45; paramB = 0.2; maxColour = 512; maxIter = 512; targetX = -1.3; break;
                case 206:
                    functionType = "z^4*exp(z)"; paramA = 0.45; paramB = 0.024; maxColour = 128; maxIter = 128; zoomFactor = 0.3; targetX = -12; targetY = 5; break;
                case 208:
                    functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; maxColour = 512; maxIter = 1024; zoomFactor = 1; targetX = 0; targetY = 0; break;
                case 210:
                    functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; maxColour = 512; maxIter = 1024; zoomFactor = 0.1; targetX = -15; targetY = 10; break;
                case 212:
                    functionType = "z^5*exp(z)"; paramA = 0.5; paramB = 0.00899; maxIter = 256; maxColour = 256; zoomFactor = 1; targetX = 0; targetY = 0; break;
                case 214:
                    functionType = "z^6*exp(z)"; paramA = 0.8; paramB = 0.31251; maxColour = 512; maxIter = 512; break;
                case 216:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044804; maxColour = 512; maxIter = 512; break;
                case 218:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044; maxColour = 512; maxIter = 512; break;

                case 300: // Featherino
                    functionType = "z - (z^4-1)/(4z^3)"; paramA = 0.099; paramB = 0.099; maxColour = 64; maxIter = 128; targetX = 0; targetY = 0; zoomFactor = 1; break;
                case 302: // Peacock
                    functionType = "z - (z^2-1)/(z^2+1)"; paramA = 0.3250; paramB = 0.72; maxColour = 128; maxIter = 256; targetX = 0.5; targetY = -0.65; zoomFactor = 0.5; break;
                case 304:
                    functionType = "z - z^3 - 4*z^4"; paramA = 0.2425; paramB = 0.0235; maxColour = 512; maxIter = 512; targetX = 0; targetY = 0; zoomFactor = 2.5; break;
                case 306: // // Newton's method for finding roots
                    functionType = "z^3 + (c-1)*z"; paramA = 0.875; paramB = 0.0125; maxColour = 512; maxIter = 512; targetX = 0; targetY = 0; zoomFactor = 1.2; break;
                case 350: // "Pattis Marble"
                    functionType = "z - (z^4-1)/(2z^4)"; paramA = 0.05; paramB = 0.05; maxColour = 101; maxIter = 300; targetX = 0.05; targetY = -0.05; zoomFactor = 0.55; break;
                case 352: // "Pattis Inversed Marble #1"
                    functionType = "z - (2z^4)/(z^4-1)"; paramA = 0.06; paramB = 0.15; maxColour = 101; maxIter = 300; targetX = 0; targetY = 0; zoomFactor = 2.5; break;
                case 354: // "Pattis Marble #2"
                    functionType = "z - (1-z^4)/(2z^4)"; paramA = 0.08; paramB = 0.15; maxColour = 101; maxIter = 300; targetX = 0.05; targetY = -0.05; zoomFactor = 0.55; break;
                case 356: // "Pattis Inversed Marble #2"
                    functionType = "z - (2z^4)/(1-z^4)"; paramA = 0.0478; paramB = 0.09915; maxColour = 101; maxIter = 300; targetX = 0; targetY = 0; zoomFactor = 2.5; break;

                case 400:
                    functionType = "z^2*sin(z)"; paramA = 0.55; paramB = 0.2; maxColour = 512; maxIter = 1024; targetX = 0; targetY = 0; zoomFactor = 2; break;
                case 402:
                    functionType = "z^2*cos(z)"; paramA = 0.48; paramB = 0.30; maxColour = 512; maxIter = 1024; zoomFactor = 1; break;
                case 404:
                    functionType = "z^2*tan(z)"; paramA = 0.55; paramB = 0.185; maxColour = 512; maxIter = 1024; break;
                case 406:
                    functionType = "z^2*sin(z)^2"; paramA = 0.64; paramB = 0.2; maxColour = 512; maxIter = 1024; break;
                case 408:
                    functionType = "z^2*cos(z)^2"; paramA = 0.6246; paramB = 0.48; maxColour = 512; maxIter = 1024; break;
                case 410:
                    functionType = "z^2*tan(z)^2"; paramA = 0.63; paramB = 0.485; maxColour = 256; maxIter = 256; break;
                case 412:
                    functionType = "z^2*sin(z)-cos(z)"; paramA = 0.2259; paramB = 0.124; maxColour = 256; maxIter = 256; zoomFactor = 1.1; break;
                case 414:
                    functionType = "z^3*sin(z)-2*cos(z)"; paramA = 1.09; paramB = 0.001; maxColour = 256; maxIter = 256; zoomFactor = 3; break;
                default:
                    selection = false; break;
            }
            return selection;
        }

        public double FractalMath(double x, double y, double constA, double constB)
        {
            // This is the method for the central mathematical calculations.

            // https://en.wikipedia.org/wiki/Julia_set <-- Dessa har jag testat att återskapa
            // https://en.wikipedia.org/wiki/Newton_fractal <-- Dessa har jag inte testat...
            // https://www.wolframalpha.com/input/?i=exp(a%2Bbi)
            // https://www.wolframalpha.com/input/?i=exp((a%2Bbi)%5E2) <-- Förenklar formler till "alternate form"!
            // http://fractalfoundation.org/resources/what-are-fractals/ <-- Some other and more complex examples...
            // http://www.fractalsciencekit.com/program/maneqn.htm <-- Other formulas and code examples

            int iteration = 0;
            double radius = 0;
            Complex z = new Complex(x, y);
            Complex constC = new Complex(constA, constB);
            Complex constZ = new Complex(x, y);

            if (functionType == "Mandelbrot")
            {
                // För den enkla funktionen "Mandelbrot" är alltid konstanterna = koordinaterna
                // z = Complex.Pow(z, 2) + constZ;
            }
            else if (functionType == "PHZ")
            {
                // konstanterna = funktion av koordinaterna + parametrarna
                constZ = new Complex(Math.Cos(x) + constA, Math.Sin(constB) + constB);
                // constZ = new Complex(Math.Sin(x) + constA, Math.Cos(constB) + constB);
                // constZ = new Complex(Math.Sin(x / y) + constA, Math.Cos(x / y) + constB);
            }

            do
            {
                iteration++;
                switch (functionType)
                {
                    case "Mandelbrot":
                        {
                            z = Complex.Pow(z, 2) + constZ;
                            break;
                        }
                    case "PHZ":
                        {
                            z = Complex.Pow(z, 2) + constZ;
                            break;
                        }
                    case "z^2":
                    case "Julia":
                        {
                            z = Complex.Pow(z, 2) + constC;
                            break;
                        }
                    case "z^3":
                        {
                            z = Complex.Pow(z, 3) + constC;
                            break;
                        }
                    case "z^4":
                        {
                            z = Complex.Pow(z, 4) + constC;
                            break;
                        }
                    case "z^5":
                        {
                            z = Complex.Pow(z, 5) + constC;
                            break;
                        }
                    case "z^6":
                        {
                            z = Complex.Pow(z, 6) + constC;
                            break;
                        }
                    case "z^7":
                        {
                            z = Complex.Pow(z, 7) + constC;
                            break;
                        }
                    case "z^8":
                        {
                            z = Complex.Pow(z, 8) + constC;
                            break;
                        }
                    case "z^9":
                        {
                            z = Complex.Pow(z, 9) + constC;
                            break;
                        }
                    case "exp(z)":
                        {
                            z = Complex.Exp(z) + constC;
                            break;
                        }
                    case "exp(z^2)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 2)) + constC;
                            break;
                        }
                    case "exp(z^3)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 3)) + constC;
                            break;
                        }
                    case "exp(z^4)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 4)) + constC;
                            break;
                        }
                    case "exp(z^5)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 5)) + constC;
                            break;
                        }
                    case "exp(z^6)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 6)) + constC;
                            break;
                        }
                    case "exp(z^7)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 7)) + constC;
                            break;
                        }
                    case "exp(z^8)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 8)) + constC;
                            break;
                        }
                    case "exp(z^9)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 9)) + constC;
                            break;
                        }
                    case "z*exp(z)":
                        {
                            z = Complex.Multiply(z, Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^2*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^3*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 3), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^4*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 4), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^5*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 5), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^6*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 6), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z^7*exp(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 7), Complex.Exp(z)) + constC;
                            break;
                        }
                    case "z - (z^4-1)/(2z^4)":
                        {
                            // "Pattis Marble"
                            z = z - (Complex.Pow(z, 4) - 1) / (Complex.Pow(Complex.Multiply(z, 2), 4)) + constC;
                            break;
                        }
                    case "z - (2z^4)/(z^4-1)":
                        {
                            // "Pattis Inversed Marble"
                            z = z - (Complex.Pow(Complex.Multiply(z, 2), 4)) / (Complex.Pow(z, 4) - 1) + constC;
                            break;
                        }
                    case "z - (1-z^4)/(2z^4)":
                        {
                            // "Pattis Marble #2"
                            z = z - (1 - Complex.Pow(z, 4)) / (Complex.Pow(Complex.Multiply(z, 2), 4)) + constC;
                            break;
                        }
                    case "z - (2z^4)/(1-z^4)":
                        {
                            // "Pattis Inversed Marble #2"
                            z = z - (Complex.Pow(Complex.Multiply(z, 2), 4)) / (1 - Complex.Pow(z, 4)) + constC;
                            break;
                        }
                    case "z - (z^4-1)/(4z^3)":
                        {
                            // Featherino
                            z = z - (Complex.Pow(z, 4) - 1) / (Complex.Pow(Complex.Multiply(z, 4), 3)) + constC;
                            break;
                        }
                    case "z - (z^2-1)/(z^2+1)":
                        {
                            // Peacock
                            z = z - Complex.Divide((Complex.Pow(z, 2) - 1), (Complex.Pow(z, 2) + 1)) + constC;
                            break;
                        }
                    case "z - z^3 - 4*z^4":
                        {
                            z = z - Complex.Pow(z, 3) - 4 * Complex.Pow(z, 4) + constC;
                            break;
                        }
                    case "z^3 + (c-1)*z":
                        {
                            // Newton's method for finding roots
                            z = z - Complex.Pow(z, 3) + Complex.Multiply(z, constC - 1) + constC;
                            break;
                        }
                    case "z^2*sin(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Sin(z)) + constC;
                            break;
                        }
                    case "z^2*cos(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Cos(z)) + constC;
                            break;
                        }
                    case "z^2*tan(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Tan(z)) + constC;
                            break;
                        }
                    case "z^2*sin(z)^2":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Pow(Complex.Sin(z), new Complex(2, 0))) + constC;
                            break;
                        }
                    case "z^2*cos(z)^2":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Pow(Complex.Cos(z), new Complex(2, 0))) + constC;
                            break;
                        }
                    case "z^2*tan(z)^2":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Pow(Complex.Tan(z), new Complex(2, 0))) + constC;
                            break;
                        }
                    case "z^2*sin(z)-cos(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 2), Complex.Sin(z)) - Complex.Cos(z) + constC;
                            break;
                        }
                    case "z^3*sin(z)-2*cos(z)":
                        {
                            z = Complex.Multiply(Complex.Pow(z, 3), Complex.Sin(z)) - Complex.Multiply(new Complex(2, 0), Complex.Cos(z)) + constC;
                            break;
                        }
                }

                x = z.Real;
                y = z.Imaginary;
                radius = Math.Sqrt(x * x + y * y);

            } while (iteration < maxIter && radius < 2);
            return --iteration;
        }

        public double Pix2CoordX(int x)
        {
            // Convert screen coordinate X to mathematical coordinate X
            return targetX + Convert.ToDouble(x - scrWidth / 2) / scrHeight * 2.5 / zoomFactor;
        }

        public double Pix2CoordY(int y)
        {
            // Convert screen coordinate Y to mathematical coordinate Y
            return targetY + Convert.ToDouble(scrHeight / 2 - y) / scrHeight * 2.5 / zoomFactor;
        }

        private void PaintGraph(object sender, PaintEventArgs e)
        {
            bool keyboardInterrupt = false;
            bool modified = false;
            double iterations;
            int spectrum;
            long maxTime = 60000; // 1 minute = 60.000 mikroseconds

            for (int i = 404; i < 1000; i++)
            {
                formulaNr = i;
                startPixWidth = 8;
                bool choice = SelectFormula(i);
                if (!choice) continue;
                repaint = true;
                Stopwatch stopwatch = Stopwatch.StartNew();
                while (repaint)
                {
                    modified = false;

                    // Rutans nedre vänstra hörn = (pixelX, pixelY)
                    for (int pixWidth = startPixWidth; pixWidth >= minPixWidth; pixWidth = pixWidth / 2)
                    {
                        for (int pixelX = 1; pixelX <= scrWidth; pixelX += pixWidth)
                        {
                            for (int pixelY = 1; pixelY <= scrHeight; pixelY += pixWidth)
                            {
                                // Convert pixel position to mathematical coordinates
                                double coorX = Pix2CoordX(pixelX);
                                double coorY = Pix2CoordY(pixelY);

                                // Calculate iterations for a pixel, and choose a colour number between 0 and 2048 
                                iterations = FractalMath(coorX, coorY, paramA, paramB);
                                spectrum = Convert.ToInt16((iterations % maxColour) / maxColour * 2048);
                                if (spectrum < 0) spectrum = 0;

                                // Interpred the colour number to a combination of primal colours
                                int colorTrans = 0;
                                int colorRed = 0;
                                int colorGreen = 0;
                                int colorBlue = 0;
                                /* Black - Red */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = spectrum; colorGreen = 0; colorBlue = 0; }
                                else spectrum = spectrum - 256;
                                /* Red - Yelow */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 255; colorGreen = spectrum; colorBlue = 0; }
                                else spectrum = spectrum - 256;
                                /* Yellow - Green */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 255 - spectrum; colorGreen = 255; colorBlue = 0; }
                                else spectrum = spectrum - 256;
                                /* Green - Teal */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 0; colorGreen = 255; colorBlue = spectrum; }
                                else spectrum = spectrum - 256;
                                /* Teal - Blue */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 0; colorGreen = 255 - spectrum; colorBlue = 255; }
                                else spectrum = spectrum - 256;
                                /* Blue - Purple */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = spectrum; colorGreen = 0; colorBlue = 255; }
                                else spectrum = spectrum - 256;
                                /* Purple - White */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 255; colorGreen = spectrum; colorBlue = 255; }
                                else spectrum = spectrum - 256;
                                /* White - Black */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255; colorRed = 255 - spectrum; colorGreen = 255 - spectrum; colorBlue = 255 - spectrum; }
                                else spectrum = spectrum - 256;
                                /* overflow --> Black */
                                if (colorTrans == 0 && spectrum >= 0 && spectrum < 256) { colorTrans = 255 - spectrum; colorRed = 0; colorGreen = 0; colorBlue = 0; }

                                // Select a brush with the chosen colour
                                Graphics g = e.Graphics;
                                SolidBrush paintBrush = new SolidBrush(Color.FromArgb(255, // Specifies the transparency of the color.
                                                                       colorRed,   // Specifies the amount of red.
                                                                       colorGreen, // specifies the amount of green.
                                                                       colorBlue)); // Specifies the amount of blue.
                                if (pixWidth > 0)
                                {
                                    e.Graphics.FillPolygon(paintBrush, new Point[]{
                                                                       new Point(pixelX, pixelY),
                                                                       new Point(pixelX + pixWidth, pixelY),
                                                                       new Point(pixelX + pixWidth, pixelY + pixWidth),
                                                                       new Point(pixelX, pixelY + pixWidth)});
                                }
                                else
                                {
                                    Pen pen = new Pen(paintBrush);
                                    e.Graphics.DrawLine(pen, new Point(pixelX, pixelY), new Point(pixelX + 1, pixelY));
                                }
                            }// for (pixelY)

                            // Check keyboard interrupts after drawing each column
                            if (keyboardInterrupt) break;

                            // Read the position of the mouse cursor as the new target
                            int mouseX = (int)System.Windows.Forms.Cursor.Position.X - 1;  // -1 for the frame of the window
                            int mouseY = (int)System.Windows.Forms.Cursor.Position.Y - 24; // -23 for the header of the window

                            // Convert the mousposition to target coordinates
                            indicateX = Pix2CoordX(mouseX);
                            indicateY = Pix2CoordY(mouseY);

                            // Check for mouseclicks
                            //switch (mouseClick)
                            //{
                            //    case "Left":
                            //        zoomFactor *= 2;
                            //        targetX = indicateX;
                            //        targetY = indicateY;
                            //        modified = true;
                            //        break;
                            //    case "Right":
                            //        zoomFactor /= 2;
                            //        targetX = indicateX;
                            //        targetY = indicateY;
                            //        modified = true;
                            //        break;
                            //    default:
                            //        break;
                            //}

                            // Update the window header
                            SetHeader(stopwatch.ElapsedMilliseconds / 1000);

                            if (modified) break;
                        }// for (pixelX)

                        // Don't show the same picture for too long!
                        if (stopwatch.ElapsedMilliseconds > maxTime) { modified = true; repaint = false; }

                        if (modified) break;
                    } // for (pixwidth)
                    if (modified) break;
                    //else repaint = false;

                    if (paramA == 0 && paramB == 0)
                    {
                        paramA = 0.5;
                        paramB = 0.5;
                        startPixWidth = 1;
                    }
                    else
                    {
                        paramA = paramA * 1.01;
                        paramB = paramB * 1.01;
                        startPixWidth = 1;
                    }
                } // while (repaint)
                // re-assign the maximum time a picture is painted
                maxTime = (long)(stopwatch.ElapsedMilliseconds * 2);
                if (maxTime < 30000) maxTime = 30000;
                if (maxTime > 300000) maxTime = 300000;

                Melody();
            } // for (int i...
        }

        private void Melody()
        {
            Console.Beep(261, 250);
            Console.Beep(392, 250);

            Console.Beep(277, 250);
            Console.Beep(415, 250);

            Console.Beep(294, 250);
            Console.Beep(440, 250);

            Console.Beep(311, 250);
            Console.Beep(466, 250);

            Console.Beep(330, 250);
            Console.Beep(494, 250);

            Console.Beep(349, 250);
            Console.Beep(523, 250);

            Console.Beep(370, 1500);

            Thread.Sleep(10000);
        }
        private void SetHeader(long timer)
        {
            graphName = "Formula " + formulaNr + ": ";

            if (functionType == "Mandelbrot") graphName += "Mandelbrot z'= z^2 + c";
            else if (functionType == "Julia") graphName += "Julia Set z' = z^2 " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";
            else if (functionType == "PHZ") graphName += "PHZ Set z' = z^2 + f(x) + f(y)i + " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";
            else graphName += "z' = " + functionType + " + " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i"; ;

            graphName += "     [mid x: " + targetX.ToString() + "]"
                      + " [mid y: " + targetY.ToString() + "]"
                      + " [maxiter: " + maxIter + "]"
                      + " [colours: " + maxColour + "]"
                      + " [zoom: " + zoomFactor.ToString() + "]"
                      + " [mouse x: " + indicateX.ToString() + "]"
                      + " [mouse y: " + indicateY.ToString() + "]"
                      + " [timer: " + timer.ToString() + "]";
            this.Text = graphName;
        }

        private void InitializeFrame()
        {
            this.SuspendLayout();
            // 
            // Fractal
            // 
            this.ClientSize = new System.Drawing.Size(scrWidth, scrHeight);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Fractal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.ResumeLayout(false);
        }

        public static void Main()
        {
            Fractal fractal = new Fractal();
            fractal.InitializeFrame();
            Application.Run(fractal);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Fractal
            // 
            this.ClientSize = new System.Drawing.Size(scrWidth, scrHeight) ;
            this.Name = "Fractal";
            this.Load += new System.EventHandler(this.Fractal_Load);
            this.ResumeLayout(false);
        }

        private void Fractal_Load(object sender, EventArgs e)
        {

        }
    } // End of class
}