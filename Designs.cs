using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_OOP
{
    class Designs
    {
        FlowLayoutPanel FLPannel;
        Calcul cal;
        PrintArr mainPrint;
        PrintArr secPrint;
        public Designs(FlowLayoutPanel FLP, Calcul _cal)
        {
            cal = _cal;
            cal_seq = new List<char>();
            cal_seq.Add('+'); cal_seq.Add('-'); cal_seq.Add('/');
            cal_seq.Add('*'); cal_seq.Add('=');
            PrintArr lbl = create_Label(FLP.Width, _cal);
            lbl.Height = 30;
            lbl.Name = "PrintArr1";
            FLP.Controls.Add(lbl);
            mainPrint = lbl;
            lbl = create_Label(FLP.Width, _cal);
            lbl.Height = 20;
            lbl.Name = "PrintArr2";
            secPrint = lbl;
            FLP.Controls.Add(lbl);
            for (int y = 0; y < 5; ++y)
            {
                Buttons btn = null;
                for (int x = 0; x < 3; ++x)
                {
                    if (y == 0 && x < 2)
                    {
                        btn = create_Button("DEL", x);
                    }
                    else if (y == 0)
                    {
                        btn = create_Button(null);
                    }
                    else
                    {
                        if (y == 4 && x == 2) { }
                        else
                        {
                            btn = create_Button("NumBtn");
                        }
                    }
                    FLP.Controls.Add(btn);
                }
                btn = create_Button("CalBtn");
                FLP.Controls.Add(btn);
            }
            FLPannel = FLP;
        }
        public PrintArr create_Label(int _width, Calcul cal)
        {
            PrintArr lbl = new PrintArr(50, _width);
            lbl.cal = cal;
            return lbl;
        }
        public int _seq = 9;
        public List<char> cal_seq;
        public Buttons create_Button(string _type)
        {
            Buttons btn = null;
            if (_type == "NumBtn")
            {
                if (_seq < 0)
                {
                    btn = new Cal_Btn('.');
                }
                else if (_seq == 0)
                {
                    btn = new NumBtn(0);
                    btn.Width = 166;
                }
                else
                {
                    btn = new NumBtn(_seq);
                }
                --_seq;
            }
            else if (_type == "CalBtn")
            {
                btn = new Cal_Btn(cal_seq[0]);
                cal_seq.RemoveAt(0);
            }
            else if (_type == null)
            {
                btn = new BinBtn();
            }
            btn.cal = this.cal;
            btn.mainPrint = this.mainPrint;
            btn.secPrint = this.secPrint;
            return btn;
        }
        public Buttons create_Button(string _type, int x)
        {
            Buttons btn = null;
            if (_type == "DEL")
            {
                btn = new DELBtn(x);
            }
            btn.cal = this.cal;
            btn.mainPrint = this.mainPrint;
            return btn;
        }
        public void ReBaKe()
        {

            FLPannel.Controls[0].Width = FLPannel.Width;
            FLPannel.Controls[1].Width = FLPannel.Width;
        }
        public void Destroy()
        {
            do
            {
                FLPannel.Controls.RemoveAt(0);
            } while (FLPannel.Controls.Count != 0);
        }
    }
    class PrintArr : Label
    {
        public Calcul cal;
        public PrintArr(int _height, int _width)
        {
            this.AutoSize = false;
            this.BackColor = Color.LightGray;
            this.Width = _width;
            this.Height = _height;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.TextChanged += new EventHandler(Text_Changed);
        }
        private void Text_Changed(object sender, EventArgs e)
        {
            PrintArr print = sender as PrintArr;
            //cal.AddProcess(print.Text[print.Text.Count() - 1]);
        }
    }
    class Buttons : Button
    {
        public Calcul cal;
        public PrintArr mainPrint;
        public PrintArr secPrint;
        private string type;
        public Buttons()
        {
            this.Width = 80;
            this.Height = 60;
            this.Name = "NULL";
            this.Text = this.Name;
        }
        public void printing()
        {
            this.mainPrint.Text = this.cal.GetListString();
        }
        public void printing(int _d)
        {
            this.mainPrint.Text += _d;
        }
        public virtual void btnClick(object sender, EventArgs e)
        {

        }
        public string Type { get => type; set => type = value; }
    }
    class NumBtn : Buttons
    {
        public NumBtn(int _num) : base()
        {
            this.Type = "NumBtn";
            this.Name = _num.ToString();
            this.Text = this.Name;
            this.Click += new EventHandler(btnClick);
        }
        public override void btnClick(object sender, EventArgs e)
        {
            NumBtn btn = sender as NumBtn;  // 현재 버튼 객체
            int temp = this.cal.AddProcess(int.Parse(this.Name));
            if (temp == 1)
            {
                printing();
            }
            printing(int.Parse(this.Name));
            List<string> dummy = this.cal.GetList();
        }
    }
    class Cal_Btn : Buttons
    {
        public Cal_Btn(char _cal) : base()
        {
            this.Type = "CalBtn";
            this.Name = _cal.ToString();
            this.Text = this.Name;
            this.Click += new EventHandler(btnClick);
        }
        public override void btnClick(object sender, EventArgs e)
        {
            Cal_Btn btn = sender as Cal_Btn;  // 현재 버튼 객체
            this.cal.AddProcess(this.Name.ToCharArray()[0]);
            printing();

        }
    }
    class DELBtn : Buttons
    {
        bool C;
        public DELBtn(int x) : base()
        {
            this.Type = "DELbtn";
            this.Name = "DEL ";
            if (x > 0)
            {
                this.C = false;
                this.Name += "CE";
            }
            else
            {
                this.C = true;
                this.Name += "C";
            }
            this.Text = this.Name;
            this.Click += new EventHandler(btnClick);

        }
        public override void btnClick(object sender, EventArgs e)
        {
            this.cal.DelProcess(C);
            printing();
        }
    }
    class BinBtn : Buttons
    {
        bool C;
        public BinBtn() : base()
        {
            this.Type = "binBtn";
            this.Name = "Binary ";
            this.Text = this.Name;
            this.Click += new EventHandler(btnClick);

        }
        public override void btnClick(object sender, EventArgs e)
        {
            if (cal.getfin())
            {
                this.secPrint.Text = cal.Result();
            }
        }
    }
}
