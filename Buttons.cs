using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p190329__
{
    //public class Buttons_site
    //{
    //    public Buttons_site()
    //    {
    //
    //    }
    //}


    public partial class CFactory
    {
        public CFactory()
        {

        }        
        public CButton createButtons(int type)
        {
            CButton btn = null;
            btn = defineButtonType(type);
            if(btn == null)
            {
                return null;
            }
            return btn;
        }
        public CButton defineButtonType(int _type)
        {
            CButton newbtn = null;
            
            char name = Convert.ToChar(_type);
            if (_type < 10)
            {
                newbtn = new NumButtons(_type);
            }
            if (_type > 10)
            {
                newbtn = new CalButtons(name);
            }
            if(newbtn == null)
            {
                return null;
            }
            newbtn.m_Name = name.ToString();
            return newbtn;
        }

    }
    public class CButton : Button
    {
        //UserControl Click;
        public CButton()
        {
            this.Click += new EventHandler(Click_event);
        }

        public string m_Name { get; set; }
        public virtual void Click_event(object sender, EventArgs e)
        {
            
        }
    }
    public class NumButtons : CButton
    {

        private int num;

        public NumButtons(int _d) : base()
        {

        }

        public int Num { get => num; set => num = value; }
    }
    public class CalButtons : CButton
    {
        
        char data;
        public CalButtons(char _d) : base()
        {
            data = _d;
        }

        public char Data { get => data; set => data = value; }
    }
    public class ResultButton : CalButtons
    {
        public ResultButton( char _d) : base(_d)
        {

        }
    }

}



