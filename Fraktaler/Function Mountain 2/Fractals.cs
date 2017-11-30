using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

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

        private Fractal()
        {
            // This constructur selects the formula that you want to view.

            new Thread(() =>
            {
                this.Paint += new PaintEventHandler(PaintGraph);
            }).Start();

        }

        private bool SelectFormula(int i)
        {
            bool selection = true;
            switch (i)
            {
                case 0:
                    functionType = "Mandelbrot"; maxColour = 512; maxIter = 2048; targetX = -0.75; break;
                case 2:
                    functionType = "Julia"; paramA = -0.5; paramB = 0.6; maxColour = 256; maxIter = 1024; targetX = 0; break;
                case 4:
                    functionType = "Julia"; paramA = -0.835; paramB = -0.2321; maxColour = 256; maxIter = 1024; targetX = 0; zoomFactor = 2; break;
                case 6:
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
                    functionType = "exp(z^2)"; paramA = -0.5; paramB = -0.17; maxColour = 256; maxIter = 256; targetY = 0; zoomFactor = 0.3; break;
                case 104:
                    functionType = "exp(z^3)"; paramA = -0.620921; paramB = 0.00127; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 106:
                    functionType = "exp(z^3)"; paramA = -0.615; paramB = 0.0022; maxColour = 256; maxIter = 265; zoomFactor = 1; break;
                case 108:
                    functionType = "exp(z^4)"; paramA = -0.53; paramB = 0.00057; maxColour = 256; maxIter = 265; zoomFactor = 0.5; break;
                case 110:
                    functionType = "exp(z^5)"; paramA = -0.54; paramB = 0.77; maxColour = 256; maxIter = 512; zoomFactor = 0.75; break;

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
                default:
                    selection = false; break;
            }
            return selection;
        }

        private double FractalMath(double x, double y, double constA, double constB)
        {
            // This is the method for the central mathematical calculations.

            // https://en.wikipedia.org/wiki/Julia_set <-- Dessa har jag testat att återskapa
            // https://en.wikipedia.org/wiki/Newton_fractal <-- Dessa har jag inte testat...
            // https://www.wolframalpha.com/input/?i=exp(a%2Bbi)
            // https://www.wolframalpha.com/input/?i=exp((a%2Bbi)%5E2) <-- Förenklar formler till "alternate form"!
            // http://fractalfoundation.org/resources/what-are-fractals/ <-- Some other and more complex examples...

            int iteration = 0;
            double a = x;
            double b = y;
            double aprim = 0;
            double bprim = 0;
            double radius = 0;

            if (functionType == "Mandelbrot")
            {
                // För den enkla funktionen "Mandelbrot" är alltid konstanterna = koordinaterna
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
                            aprim = a * a * a * a * a * a * a * a - 28 * a * a * a * a * a * a * b * b + 70 * a * a * a * a * b * b * b * b
                                  - 28 * a * a * b * b * b * b * b * b + b * b * b * b * b * b * b * b + constA;
                            bprim = 8 * a * a * a * a * a * a * a * b - 56 * a * a * a * a * a * b * b * b + 56 * a * a * a * b * b * b * b * b
                                   - 8 * a * b * b * b * b * b * b * b + constB;
                            break;
                        }
                    case "z^9":
                        {
                            aprim = a * a * a * a * a * a * a * a * a - 36 * a * a * a * a * a * a * a * b * b + 126 * a * a * a * a * a * b * b * b * b
                                  - 84 * a * a * a * b * b * b * b * b * b + 9 * a * b * b * b * b * b * b * b * b + constA;
                            bprim = 9 * a * a * a * a * a * a * a * a * b - 84 * a * a * a * a * a * a * b * b * b + 126 * a * a * a * a * b * b * b * b * b
                                  - 36 * a * a * b * b * b * b * b * b * b + b * b * b * b * b * b * b * b * b + constB;
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
                            bprim = Math.Exp(a) * a * Math.Sin(b) + Math.Exp(a) * b * Math.Cos(b) + constB;
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

        private double Pix2CoordX(int x)
        {
            // Convert screen coordinate X to mathematical coordinate X
            return targetX + Convert.ToDouble(x - scrWidth / 2) / scrHeight * 2.5 / zoomFactor;
        }

        private double Pix2CoordY(int y)
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

            for (int i = 15; i < 1000; i++)
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

                    paramA = paramA + 0.0001;
                    paramB = paramB + 0.0001;
                    startPixWidth = 1;

                } // while (repaint)
                // re-assign the maximum time a picture is painted
                maxTime = (long)(stopwatch.ElapsedMilliseconds * 2);
                if (maxTime < 30000) maxTime = 30000;
                if (maxTime > 300000) maxTime = 300000;
            } // for (int i...
        }

        private void SetHeader(long timer)
        {
            graphName = "Formula " + formulaNr + ": ";

            if (functionType == "Mandelbrot") graphName += "Mandelbrot z'= z^2 + c";
            else if (functionType == "Julia") graphName += "Julia Set z' = z " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";
            else graphName += "Julia Set z' = " + functionType + " + " + Convert.ToString(paramA) + " + " + Convert.ToString(paramB) + "i";

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
            this.ClientSize = new System.Drawing.Size(500, 500);
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
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Fractal";
            this.Load += new System.EventHandler(this.Fractal_Load);
            this.ResumeLayout(false);

        }

        private void Fractal_Load(object sender, EventArgs e)
        {

        }
    } // End of class
}