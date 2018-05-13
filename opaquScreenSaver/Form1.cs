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
        public static int checkinterval = 2000;//5 * 60 * 1000;//5funn
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
            public Pen mPen;
            public void initPens()
            {
                
                int r= ScreenSaver.random.Next()&0xff;
                int g = ScreenSaver.random.Next() & 0xff;
                int b = ScreenSaver.random.Next() & 0xff;
                Color c = System.Drawing.Color.FromArgb((int)(255*0.75),r,g,b);
                mPen = new Pen(c, mr*1);
            }
            public void DrawElipse(Graphics g)
            {
                g.DrawEllipse(mPen, (RectangleF)this);
            }
        };
        struct idpair
        {
          public  int a, b;
        };
        public int cerclemax = 0;
        void updateCercles()
        {
            if (cerclemax == 0) cerclemax = cercles.Count;
            //System.Console.WriteLine("max " + cerclemax.ToString() + " now " + cercles.Count.ToString());

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
                        SizeF tmpvec = cercles[a].mLvec;
                       
                        cercles[a].mLvec = cercles[b].mLvec;
                        cercles[b].mLvec = new SizeF(tmpvec.Width+(float)(random.NextDouble()-0.5), tmpvec.Height+ (float)(random.NextDouble()-0.5));
                        //cercles[a].mPen.Color = cercles[b].mPen.Color = Color.Green;
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
                    if (cercles[a].areaOutCount > 3)
                        dellist.Add(a);
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
            return;
            for (int i = 0; i < cercles.Count; i++)
            {
                //myObj updateObj = new myObj(cercles[i].mLPoint[bid][0], cercles[i].mr, cercles[i].mLvec[bid][0]);
                //updateObj.mLvec[bid][0] = cercles[i].mLvec[bid][0];
                //updateObj.mLPoint[bid][0] = new PointF(
                //    updateObj.mLPoint[bid][0].X + updateObj.mLvec[bid][0].Width,
                //    updateObj.mLPoint[bid][0].Y + updateObj.mLvec[bid][0].Height
                //);
                //updateObj.mPen = cercles[i].mPen;
                //PointF updateObjmPosition = new PointF(0, 0);
                //int crossCount = 0;
                //int crossedId;// = isCross1(updateObj, cercles, i);
                //List<int> crossedIds = new List<int>();
                //int maxtrycount = 1000;
                //while ((crossedId= isCross1(updateObj, cercles, i,crossedIds)) >= 0 && crossCount < maxtrycount &&true)
                //{
                //    crossedIds.Add(crossedId);
                //    PointF avgPoint=new PointF();
                //    SizeF avgSize = new SizeF();
                //    for(int c = 0; c < crossedIds.Count; c++)
                //    {
                //        int cc = crossedIds[c];
                //        avgPoint.X += cercles[cc].mLPoint[bid][0].X;
                //        avgPoint.Y += cercles[cc].mLPoint[bid][0].Y;
                //        avgSize.Width = cercles[cc].mLvec[bid][0].Width;
                //        avgSize.Height = cercles[cc].mLvec[bid][0].Height;
                //    }
                //    avgPoint.X /= crossedIds.Count;
                //    avgPoint.Y /= crossedIds.Count;
                //    avgSize.Width /= crossedIds.Count;
                //    avgSize.Height /= crossedIds.Count;
                //    float perx = (avgPoint.X - cercles[i].mLPoint[bid][0].X) / avgSize.Width;
                //    float pery = (avgPoint.Y - cercles[i].mLPoint[bid][0].Y) / avgSize.Height;
                //    float perMin =  Math.Abs(perx) < Math.Abs(pery) ? perx: pery;
                //    updateObj.mLPoint[bid][0] = new PointF(
                //        cercles[i].mLPoint[bid][0].X + updateObj.mLvec[bid][0].Width*perMin,
                //        cercles[i].mLPoint[bid][0].Y + updateObj.mLvec[bid][0].Height*perMin
                //    );
                //    crossCount++;
                //}
                //if (crossCount == maxtrycount)
                //{
                //    updateObj.mLPoint[bid][0] = ( cercles[i].mLPoint[bid][0]);
                //}

                //if ((0 - updateObj.mr*2) < updateObj.mLPoint[bid][0].X && updateObj.mLPoint[bid][0].X < (Width + updateObj.mr * 2)) ; else updateObj.mLvec[bid][0].Width *= -1;
                //if ((0 - updateObj.mr * 2) < updateObj.mLPoint[bid][0].Y && updateObj.mLPoint[bid][0].Y < (Height + updateObj.mr * 2)) ; else updateObj.mLvec[bid][0].Height *= -1;
                //cercles[i] = updateObj;
            }
            cercles.Sort((a,b) => Math.Sign( a.mr - b.mr));
            //randamposition();
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
            float dx = 50.0f / Math.Min(a.mr , b.mr) ;

            //Math.Min(a.mr, b.mr);
            double d = Math.Sqrt(
            Math.Pow(a.mLPoint.X - b.mLPoint.X, 2) +
            Math.Pow(a.mLPoint.Y - b.mLPoint.Y, 2));
            float maxd = a.mr + b.mr;
            return maxd*dx - (float)d;
        }
        //int isCross1(myObj a,List<myObj> lb,int pass=-1,List<int> passlist=null)
        //{
        //    for(int b = 0; b < lb.Count; b++)
        //    {
        //        if (b == pass) continue;
        //        if (passlist != null)
        //        {
        //            int p = 0;
        //            for (; p < passlist.Count; p++)
        //                if (b == passlist[p]) break;
        //            if (p < passlist.Count) continue;
        //        }
        //        if (isCross(a, lb[b],nid))return b;
        //    }
        //    return -1;
        //}
        //int isCross2(List<myObj> la,List<myObj> lb,int pass=-1)
        //{
        //    for(int a = 0; a < la.Count; a++)
        //    {
        //        if (a == pass) continue;
        //        if (isCross1(la[a], lb)>=0) return a;
        //    }
        //    return -1;
        //}
        public ScreenSaver()
        {

            activeVkey = new bool[255];
            for (int i = 0; i < 255; i++) activeVkey[i] = false;

            InitializeComponent();
            //////////////////////
            WindowState = FormWindowState.Minimized;
            Visible = false;
            TopMost = true;
            timercheck.Interval = checkinterval;
            timercheck.Enabled = true;
            /////////////////
            cercles = new List<myObj>();
            random = new Random();
            this.Bounds = getFullScreen();
            //randamposition(0);
            //for (int i = 0; i < 100; i++)
            //{
            //    //System.Threading.Thread.Sleep(100);
            //    updateCercles();
            //}

        }
        //public bool isCrossCercles()
        //{
        //    for(int a = 0; a < cercles.Count; a++)
        //    {
        //        for(int b = 0; b < cercles.Count; b++)
        //        {
        //            if (a == b) continue;
        //            if (!isCross(cercles[a], cercles[b])) return false;
        //        }
        //    }
        //    return true;
            
        //}
        public void randamposition(int cercleLimit)
        {
            //cercles.Clear();

            int sw = Width;
            int sh = Height;
            int minr = (int)(Math.Min(sw,sh)/20);
            int maxr = (int)(Math.Max(sw, sh) / 10);
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
            if (dmsec > 2000) dmsec = 2000;
            msec = msec2;
            System.Console.WriteLine(dmsec);
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
        private void ScreenSaver_Shown(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - lastUserInputDateTime;
            if (span >= TimeSpan.FromMilliseconds(checkinterval))
            {
                WindowState = FormWindowState.Maximized;
                Visible = true;
                timercheck.Enabled = false;
            }
            else
            {
                timercheck.Interval = checkinterval - span.Milliseconds;
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
                r.Size = new Size(
                    Math.Max(r.Location.X + r.Width, s.Bounds.Location.X + s.Bounds.Width),
                    Math.Max(r.Location.Y + r.Height, s.Bounds.Location.Y + s.Bounds.Height)
                    );
            }
            r.Size = new Size(r.Size.Width - r.Location.X, r.Size.Height - r.Location.Y);
            return r;
        }
        private void ScreenSaver_VisibleChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                timerdraw.Stop();
                this.DoubleBuffered = false;
            }
            else if(WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else if (WindowState==FormWindowState.Normal)
            {
                mouseMoveEventCounter = 0;
                timerdraw.Start();
                this.DoubleBuffered = true;
                //
                this.Bounds = getFullScreen();
                TopLevel = true;
                TopMost = true;
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
            timercheckSub.Enabled = false;
            for (int v = 0; v < 256; v++)
            {
                if (v < 255)
                {
                    if (GetAsyncKeyState(v) != 0 && activeVkey[v])
                    {
                        lastUserInputDateTime = DateTime.Now;
                        break;
                    }
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

            timercheckSub.Enabled = true;
        }
        public void invisibleEnable()
        {
            Visible = false;
            WindowState = FormWindowState.Minimized;
            timercheck.Interval = checkinterval;
            timercheck.Enabled = true;
        }
        private void ScreenSaver_KeyDown(object sender, KeyEventArgs e)
        {
            invisibleEnable();
        }

        private void ScreenSaver_MouseMove(object sender, EventArgs e)
        {
            invisibleEnable();
        }

        private void ScreenSaver_KeyDown(object sender, MouseEventArgs e)
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
    }
}
