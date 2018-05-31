using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opaquScreenSaver
{
    public partial class ScreenSaver : Form
    {
        public static Random random;
        //public static int checkinterval = 2000;//5 * 60 * 1000;//5funn
        public DateTime lastUserInputDateTime = DateTime.Now;
        public bool[] activeVkey;
        public int initialcount = 0;
        /// <summary>
        /// ///////
        /// </summary>
        public class myObj {
            public PointF mLPoint;
            public float mr;
            public SizeF mLvec;
            public int areaOutCount = 0;
            public myObj(PointF inPoint, float inR, SizeF invec)//Random irandom = null)
            {
                mLPoint = inPoint;// new PointF();
                mLvec = invec;// new SizeF();
                //for (int i = 0; i < 2; i++) {
                //    mLPoint[i] = new List<PointF>();
                //    mLvec[i] = new List<SizeF>();
                //    //mLPoint[i].Clear();
                //    //mLvec[i].Clear();
                //    mLPoint[i].Add(inPoint);
                //    mLvec[i].Add(invec);
                //}
                mr = inR;
            }
            public void copyValue(myObj updateParam)
            {
                mLPoint = updateParam.mLPoint;
                mr = updateParam.mr;
                mLvec = updateParam.mLvec;
                mPen = updateParam.mPen;
            }
            public static implicit operator RectangleF(myObj obj)
            {
                PointF tmpPF = new PointF(obj.mLPoint.X - obj.mr, obj.mLPoint.Y - obj.mr);
                float tmpr2 = obj.mr * 1;
                SizeF tmpSF = new SizeF(tmpr2, tmpr2);
                return new RectangleF(tmpPF, tmpSF);
            }
            public static implicit operator System.Windows.Rect(myObj obj)
            {
                System.Windows.Point tmpPF = new System.Windows.Point(obj.mLPoint.X - obj.mr, obj.mLPoint.Y - obj.mr);
                float tmpr2 = obj.mr * 1;
                System.Windows.Point tmpSF = new System.Windows.Point(tmpr2, tmpr2);
                return new System.Windows.Rect(tmpPF, tmpSF);
            }
            public Pen mPen;
            public void initPens()
            {
                
                int r= ScreenSaver.random.Next()&0xff;
                int g = ScreenSaver.random.Next() & 0xff;
                int b = ScreenSaver.random.Next() & 0xff;
                int a1 = (int)(255 * 0.75);
                int a2 = (int)(255 * 0.25);
                Color c1 = System.Drawing.Color.FromArgb(a1,r,g,b);
                //Color c2 = System.Drawing.Color.FromArgb(a2, r, g, b);

                //Point p1 = new Point((int)mLPoint.X,(int)mLPoint.Y);
                //Point p2= new Point((int)(mLPoint.X+mr), (int)(mLPoint.Y+mr));

                //System.Drawing.Drawing2D.LinearGradientBrush mBrush = new System.Drawing.Drawing2D.LinearGradientBrush(p1,p2,c1,c2) ;

                mPen = new Pen(c1, mr*1);
            }
            class mybrush : System.Drawing.Brush
            {
                public override object Clone()
                {
                    return this;
                    throw new NotImplementedException();
                }
                public mybrush():base()
                {
                    
                }
            }
            public void DrawElipse(Graphics gr)
            {
                int r = mPen.Color.R;
                int g = mPen.Color.G;
                int b = mPen.Color.B;
                int a1 = (int)(255 * 0.75);
                int a2 = (int)(255 * 0.25);
                Color c1 = System.Drawing.Color.FromArgb(a1, g, b,r);
                Color c2 = System.Drawing.Color.FromArgb(a2, r, g, b);

                Point p1 = new Point((int)(mLPoint.X - mr * 3.14/2), (int)(mLPoint.Y + mr * 3.14/2));
                Point p2 = new Point((int)(mLPoint.X + mr * 3.14 / 2), (int)(mLPoint.Y - mr * 3.14 / 2));

                System.Drawing.Drawing2D.LinearGradientBrush mBrush = new System.Drawing.Drawing2D.LinearGradientBrush(p1, p2, c1, c2);
                mybrush test = new mybrush();
                //Pen pen = new Pen(test,mr);
                Pen pen = new Pen(mBrush,mr);
                gr.DrawEllipse(pen, (RectangleF)this);
                /////
                //System.Windows.Media.RadialGradientBrush blush = new System.Windows.Media.RadialGradientBrush();
                //blush.GradientOrigin = new System.Windows.Point(0.75, 0.25);
                //blush.GradientStops.Add(new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Yellow, 0));
                //blush.GradientStops.Add(new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Orange, 0.5));
                //blush.GradientStops.Add(new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Red, 1));

                //System.Windows.Media.rectangle eg = new System.Windows.Media.EllipseGeometry(this);

                //System.Windows.Media.RectangleGeometry rg = new System.Windows.Media.RectangleGeometry(this);

                //Blush
                //Blush blush2 = new Blush();

                //Pen rPen = new Pen(blush2);

                //g.DrawEllipse(blush, this);
            }
        };
        struct idpair
        {
          public  int a, b;
        };
        public int cerclemax = 1;
        SizeF sizexdouble(SizeF size,double x)
        {
            return new SizeF(size.Width * (float)x, size.Height * (float)x);
        }
        SizeF sizexsize(SizeF s0, SizeF s1)
        {
            return new SizeF(s0.Width * s1.Width, s0.Height * s1.Height);
        }
        double lenSize(SizeF s)
        {
            return Math.Sqrt(s.Width * s.Width + s.Height * s.Height);
        }
        SizeF normsize(SizeF s)
        {
            double l = lenSize(s);
            return sizexdouble(s, 1 / l);
        }
        double dotsize(SizeF a,SizeF b)
        {
            return a.Width * b.Width + a.Height * b.Height;
        }
        
        SizeF reflectSize(SizeF s,SizeF n)
        {
            s = sizexdouble(s, -1);
            return sizexdouble( sizexdouble(n,2) , dotsize(s, n) )- s;
        }
        void updateCercles()
        {
            //if (cerclemax == 0) cerclemax = cercles.Count;
            System.Console.WriteLine("max " + cerclemax.ToString() + " now " + cercles.Count.ToString());

            float step = 1;
            for (int a = 0; a < cercles.Count; a++)
            {
                SizeF addSizeF = new SizeF(cercles[a].mLvec.Width * step, cercles[a].mLvec.Height * step);
                cercles[a].mLPoint = cercles[a].mLPoint + addSizeF;
                //cercles[a].mPen.Color = Color.Blue;
            }
            List<idpair> crossPair = new List<idpair>();
            for (int a = 0; a < cercles.Count; a++)
            {
                for (int b = a + 1; b < cercles.Count; b++)
                {
                    float crossF = isCross(cercles[a], cercles[b]);
                    if (crossF == 0)
                    {
                        //cercles[a].mLvec[nid][0] = cercles[b].mLvec[bid][0];
                        //cercles[b].mLvec[nid][0] = cercles[a].mLvec[bid][0];
                        //cercles[a].mPen.Color = cercles[b].mPen.Color = Color.Black;
                    }
                    else
                    if (crossF > 0)
                    {
                        idpair idp = new idpair();
                        idp.a = a;
                        idp.b = b;
                        crossPair.Add(idp);
                        SizeF tmpveca = cercles[a].mLvec;
                        SizeF tmpvecb = cercles[b].mLvec;
                        //tmpvecb = new SizeF(tmpvecb.Width + (float)(random.NextDouble() - 0.5), tmpvecb.Height + (float)(random.NextDouble() - 0.5));

                        double arx = cercles[a].mr;
                        arx *= arx;
                        arx += (random.NextDouble() - 0.5)/4;
                        double brx = cercles[b].mr;
                        brx *= brx;
                        brx += (random.NextDouble() - 0.5)/4;

                        SizeF arvec = new SizeF(tmpveca.Width * (float)arx, tmpveca.Height * (float)arx);
                        SizeF brvec = new SizeF(tmpvecb.Width * (float)brx, tmpvecb.Height * (float)brx);

                        double allx = 1/crossF;// arx + brx;
                        double aax = 0*arx / allx;// brx / arx*1.001;
                        double abx = brx / allx;// arx / brx*1.001;
                        double bax = 0*brx / allx;
                        double bbx = arx / allx;

                        //cercles[a].mLvec = new SizeF((float)(brvec.Width / arx), (float)(brvec.Height / arx));
                        //cercles[b].mLvec = new SizeF((float)(arvec.Width / brx), (float)(arvec.Height / brx));
                        //cercles[a].mLvec = new SizeF((float)(tmpveca.Width* aax + tmpvecb.Width * abx), (float)(tmpveca.Width * aax + tmpvecb.Width * abx));
                        //cercles[b].mLvec = new SizeF((float)(tmpveca.Width * bax + tmpvecb.Width * bbx), (float)(tmpveca.Width * bax + tmpvecb.Width * bbx));
                        //cercles[a].mPen.Color = cercles[b].mPen.Color = Color.Green;
                        ////////////////
                        SizeF dn = normsize(cercles[a].mLvec - cercles[b].mLvec);
                        double ar3 = Math.Pow(cercles[a].mr, 3);
                        double br3 = Math.Pow(cercles[b].mr, 3);

                       
                        SizeF avec = normsize(cercles[a].mLvec);///ar3);
                        SizeF bvec = normsize( cercles[b].mLvec);/// br3);
                        SizeF reflecta = reflectSize(avec, dn);
                        SizeF reflectb = reflectSize(bvec, dn);


                        double per = ar3 + br3;
                        cercles[a].mLvec = sizexdouble(reflecta, 1);//* ar3);
                        cercles[b].mLvec = sizexdouble( reflectb, 1);//* br3);
                        //double per = ar3 + br3;
                        //double perx = 1;
                        //cercles[a].mLvec = sizexdouble( cercles[a].mLvec, ar3 / per/ perx) + sizexdouble(reflecta, perx - br3 / per);
                        //cercles[b].mLvec = sizexdouble(cercles[a].mLvec, br3 / per / perx) + sizexdouble(reflectb, perx - ar3 / per);
                    }
                }
            }
            List<int> dellist = new List<int>();
            for (int a = 0; a < cercles.Count; a++)
            {
                float blankr = 4;
                bool isDel = false;
                if (-cercles[a].mr * blankr < cercles[a].mLPoint.X && cercles[a].mLPoint.X < Width + cercles[a].mr * blankr) ;
                else
                {
                    isDel = true;
                    cercles[a].mLvec.Width *= -1;
                }
                if (-cercles[a].mr * blankr < cercles[a].mLPoint.Y && cercles[a].mLPoint.Y < Height + cercles[a].mr * blankr) ;
                else
                {
                    isDel = true;
                    cercles[a].mLvec.Height *= -1;
                }
                if (isDel)
                {
                    cercles[a].areaOutCount++;
                    if (cercles[a].areaOutCount > 10)
                        dellist.Add(a);
                    else
                    {
                        cercles[a].mLPoint += cercles[a].mLvec;
                    }
                }
            }
            while (dellist.Count > 0)
            {
                int delId = dellist[dellist.Count - 1];
                if (cercles.Count <= delId)
                {
                    int yabai = 0;
                }
                cercles.RemoveAt(delId);
                dellist.RemoveAt(dellist.Count - 1);
            }

            if (cercles.Count < cerclemax)
                randamposition(cerclemax);// dellist.Count + cercles.Count);
            return;
        }
        List<myObj> cercles;
        int mouseMoveEventCounter;
        
        //bool isCross(RectangleF a, RectangleF b)
        //{
        //    float ar = a.Width / 2;
        //    float br = b.Width / 2;

        //    float acx = a.Location.X + ar;
        //    float acy = a.Location.Y + ar;
            
        //    float bcx = b.Location.X + br;
        //    float bcy = b.Location.Y + br;
        //    double d = Math.Sqrt(
        //    Math.Pow(acx - bcx, 2) +
        //    Math.Pow(acy - bcy, 2));
        //    float maxd = ar+br;
        //    return d < maxd;
        //}
        float isCross(myObj a,myObj b)
        {
            //if (Math.Min(a.mr, b.mr) > Math.Abs(a.mr - b.mr)) return -1;
            //if(Math.Abs(a.mr - b.mr)>50)return 1;
            double dx = //50.0f / Math.Min(a.mr , b.mr) ;
                Math.Sqrt(1-(Math.Max(a.mr, b.mr) - Math.Min(a.mr, b.mr) )/ Math.Max(a.mr, b.mr));
            //dx = 1;

            //Math.Min(a.mr, b.mr);
            double d = Math.Sqrt(
            Math.Pow(a.mLPoint.X - b.mLPoint.X, 2) +
            Math.Pow(a.mLPoint.Y - b.mLPoint.Y, 2));
            float maxd = a.mr + b.mr;
            return maxd*(float)dx - (float)d;
        }
        int checkinterval
        {
            get
            {
                const int m = 60 * 1000;
                if (menu1.Checked) return m * 1;
                if (menu15.Checked) return m * 15;
                if (menu30.Checked) return m * 30;
                if (menu60.Checked) return m * 60;
                if (menu120.Checked) return m * 120;
                return 120*m;
            }
        }
        public ScreenSaver()
        {

            activeVkey = new bool[255];
            for (int i = 0; i < 255; i++) activeVkey[i] = false;

            InitializeComponent();
            //////////////////////
            timercheck.Interval = checkinterval;
            timercheck.Enabled = true;
            /////////////////
            cercles = new List<myObj>();
            random = new Random();
        }
        public void randamposition(int cercleLimit)
        {
            //cercles.Clear();

            int sw = Width;
            int sh = Height;
            int minr = (int)(Math.Min(sw,sh)/100);
            int maxr = (int)(Math.Max(sw, sh) / 20);
            int sleshCount = 2000;// (sw*sh)>>(4*101);
            int count = 0;
            while (count < sleshCount)
            {
                //System.Threading.Thread.Sleep(100);
                int r = (int)(minr + (maxr - minr) * random.NextDouble());
                int cx;//= (int)(sw * random.NextDouble());
                int cy;//= (int)(sh * random.NextDouble());
                if (cercleLimit > 0||true)
                {
                    double x=random.NextDouble() * 2 * (sw + sh);
                    if (x < sw)
                    {
                        cx = (int)(x-0);
                        cy = -2*r;
                    }
                    else if (x < 2 * sw)
                    {
                        cx = (int)(x - sw);
                        cy = sh + 2 * r;
                    }
                    else if (x < 2 * sw + sh)
                    {
                        cx = -2 * r;
                        cy = (int)(x - 2 * sw);
                    }
                    else if (x < 2 * sw + 2*sh)
                    {
                        cx = sw + 2 * r;
                        cy = (int)(x - 2 * sw - sh);
                    }
                    else
                    {
                        cx = 0;
                        cy = 0;
                        int yabai = 0;
                    }
                }
                else
                {
                    cx = (int)(sw * random.NextDouble());
                    cy = (int)(sh * random.NextDouble());
                }
                SizeF vec=new SizeF(0,0);
                do
                {
                    vec.Width = (float)(random.NextDouble() - 0.5);
                    vec.Height = (float)(random.NextDouble() - 0.5);
                } while(vec.Width==0&&vec.Height==0);
                double d = Math.Sqrt(vec.Width * vec.Width + vec.Height * vec.Height);
                vec.Width /= (float)d;
                vec.Height /= (float)d;
                myObj newcercle = new myObj(
                    new PointF(cx, cy), r,vec
                    );
                newcercle.initPens();
                bool findCross = false;
                foreach (myObj tgt in cercles)
                {
                    if (isCross(newcercle, tgt)>=0)
                    {
                        findCross |= true;
                        break;
                    }
                }
                if (!findCross)
                {
                    cercles.Add(newcercle);
                    if (cercleLimit != 0 && cercles.Count > cercleLimit) break;
                    continue;
                    //errorCount = 0;
                }
                //else
                count++;
            }
            if (initialcount == 0) initialcount = cercles.Count;
            return;
        }



        int msec = 0;
        private void draw()
        {
            //using System.Drawing;

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(this.Width, this.Height);
            DateTime now = DateTime.Now;
            int msec2 =1000*60*now.Minute+ 1000* now.Second+ now.Millisecond;
            int dmsec = msec2 - msec;
            if (dmsec > 16*8) cerclemax--;
            else cerclemax++;
            if (cerclemax < 1) cerclemax = 1;
            if (dmsec > 2000) dmsec = 2000;
            msec = msec2;
           System.Console.WriteLine(
               "draw"+dmsec);
            //System.Console.WriteLine("%d %d",initialcount,cercles.Count);
            for (int i=0;i<(dmsec/(16)*1+1);i++)
                updateCercles();

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //Penオブジェクトの作成(幅1の黒色)
            //(この場合はPenを作成せずに、Pens.Blackを使っても良い)
            //Pen p = new Pen(Color.Black, 1);
            foreach (myObj r in cercles) {
                r.DrawElipse(g);
                //g.DrawEllipse(p, r);
            }            
            ////3つの長方形の位置と大きさを配列に入れる
            //Rectangle[] recst = {new Rectangle(0, 0, 40, 20),
            //            new Rectangle(10, 5, 20, 50),
            //            new Rectangle(5, 10, 50, 40)};
            ////3つの長方形を描く
            //g.DrawRectangles(p, recst);

            //リソースを解放する
            //p.Dispose();
            g.Dispose();

            //PictureBox1に表示する
            Image oldImage = this.BackgroundImage;
            this.BackgroundImage = canvas;
            if(oldImage!=null)oldImage.Dispose();

            //canvas.Dispose();
        }
        private void timercheck_tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - lastUserInputDateTime;
            System.Console.WriteLine("span"+span);
            if (span >= TimeSpan.FromMilliseconds(checkinterval))
            {
                Console.WriteLine("timecheck"+ TimeSpan.FromMilliseconds(checkinterval));
                WindowState = FormWindowState.Normal;
                Visible = true;
                TopMost = true;
                timercheck.Enabled = false;
            }
            else
            {
                timercheck.Interval = checkinterval - span.Milliseconds;
                Console.WriteLine("timecheckElse" + TimeSpan.FromMilliseconds(checkinterval));
                if (!timercheck.Enabled)
                {
                    timercheck.Enabled = true;
                }
            }
        }
        private void ScreenSaver_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveEventCounter++;
            if (mouseMoveEventCounter > 20)
            {
                mouseMoveEventCounter = 0;
                invisibleEnable();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Visible)
            {
                updateCercles();
                draw();
            }
        }
        Rectangle getFullScreen()
        {
            Rectangle r = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            r.Size = new Size(r.Size.Width + r.Location.X, r.Size.Height + r.Location.Y);
            foreach (Screen s in System.Windows.Forms.Screen.AllScreens)
            {
                r.Location = new Point(
                    Math.Min(r.Location.X, s.Bounds.Location.X),
                    Math.Min(r.Location.Y, s.Bounds.Location.Y)
                    );
            }
            foreach (Screen s in System.Windows.Forms.Screen.AllScreens)
            {
                    r.Size = new Size(
                    Math.Max(r.Location.X + r.Width, r.Location.X + s.Bounds.Size.Width),
                    Math.Max(r.Location.Y + r.Height, r.Location.Y + s.Bounds.Size.Height)
                    );
            }
            r.Size = new Size(r.Size.Width - r.Location.X, r.Size.Height - r.Location.Y);
            r.Size = new Size(r.Size.Width - r.Location.X, r.Size.Height - r.Location.Y);
            return r;
        }
        //FormWindowState laststate = FormWindowState.Normal;
        private void ScreenSaver_VisibleChanged(object sender, EventArgs e)
        {
            //if (laststate == WindowState) return;
            //else laststate = WindowState;
            if (WindowState == FormWindowState.Minimized)
            {
                Console.WriteLine("min");
                timercheck.Interval = checkinterval;
                timercheck.Enabled = true;
                timerdraw.Stop();
                this.DoubleBuffered = false;
            }
            //else if(WindowState == FormWindowState.Maximized)
            //{
            //    Console.WriteLine("max");
            //    WindowState = FormWindowState.Normal;
            //}
            else if (WindowState == FormWindowState.Normal)
            {
                Console.WriteLine("normal");
                this.ScaleControl(new SizeF(1, 1), BoundsSpecified.None);

                Rectangle fulls= getFullScreen();
                if (this.Bounds != fulls) this.Bounds = fulls;
                mouseMoveEventCounter = 0;
                timerdraw.Enabled=true;
                this.DoubleBuffered = true;
                //
                //Visible = true;
                //TopLevel = true;
                //TopMost = true;
            }
            //else if(WindowState == FormWindowState.Normal)
            //{
            //    mouseMoveEventCounter = 0;
            //    timerdraw.Start();
            //    this.DoubleBuffered = true;
            //}
            else
            {
                int breaki=0;
            }
        }
        
        private void ScreenSaver_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);
        struct POINT
        {
            public int x { get; set; }
            public int y { get; set; }
            public static implicit operator Point(POINT point)
            {
                return new Point(point.x, point.y);
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT result);

        POINT lastmpos;
        private void timercheckSub_Tick(object sender, EventArgs e)
        {
            //timercheckSub.Enabled = false;
            for (int v = 0; v < 256; v++)
            {
                if (v < 255)
                {
                    
                    int keyState = GetAsyncKeyState(v);
                    //System.Console.WriteLine(v.ToString() + " " + activeVkey[v]+" "+keyState);
                    if (keyState != 0 && activeVkey[v])
                    {
                        lastUserInputDateTime = DateTime.Now;
                        this.timercheck.Enabled = true;
                        System.Console.WriteLine(v);

                        System.Console.WriteLine(lastUserInputDateTime);

                        break;
                    }
                    else if (v==29) ;//OEM specific
                    else if (v == 246) ;//OEM specific
                    else if (0xe9 <= v && v <= 0xf5) ;//OEM specific
                    else activeVkey[v] = true;
                }
                else
                {
                    POINT nowmpos = new POINT();
                    //if (lastcpos == null) lastcpos = nowcpos;
                    if (GetCursorPos(out nowmpos))
                    {
                        if (lastmpos.x != nowmpos.x || lastmpos.y != nowmpos.y)
                        {
                            lastUserInputDateTime = DateTime.Now;
                            this.timercheck.Enabled = true;
                            lastmpos = nowmpos;
                            {
                                invisibleEnable();
                            }
                            break;
                        }
                    }
                    else
                    {
                        //lastUserInputDateTime = DateTime.Now;
                    }
                }
            }
            //timercheckSub.Enabled = true;
        }
        public void invisibleEnable()
        {
            Visible = false;
            //WindowState = FormWindowState.Minimized;
        }
        private void ScreenSaver_KeyDown(object sender, KeyEventArgs e)
        {
            invisibleEnable();
        }

        private void ScreenSaver_KeyDown(object sender, KeyPressEventArgs e)
        {
            invisibleEnable();
        }

        private void ScreenSaver_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            invisibleEnable();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        
        private void menuExit_CheckStateChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menu0_CheckedChanged(object sender, EventArgs e)
        {
            menu0.Checked = false;
            lastUserInputDateTime = new DateTime();
            timercheck.Interval = 1;
            timercheck.Enabled = true;
        }
        private void menuX_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem[] items = { menu1, menu15,menu30,menu60,menu120};
            System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
            //foreach (System.Windows.Forms.ToolStripMenuItem i in items)
            for(int i=0;i<5;i++)
            {
                //if (i == item) item.Checked = true;
                //else
                items[i].Checked = items[i]==item;
                if (items[i].Checked) timercheck.Interval = checkinterval;
            }
        }
    }
}
