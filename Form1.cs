using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_OOP
{

    public partial class Form1 : Form
    {

        // 사칙연산 우선순위
        // 1.괄호 먼저 처리한다
        // 2.곱셈과 나눗셈 먼저 처리한다
        // 3. 우선순위가 같은경우 먼저 기술된 순서대로 처리한다
        Designs designs;    //동적생성 디자인 클래스
        Calcul cal;         //계산 모듈
        FlowLayoutPanel FLPannel;

        public Form1()
        {
            InitializeComponent();
            FLPannel = flowLayoutPanel1;
            cal = new Calcul();             //계산모듈 생성
            designs = new Designs(FLPannel,cal);    //동적생성을 위한 디자인 클래스 생성(레이아웃, 계산모듈)
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            designs.ReBaKe();
        }
    }
}
