using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.TZTest
{
   public   class TwoDivide
    {
        public static (double, double, double, double) Mubiao(double a0, double b0, double x0,double a,double b, double step = 0.1)
        {
            double yleft = cal(a0);
            double yright = cal(b0);
            double y0 = cal(x0);

            double ymax = Math.Max(yleft, yright);
            ymax = Math.Max(ymax, y0);
            double resa = a;
            double resb = b;
            double resx0 = x0;
            if (ymax == yleft)
            {
                resa = a0-(x0-a0)/2;
                resb =a0 + (x0 - a0) / 2;
                resx0 = a0;
            }
            else if (ymax == y0)
            {
                resa = aver(a0, x0); 
                resb = aver(b0, x0);
                resx0 = x0;
            }
            else if (ymax == yright)
            {
                resa = b0-(b0-x0 )/2;
                resb = b0 + (b0 - x0) / 2;
                resx0 = b0;
            }
            if (resa <= a) resa = a;
            if (resb >= b) resb = b;

            resa = normalstep(a, b, resa, step);
            resb = normalstep(a, b, resb, step);
            resx0 = normalstep(a, b, resx0, step);

            return (ymax, resa, resb, resx0);
        }


        public static  (double,double ,double ,double  )Mubiao(double a, double b, double x0,double step =0.1)
        {
            double xleft = aver(a, x0);
            double xright = aver(b, x0);
            xleft = normalstep(a, b, xleft, step);
            xright = normalstep(a, b, xright, step);
            x0= normalstep(a, b, x0, step);
            double yleft = cal(xleft);
            double yright = cal(xright);
            double y0 = cal(x0);

            double ymax = Math.Max  (yleft, yright);
            ymax = Math.Max(ymax, y0);
            double resa = a;
            double resb = b;
            double resx0 = x0;
            if (ymax == yleft)
            {
                resa = aver(a, xleft);
                resb = aver(xleft, x0);
                resx0 = xleft;
            }
            else if (ymax == y0)
            {
                resa = xleft;
                resb = xright;
                resx0 = x0;
            }
            else if (ymax == yright)
            {
                resa = aver(x0, xright) ;
                resb = aver (xright,b);
                resx0 = xright ;
            }

            resa = normalstep(a, b, resa, step);
            resb = normalstep(a, b, resb, step);
            resx0 = normalstep(a, b, resx0, step);

            return (ymax,resa,resb,resx0);
        }
        public static double aver(double a, double b)
        { return  (a+b)/ 2; }

        public static double cal(double a)
        {
            return Math.Sin (a);  
        }

        public static double normalstep(double a, double b,double x, double step)
        {
            int n = (int)Math.Ceiling ( (x - a) / step);
            double tem = a + n * step;
            if (tem >= b) return b;
            double temright = a + (n + 1) * step;
            if (Math.Abs(tem - x) <= Math.Abs(temright - x))
                return tem;
            if (temright >= b) return b;
            return temright;

        }

    }
}
