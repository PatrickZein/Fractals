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
        private long ticker = 0;
        private long totalTicker = 0;
        private long maxUsedColour = 0;

        Random r = new Random();

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

            // Assign default parameters
            paramA = 0;
            paramB = 0;
            targetX = 0;
            targetY = 0;
            zoomFactor = 1;
            maxIter = 512;
            maxColour = 512;

            switch (i)
            {
                case 1:
                    functionType = "Mandelbrot"; targetX = -0.75; break;
                case 3:
                    functionType = "PHZ"; maxColour = 256; zoomFactor = 0.75;  break;
                case 5:
                    functionType = "PHZ"; paramA = -0.735; paramB = 0.35; maxColour = 128; break;
                case 7:
                    functionType = "Julia"; paramA = -0.5; paramB = 0.6; maxColour = 256; break;
                case 9:
                    functionType = "Julia"; paramA = -0.835; paramB = -0.2321; maxColour = 256; zoomFactor = 2; break;
                case 11:
                    functionType = "Julia"; paramA = -0.75; paramB = 0.11; maxColour = 256; zoomFactor = 1.25; break;

                case 13:
                    // "Kinesisk drake"
                    functionType = "z^2"; paramA = -0.8; paramB = 0.156; maxColour = 256; maxIter = 256; zoomFactor = 1.3; targetY = -0.025; break;
                case 15:
                    functionType = "z^2"; paramA = -0.5; paramB = 0.59999; maxColour = 256; maxIter = 512; break;
                case 17:
                    functionType = "z^2"; paramA = -0.1; paramB = 0.6517; maxColour = 1024; break;
                case 19:
                    functionType = "z^2"; paramA = -0.456; paramB = 0.5678; zoomFactor = 1.2; break;
                case 21:
                    functionType = "z^3"; paramA = 0.4; paramB = 0; maxColour = 128; maxIter = 128; zoomFactor = 1.1; break;
                case 23:
                    functionType = "z^3"; paramA = -0.475; paramB = 0.567; maxColour = 1024; targetY = 0.05; break;
                case 25:
                    functionType = "z^4"; paramA = 0.484; paramB = 0; maxColour = 1024; break;
                case 27:
                    functionType = "z^4"; paramA = 0.620855; paramB = 0.69; maxColour = 256; break;
                case 29:
                    functionType = "z^5"; paramA = 0.544; paramB = 0; maxColour = 256; break;
                case 31:
                    functionType = "z^5"; paramA = 0.463; paramB = 0.66; maxColour = 256; maxIter = 512; targetY = -0.05; break;
                case 33:
                    functionType = "z^5"; paramA = 0.55; paramB = 0.762; maxColour = 256; targetY = -0.05; break;
                case 35:
                    functionType = "z^6"; paramA = 0.5899; paramB = 0.0007; break;
                case 37:
                    functionType = "z^7"; paramA = 0.6267; paramB = 0.001; zoomFactor = 1.1; break;
                case 39:
                    functionType = "z^8"; paramA = 0.679555; paramB = 0.00735; maxColour = 256; zoomFactor = 1.1; break;
                case 41:
                    functionType = "z^9"; paramA = 0.684999; paramB = 0.001; maxColour = 256; zoomFactor = 1.1; break;
                case 43:
                    functionType = "z^9"; paramA = 0.7; paramB = 0.0068; maxColour = 256; zoomFactor = 1.1; break;
                case 45:
                    functionType = "z^9"; paramA = 0.701; paramB = 0.0068; maxColour = 256; zoomFactor = 2; break;
                case 47:
                    functionType = "z^9"; paramA = 0.7015022; paramB = 0.0068; maxColour = 256; break;
                case 49:
                    functionType = "z^10"; paramA = 0.71955; paramB = 0.0068; maxColour = 256; break;
                case 99:
                    functionType = "z^pi"; paramA = 0.4001; paramB = 0.000; maxColour = 256; break;

                case 100:
                    functionType = "exp(z)"; paramA = -0.988; paramB = 0.003; maxColour = 256; maxIter = 256; targetY = -0.2; zoomFactor = 5; break;
                case 102:
                    // Mitt Z!
                    functionType = "exp(z^2)"; paramA = -0.5; paramB = -0.17; maxColour = 256; maxIter = 256; zoomFactor = 0.3;
                    break;
                case 103:
                    // Nytt Z!
                    functionType = "exp(z^2)"; paramA = -0.54; paramB = -0.16; maxColour = 256; maxIter = 256; zoomFactor = 0.535; targetY = -0.06;
                    break;
                case 104:
                    // Nytt Z!
                    functionType = "exp(z^2)"; paramA = -0.55; paramB = -0.175; maxColour = 256; maxIter = 256; zoomFactor = 0.6; targetY = -0.06;
                    break;
                case 105:
                    functionType = "exp(z^3)"; paramA = -0.620921; paramB = 0.00127; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 106:
                    functionType = "exp(z^3)"; paramA = -0.615; paramB = 0.0022; maxColour = 256; maxIter = 265; break;
                case 107:
                    functionType = "exp(z^3)"; paramA = -0.59; paramB = 0.004; maxColour = 256; maxIter = 265; break;
                case 108:
                    functionType = "tester"; paramA = 0.3659; paramB = 0.001; targetX = -0.2; zoomFactor = 10; maxColour = 256; maxIter = 256; break;
                case 109:
                    functionType = "exp(z^4)"; paramA = -0.53; paramB = 0.00057; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 110:
                    functionType = "exp(z^5)"; paramA = -0.54; paramB = 0.77; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break;
                case 112:
                    functionType = "exp(z^6)"; paramA = -0.59; paramB = 0.73; maxColour = 256; zoomFactor = 0.75; break; //#####
                case 114:
                    functionType = "exp(z^7)"; paramA = -0.69; paramB = 0.79; maxColour = 256; zoomFactor = 0.5; break; //#####
                case 116:
                    functionType = "exp(z^8)"; paramA = -0.75; paramB = 0.83; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break; //#####
                case 118:
                    functionType = "exp(z^9)"; paramA = -0.83002; paramB = 0.87; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break; //#####
                case 120:
                    functionType = "exp(z^10)"; paramA = -0.850; paramB = 0.8789999; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break; //#####

                case 200:
                    functionType = "z*exp(z)"; paramA = 0.01; paramB = 0.00015; zoomFactor = 0.1; break;
                case 202:
                    functionType = "z^2*exp(z)"; paramA = 0.21; paramB = 0.009; break;
                case 204:
                    functionType = "z^3*exp(z)"; paramA = 0.45; paramB = 0.2; maxIter = 512; targetX = -1.3; break;
                case 206:
                    functionType = "z^4*exp(z)"; paramA = 0.45; paramB = 0.024; maxColour = 128; maxIter = 128; zoomFactor = 0.3; targetX = -13; targetY = 5; break;
                case 207: // Minovar till T-skorta
                    functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; targetY = -0.03; zoomFactor = 1.02; maxColour = 150; maxIter = 512; break;
                case 208: // Minovar
                    functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; break;
                case 209:
                    functionType = "z^5*exp(z)"; paramA = 0.52; paramB = 0.018; zoomFactor = 0.1; targetX = -15; targetY = 10; break;
                case 211: // Änglar
                    functionType = "z^5*exp(z)"; paramA = 0.5; paramB = 0.00899; maxIter = 256; maxColour = 256; break;
                case 212:
                    functionType = "z^5*exp(z)"; paramA = 0.5; paramB = 0.00899; maxIter = 256; maxColour = 256; zoomFactor = 0.1; targetX = -15; break;
                case 214: // Sökandet
                    functionType = "z^5*exp(z)"; paramA = 0.51; paramB = 0.0204; maxIter = 256; maxColour = 256; break;
                case 215:
                    functionType = "z^5*exp(z)"; paramA = 0.51; paramB = 0.0204; maxIter = 256; maxColour = 256; zoomFactor = 0.1; targetX = -15; break;
                case 217:
                    functionType = "z^5*exp(z)"; paramA = 0.502; paramB = 0.00819; maxColour = 128; break; // Playground May 2018!
                case 221:
                    functionType = "z^6*exp(z)"; paramA = 0.8; paramB = 0.31251; maxIter = 512; break;
                case 222:
                    functionType = "z^6*exp(z)"; paramA = 0.8; paramB = 0.31251; maxColour = 128; maxIter = 256; zoomFactor = 0.1; targetX = -20; break;
                case 231:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044804; maxIter = 512; break;
                case 232:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044804; maxIter = 512; zoomFactor = 0.1; targetX = -15; break;
                case 233:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044; maxIter = 512; break;
                case 234:
                    functionType = "z^7*exp(z)"; paramA = 0.65; paramB = 0.044; maxIter = 512; zoomFactor = 0.1; targetX = -15; break;

                case 300: // Featherino
                    functionType = "z - (z^4-1)/(4z^3)"; paramA = 0.099; paramB = 0.099; maxColour = 64; maxIter = 128; break;
                case 302: // Peacock
                    functionType = "z - (z^2-1)/(z^2+1)"; paramA = 0.3250; paramB = 0.72; maxColour = 128; maxIter = 256; targetX = 0.5; targetY = -0.65; zoomFactor = 0.5; break;
                case 304:
                    functionType = "z - z^3 - 4*z^4"; paramA = 0.2425; paramB = 0.0235; maxIter = 512; zoomFactor = 2.5; break;
                case 306: // Newton's method for finding roots
                    functionType = "z^3 + (c-1)*z"; paramA = 0.875; paramB = 0.0125; maxIter = 512; zoomFactor = 1.2; break;
                case 350: // "Pattis Marble"
                    functionType = "z - (z^4-1)/(2z^4)"; paramA = 0.05; paramB = 0.05; maxColour = 101; maxIter = 300; targetX = 0.05; targetY = -0.05; zoomFactor = 0.55; break;
                case 352: // "Pattis Inversed Marble #1"
                    functionType = "z - (2z^4)/(z^4-1)"; paramA = 0.06; paramB = 0.15; maxColour = 101; maxIter = 300; zoomFactor = 2.5; break;
                case 354: // "Pattis Marble #2"
                    functionType = "z - (1-z^4)/(2z^4)"; paramA = 0.08; paramB = 0.15; maxColour = 101; maxIter = 300; targetX = 0.05; targetY = -0.05; zoomFactor = 0.55; break;
                case 356: // "Pattis Inversed Marble #2"
                    functionType = "z - (2z^4)/(1-z^4)"; paramA = 0.0478; paramB = 0.09915; maxColour = 101; maxIter = 300; zoomFactor = 2.5; break;

                case 400:
                    functionType = "z^2*sin(z)"; paramA = 0.55; paramB = 0.2; zoomFactor = 2; break;
                case 402:
                    functionType = "z^2*cos(z)"; paramA = 0.48; paramB = 0.30; break;
                case 404:
                    functionType = "z^2*tan(z)"; paramA = 0.55; paramB = 0.185; break;
                case 406:
                    functionType = "z^2*sin(z)^2"; paramA = 0.64; paramB = 0.2; break;
                case 408:
                    functionType = "z^2*cos(z)^2"; paramA = 0.6246; paramB = 0.48; break;
                case 410:
                    functionType = "z^2*tan(z)^2"; paramA = 0.63; paramB = 0.485; maxColour = 256; maxIter = 256; break;

                case 500:
                    functionType = "z*sqrt(z)"; paramA = 0.019; paramB = 0.42; targetY = -0.2; zoomFactor = 0.95; break;
                case 502:
                    functionType = "z*sqrt(z)"; paramA = 0.052; paramB = 0.41; targetY = -0.2; zoomFactor = 0.95; break;
                case 510:
                    functionType = "z^2.5"; paramA = -0.34; paramB = 0.9; maxColour = 256; targetY = 0.1; break;
                case 512:
                    functionType = "z^2.5"; paramA = 0.1; paramB = 0.725; maxColour = 256; targetY = 0.1; break;
                case 520:
                    functionType = "z^e"; paramA = -0.188; paramB = 1.1035; maxColour = 256; targetY = 0.1; break;
                case 522:
                    functionType = "z^e"; paramA = -0.181; paramB = 1.1; maxColour = 256; zoomFactor = 2; break;
                case 530:
                    functionType = "z^2/e"; paramA = 0.7; paramB = 0.00333; maxColour = 1024; zoomFactor = 0.4; break;
                case 532:
                    functionType = "z^2/e"; paramA = 0.9; paramB = 0.154; zoomFactor = 0.4; break;
                case 540:
                    functionType = "z^3-z^2"; paramA = -0.30; paramB = 0.175; maxColour = 1024; break;
                case 550:
                    functionType = "z^5-z^2"; paramA = -0.43; paramB = 0.059; break;
                case 560:
                    functionType = "z^2/sin(z)"; paramA = 0.01; paramB = 0.01; maxColour = 256; maxIter = 128; targetY = -2.5; zoomFactor = 0.1; break;
                case 999:
                    functionType = "tester"; paramA = 0.3659; paramB = 0.001; targetX = -0.2; zoomFactor = 10; maxColour = 256; maxIter = 256; break;

                default:
                    {
                        // For any undefined number: select a random number!
                        selection = false;
                        formulaNr = r.Next(0, 1000);
                        selection = SelectFormula(formulaNr);
                        break;
                    }
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
                ticker++;
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
                    case "z^3 - 3z":
                        {
                            z = Complex.Subtract(Complex.Pow(z, 3), Complex.Multiply(z, 3)) + constC;
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
                    case "z^10":
                        {
                            z = Complex.Pow(z, 10) + constC;
                            break;
                        }
                    case "z^pi":
                        {
                            z = Complex.Pow(z, Math.PI) + constC;
                            break;
                        }
                    case "exp(z)":
                        {
                            z = Complex.Exp(z) + constC;
                            break;
                        }
                    case "exp(z^2)":
                        {
                            // Mitt Z!
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
                    case "exp(z^10)":
                        {
                            z = Complex.Exp(Complex.Pow(z, 10)) + constC;
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
                    case "z^2/sin(z)":
                        {
                            z = Complex.Divide(Complex.Pow(z, 2), Complex.Sin(z)) + constC;
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
                    case "z*sqrt(z)":
                        {
                            z = Complex.Multiply(z, Complex.Sqrt(z)) + constC;
                            break;
                        }
                    case "z^2.5":
                        {
                            z = Complex.Pow(z, 2.5) + constC;
                            break;
                        }
                    case "z^e":
                        {
                            z = Complex.Pow(z, Math.E) + constC;
                            break;
                        }
                    case "z^2/e":
                        {
                            z = Complex.Divide(Complex.Pow(z, 2), Math.E) + constC;
                            break;
                        }
                    case "z^3-z^2":
                        {
                            z = Complex.Subtract(Complex.Pow(z, 3), Complex.Pow(z, 2)) + constC;
                            break;
                        }
                    case "z^5-z^2":
                        {
                            z = Complex.Subtract(Complex.Pow(z, 5), Complex.Pow(z, 2)) + constC;
                            break;
                        }
                    case "tester":
                        {
                            z = Complex.Subtract(Complex.Pow(z, 3), Complex.Multiply(z, 3)) + constC;
                            break;
                        }
                }

                x = z.Real;
                y = z.Imaginary;
                radius = Math.Sqrt(x * x + y * y);

            } while (iteration < maxIter && radius < 2);
            if (iteration > maxUsedColour) maxUsedColour = iteration;
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
            long maxTime = 100_000; // Maxtime per formula (1 or more pictures). 1 minute = 60.000 mikroseconds.
            Bitmap bitmap = new Bitmap(scrWidth, scrHeight); // Bilden rensas ALDRIG i huvudloopen!

            for (int i = 1; i < 1000; i++)
            {
                try { 
                    formulaNr = i;
                    startPixWidth = 3;
                    bool choice = SelectFormula(i);
                    if (!choice) continue;
                    repaint = true;
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    while (repaint)
                    {
                        modified = false;
                        maxUsedColour = 0; // Nollställs för varje ny bild!
                        ticker = 0; // Nollställs för varje ny bild!
                        totalTicker = 0;

                        // SetHeader(0); // Initial header

                        // Rutans nedre vänstra hörn = (pixelX, pixelY)
                        for (int pixWidth = startPixWidth; pixWidth >= minPixWidth; pixWidth--)
                        {
                            for (int pixelX = 0; pixelX < scrWidth; pixelX += pixWidth)
                            {
                                for (int pixelY = 0; pixelY < scrHeight; pixelY += pixWidth)
                                {
                                    if ((pixWidth < 3) && (pixelX % 3 == 0) && (pixelY % 3 == 0))
                                        continue;
                                    else if ((pixWidth < 2) && (pixelX % 2 == 0) && (pixelY % 2 == 0))
                                        continue;

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

                                    /*
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
                                    */

                                    // Draw pixels into the bitmap -- in a way that will look like a kind of fade-in.
                                    if (pixWidth == 3)
                                    {
                                        // Draw large pixels on this level
                                        bitmap.SetPixel(pixelX, pixelY, Color.FromArgb(255, colorRed, colorGreen, colorBlue));
                                        if ((pixelX + 1 < scrWidth) && (pixelY + 1 < scrHeight))
                                        { 
                                            // Draw a crude bitmap on this level
                                            bitmap.SetPixel(pixelX + 1, pixelY, Color.FromArgb(255, colorRed, colorGreen, colorBlue));
                                            bitmap.SetPixel(pixelX + 1, pixelY + 1, Color.FromArgb(255, colorRed, colorGreen, colorBlue));
                                            bitmap.SetPixel(pixelX, pixelY + 1, Color.FromArgb(255, colorRed, colorGreen, colorBlue));
                                        }
                                    }
                                    else // Draw single pixels on the more detailed levels.
                                        bitmap.SetPixel(pixelX, pixelY, Color.FromArgb(255, colorRed, colorGreen, colorBlue));

                                    if (ticker > 1_000_000) // Ticker == one iteration of the current formula
                                    {
                                        // Copy bitmap to canvas. If this is done once per 1.000.000 ticks --> updates done approx twice per second.
                                        e.Graphics.DrawImage(bitmap, 0, 0);
                                        totalTicker += ticker;
                                        ticker = 0;

                                        // Update the window header
                                        //if (stopwatch.ElapsedMilliseconds % 1000 < 200)
                                        SetHeader(stopwatch.ElapsedMilliseconds / 1000);
                                    }
                                }// for (pixelY)

                                // Check keyboard interrupts after drawing each column
                                // if (keyboardInterrupt) break;

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

                                if (modified) break;
                            }// for (pixelX)

                            // Copy bitmap to canvas
                            e.Graphics.DrawImage(bitmap, 0, 0);
                            ticker = 0;

                            // Don't show the same picture for too long!
                            if (stopwatch.ElapsedMilliseconds > maxTime ||
                                maxUsedColour < maxColour / 4)
                            {
                                modified = true;
                                repaint = false;
                            }

                            if (modified) break;
                        } // for (pixwidth)
                        if (modified || functionType.Equals("Mandelbrot")) break;
                        //else repaint = false;

                        if (paramA == 0 && paramB == 0)
                        {
                            paramA = 0.5;
                            paramB = 0.5;
                            startPixWidth = 3;
                        }
                        else
                        {
                            paramA = paramA * 1.01;
                            paramB = paramB * 1.01;
                            startPixWidth = 3;
                        }
                    } // while (repaint)
                      // re-assign the maximum time a picture is painted
                      // maxTime = (long)(stopwatch.ElapsedMilliseconds * 1.25);
                      // if (maxTime < 30000) maxTime = 30000;

                    // Melody();
                }
                catch (Exception err)
                {
                    if (err.Source != null)
                        Console.WriteLine("Exception : {0}", err.Source);
                    throw;
                }
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

            graphName += "   [mid x: " + targetX.ToString() + "]"
                      + " [mid y: " + targetY.ToString() + "]"
                      + " [maxiter: " + maxIter + "]"
                      + " [colours: " + maxColour + "]"
                      + " [zoom: " + zoomFactor.ToString() + "]"
                      + " [mouse x: " + indicateX.ToString() + "]"
                      + " [mouse y: " + indicateY.ToString() + "]"
                      + " [timer: " + timer.ToString() + "]"
                      + " [iterats: " + totalTicker.ToString("#,#;(#,#)") + "]";

            if (timer != 0) graphName += " [iter/sek: " + (totalTicker / timer).ToString("#,#.#;(#,#.#)") + "]";
            this.Text = graphName;
        }

        public string TellName()
        {
            return functionType;
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
            this.ClientSize = new System.Drawing.Size(scrWidth, scrHeight);
            this.Name = "Fractal";
            this.Load += new System.EventHandler(this.Fractal_Load);
            this.ResumeLayout(false);
        }

        private void Fractal_Load(object sender, EventArgs e)
        {

        }
    } // End of class
}