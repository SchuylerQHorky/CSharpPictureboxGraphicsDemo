/*Author: Schuyler Horky
 *Date: 10/22/12
 *Use: Demo program for display code
 *
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace GraphicsTemplate
{
    public partial class Form1 : Form
    {

        bool Started = false;//has the program started yet?

        Graphics Canvas;//these variables help with displaying stuff to the screen
        Graphics ScratchCanvas;
        Bitmap Scratch;

        //quit flag used to kill the main loop
        bool userQuits = false;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)//this function is triggered when the user selects the program window
        {
 	        base.OnActivated(e);

            if (Started)//if the program already started, don't start again
                return;//this gets out of the function
            else//if the program hasn't started, do stuff
                Started = true;//program needs to remember that we started it

            Canvas = PicBox.CreateGraphics();//these variables help with displaying stuff to the screen
            Scratch = new Bitmap(PicBox.Width, PicBox.Height);
            ScratchCanvas = Graphics.FromImage(Scratch);


            Font fSmall = new Font("Consolas", 10, GraphicsUnit.Point);//fSmall, fMedium, and fBig are 10, 16, and 30 point fonts.
            Font fMedium = new Font("Consolas", 16, GraphicsUnit.Point);
            Font fBig = new Font("Consolas", 30, GraphicsUnit.Point);

            float Theta = 1f;//some numbers
            int Theta2 = -100;
            float Radius = 1f;

            ScratchCanvas.FillRectangle(Brushes.Black, 0, 0, PicBox.Width, PicBox.Height);//make the background black

            while (!userQuits)
            {
                Random Rand = new Random((int)Theta*1);//creates a random seed
                byte R = (byte)((Rand.Next(0, 9) * 32) - 1);//Rand.Next makes it so the random will only give a number from 0-8 (9 is length)
                byte G = (byte)((Rand.Next(0, 9) * 32) - 1);
                byte B = (byte)((Rand.Next(0, 9) * 32) - 1);

                ScratchCanvas.FillRectangle(Brushes.Black, 0, 0, PicBox.Width, PicBox.Height);

                ScratchCanvas.DrawString("Hello World!", fSmall, new SolidBrush(Color.FromArgb(255, R, G, B)), (int)(Radius * Math.Sin(Theta)) + PicBox.Width / 2, (int)(Radius * Math.Cos(Theta)) + PicBox.Height / 2);//This function draws text to a specific location
                ScratchCanvas.DrawString("Hello World!", fMedium, new SolidBrush(Color.FromArgb(255, B, R, G)), 2*Theta2, (int)(10 * Math.Cos(Theta2)) + PicBox.Height / 2);
                ScratchCanvas.DrawString("Hello World!", fBig, new SolidBrush(Color.FromArgb(255, B, G, R)), 800 - 15 * Theta2, 5* Math.Abs((int)(3 * Math.Cos(Theta2))) + PicBox.Height / 4);

                /*
                 * The best strategy for displaying stuff to the screen is to figure out everything you want to draw to the screen and put it into
                 * a variable; in this case, ScratchCanvas or Scratch (kinda tricky like that).
                 * Once everything is in Scratch, draw Scratch to the screen.
                 */


                Canvas.DrawImage(Scratch, 0, 0);//Draw Scratch

                Theta2++;//Add stuff to numbers
                Theta *= 1.01f;
                Radius *= 1.01f;

                System.Threading.Thread.Sleep((int)(1000 / 30));//30 frames per second is a standard for display. Since the sleep function wants
                //an integer, and 1000/30 is not, we cast to an int
                Application.DoEvents();//This checks for things like when we close the program
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            userQuits = true;
        }
    }
}
