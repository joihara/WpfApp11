using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp11.Library
{
    public class Manager : Consultant
    {
        public override bool Edit() { return true; }

        private FileUtil fileUtil;



        public Manager(){
               
            Console.WriteLine();
        }
    }
}
