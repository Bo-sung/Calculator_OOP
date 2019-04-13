using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_OOP
{
    class Calcul
    {
        //      Inputs :
        //      AddProcess(Int32) 	//숫자 입력
        //      AddProcess(char)	//부호 입력
        //      DelProcess(Boolean)	//삭제 명령, True : C, False : CE
        //      
        //      Outputs :
        //      getFin()	//계산 완료여부 Boolean로 반환
        //      GetListPrInt32()	//입력된 List 데이터 String으로 반환.
        //      ReturnBinary()	//결과값 2진수로 변환후 String으로 반환

        Boolean min;
        Boolean fin;
        public Boolean getfin()
        {
            return fin;
        }
        private String m_result;    //결과
        private Boolean prev_ch;
        List<String> m_callist = new List<String>();
        List<String> m_printList = new List<String>();
        void reset_printlist()
        {
            m_printList = null;
            m_printList = new List<String>();
        }
        void reset_callist()
        {
            m_callist = null;
            m_callist = new List<String>();
        }
        public List<String> GetList()
        {
            List<String> _list = m_callist;
            return _list;
        }
        public String GetListString()
        {
            String str = null;
            for(Int32 i = 0; i < m_printList.Count; ++i)
            {
                str += m_printList[i] + " ";
            }
            return str;
        }
        Boolean Prev_ch { get => prev_ch; set => prev_ch = value; }
        public String returnBinary()
        {
            String output16 = null;
            if (m_printList.Count > 0 && m_printList.Count % 2 == 1)
            {
                 output16 = System.Convert.ToString(Int32.Parse(m_printList[m_printList.Count - 1]),2);
            }
            return output16;
        }
        public Calcul()
        {
            min = false;
            fin = false;
        }
        public void DelProcess(Boolean c)
        {
            if (c)//다 지움
            {
                reset_callist();
                reset_printlist();
                min = false;
                prev_ch = false;
            }
            else if(!fin) //지금꺼만 지움
            {                
                m_printList.RemoveAt(m_printList.Count - 1);
            }
            m_result = null;
        }
        public Int32 AddProcess(Int32 _d)
        {
            if(fin)
            {
                DelProcess(true);
                m_result += _d.ToString();
                Prev_ch = false;
                fin = false;
                return 1;
            }
            m_result += _d.ToString();
            Prev_ch = false;
            return 0;
        }
        public Int32 AddProcess(char _d)
        {
            
            if (_d == '.')
            {
                m_result += _d;
            }
            else
            {
                if(!fin &&m_result != null)
                {
                    m_printList.Add(m_result);
                }
                fin = false;
                if (min)
                {
                    if (m_result != null)
                    {
                        float temp = float.Parse(m_result);
                        temp = temp * -1;
                        m_result = temp.ToString();
                    }
                    min = false;
                }
                if (Prev_ch)
                {
                    m_callist.RemoveAt(m_callist.Count - 1);
                    m_printList.RemoveAt(m_printList.Count -1);
                    min = false;
                }
                else
                {
                    m_callist.Add(m_result);                    
                    m_result = null;
                }
                if (_d != '=')
                {
                    if (_d == '-')
                    {
                        m_callist.Add("+");
                        min = true;
                    }
                    else
                    {
                        m_callist.Add(_d.ToString());
                    }
                    m_printList.Add(_d.ToString());
                    Prev_ch = true;
                }
                else
                {
                    m_printList.Add(_d.ToString());
                    m_callist = calculate(m_callist);
                    m_result = m_callist[0].ToString();
                    m_printList.Add(m_result);
                    m_callist.RemoveAt(0);
                    fin = true;
                }
            }
            return 0;
        }
        public String Result()
        {
            return returnBinary();
        }
        List<String> calculate(List<String> _list)
        {
            List<String> list = _list;
            for (Int32 i = 0; i < list.Count(); ++i)
            {
                if (list[i] == "*")
                {
                    
                    Decimal temp = 0;
                    temp = Decimal.Parse(list[i - 1]) * Decimal.Parse(list[i + 1]);
                    list[i - 1] = temp.ToString();
                    list.RemoveAt(i);
                    list.RemoveAt(i);
                    i = 0;
                }
                else if(list[i] == "/")
                {
                    Decimal temp = 0;
                    temp = Decimal.Parse(list[i - 1]) / Decimal.Parse(list[i + 1]);
                    list[i - 1] = temp.ToString();
                    list.RemoveAt(i);
                    list.RemoveAt(i);
                    i = 0;
                }
            }
            for (Int32 i = 1; i < list.Count(); ++i)
            {
                Decimal temp = 0;
                if (list[i] == "*")
                {
                    temp = Decimal.Parse(list[i - 1]) * Decimal.Parse(list[i + 1]);
                }
                else if (list[i] == "/")
                {
                    temp = Decimal.Parse(list[i - 1]) / Decimal.Parse(list[i + 1]);
                }
                else if (list[i] == "+")
                {
                    temp = Decimal.Parse(list[i - 1]) + Decimal.Parse(list[i + 1]);
                }
                list.RemoveAt(i - 1);
                list.RemoveAt(i - 1);
                list[i - 1] = temp.ToString();
                i = 0;
            }
            return list;
        }
    }
}
