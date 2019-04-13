using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_OOP
{
    class Calcul
    {
        Boolean min;
        Boolean fin;
        private String m_result;    //결과
        private Boolean prev_ch;
        Boolean Prev_ch { get => prev_ch; set => prev_ch = value; }
        List<String> m_callist = new List<String>();
        List<String> m_printList = new List<String>();



        //      Inputs :
        //      AddProcess(Int32) 	//숫자 입력
        //      AddProcess(char)	//부호 입력
        //      DelProcess(Boolean)	//삭제 명령, True : C, False : CE
        //      
        //      Outputs :
        //      getFin()	//계산 완료여부 Boolean로 반환
        //      GetListString()	//입력된 List 데이터 String으로 반환.
        //      ReturnBinary()	//결과값 2진수로 변환후 String으로 반환
        public Calcul()
        {
            min = false;
            fin = false;
        }

        public void DelProcess(Boolean c)
        {
            if (c)//다 지움
            {
                Reset_List(m_callist);
                Reset_List(m_printList);
                min = false;
                prev_ch = false;
            }
            else if (!fin) //지금꺼만 지움
            {
                m_printList.RemoveAt(m_printList.Count - 1);
            }
            m_result = null;
        }
        public Int32 AddProcess(Int32 _d)
        {
            if (fin)
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
        public Int32 AddProcess(Char _d)
        {

            if (_d == '.')
            {
                m_result += _d;
            }
            else
            {
                if (!fin && m_result != null)
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
                    m_printList.RemoveAt(m_printList.Count - 1);
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

        public Boolean getfin()
        {
            return fin;
        }
        public String PrintString()
        {
            return GetListString(Get_PrintList());
        }
        public String ReturnBinary()
        {
            String output16 = null;
            if (m_printList.Count > 0 && m_printList.Count % 2 == 1)
            {
                output16 = System.Convert.ToString(Int32.Parse(m_printList[m_printList.Count - 1]), 2);
            }
            return output16;
        }

        void Reset_List(List<String> list)
        {
            list = null;
            list = new List<string>();
        }
        String GetListString(List<String> list)
        {
            String str = null;
            for (Int32 i = 0; i < list.Count; ++i)
            {
                str += list[i] + " ";
            }
            return str;
        }
        List<String> Get_CalList()
        {
            List<String> _list = m_callist;
            return _list;
        }
        List<String> Get_PrintList()
        {
            List<String> _list = m_printList;
            return _list;
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
