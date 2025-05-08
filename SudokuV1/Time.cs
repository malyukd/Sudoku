using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuV1
{
    public class Time
    {
        public static Time[] recordList = new Time[5];
        private int sec;
        private int min;
        public Time(int sec, int min) { 
            this.sec = sec;
            this.min = min;
        }
        public Time(int sec)
        {
            this.sec = sec % 60; ;
            this.min = sec/60;
        }
        public int ToSec()
        {
            return this.min * 60 + this.sec;
        }
        public string ToStr()
        {
            return $"{this.min:00}:{this.sec:00}";

        }

        public bool Record(int level)
        {
            if (recordList[level-1] == null || recordList[level-1].ToSec() > this.ToSec())
            {
                recordList[level-1] = this;
                return true;
            }
            return false;
        }
    }
}
